using LetheAISharp.Agent.Tools;
using LetheAISharp.LLM;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using OpenAI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LetheChat.Plugins
{
    /// <summary>
    /// Function-calling toolset that gives the persona a sandboxed view of a user-configured
    /// "workspace" folder: directory navigation, filename pattern matching (Glob), content
    /// search (Grep), text reading/writing, literal string replacement, recycle-bin delete,
    /// move/rename, and shell-opening files/programs for the user.
    /// All paths are workspace-relative and strictly contained by <see cref="TryResolvePath"/>.
    /// Settings come from <c>FileTools.json</c> beside the executable (see <see cref="FileToolsSettings"/>).
    /// </summary>
    internal class FileTools : IToolList
    {
        public string Id => "File Tools";

        public string Description =>
            "A set of tools for navigating, searching, reading and writing files inside a sandboxed " +
            "workspace folder on the user's machine, managing them (delete to recycle bin, move/rename), " +
            "and opening files or programs for the user.";

        public string SystemPromptInstruction =>
            "You have access to a [File] management tools operating inside a sandboxed 'workspace' folder on the user's machine. " +
            "All paths you provide are RELATIVE to that folder; absolute paths are refused. You cannot see the filesystem directly, " +
            "so orient yourself with ListDirectory (single folder) or GetTree (recursive outline) first, and use Glob " +
            "(filename patterns, e.g. '**/*.md') or Grep (search file contents by regex) to locate files. " +
            "ReadFile is line-windowed: if the reply says output was truncated, call it again with a higher startLine to continue. " +
            "Always ReadFile a file before editing it; ReplaceString matches literal text, so copy the exact characters from the file. " +
            "WriteFile replaces the WHOLE file; use AppendToFile to add text at the end. DeletePath sends files or folders to the " +
            "recycle bin; MovePath renames or moves them. OpenWithShell opens a file, folder or program from the workspace for the " +
            "user with its default application. Hidden, system and link/junction entries are not visible.";

        private readonly List<Tool> toolList = [];

        public IReadOnlyList<Tool> GetToolList() => toolList;

        public bool RequiresConfirmation(string functionName)
        {
            // StartsWith is used because some backends append random strings to function names;
            // see WebSearchTools. Only shell execution requires user approval.
            return functionName.StartsWith(nameof(FS_OpenWithShell), StringComparison.OrdinalIgnoreCase);
        }

        // ── Sandbox ──────────────────────────────────────────────────────────

        /// <summary>Maximum size of a file accepted by text-reading/editing tools.</summary>
        private const long MaxTextFileBytes = 2 * 1024 * 1024;
        /// <summary>Maximum characters returned by a single ReadFile call.</summary>
        private const int MaxReadChars = 15_000;
        /// <summary>Maximum lines a single ReadFile call may request.</summary>
        private const int MaxReadLines = 250;
        /// <summary>Maximum entries returned by ListDirectory.</summary>
        private const int MaxListEntries = 200;
        /// <summary>Maximum lines returned by GetTree.</summary>
        private const int MaxTreeLines = 400;
        /// <summary>Maximum depth GetTree/Glob may be asked to descend.</summary>
        private const int MaxTreeDepth = 8;
        /// <summary>Maximum paths returned by Glob.</summary>
        private const int MaxGlobResults = 100;
        /// <summary>Maximum matches returned by Grep.</summary>
        private const int MaxGrepMatches = 100;
        /// <summary>Maximum files Grep will scan before stopping.</summary>
        private const int MaxGrepFiles = 200;
        /// <summary>Maximum size of a file Grep will scan.</summary>
        private const long MaxGrepFileBytes = 2 * 1024 * 1024;
        /// <summary>Number of leading bytes inspected to detect binary files.</summary>
        private const int BinarySniffBytes = 4096;

        // Hidden/system/reparse entries are skipped at enumeration level; dot-prefixed
        // names are filtered in code to match the ObsidianToolset convention.
        private static readonly EnumerationOptions TopLevelEnumOptions = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = false,
            AttributesToSkip = FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReparsePoint
        };

        private static readonly EnumerationOptions RecursiveEnumOptions = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReparsePoint
        };

        /// <summary>
        /// Resolves and validates the configured workspace root. Returns false with an error string
        /// when no folder is configured, the path is invalid, or the folder does not exist.
        /// </summary>
        private static bool TryGetRoot(out string root, out string error)
        {
            root = string.Empty;
            error = string.Empty;
            var folder = Program.Settings.WorkspaceFolder?.Trim() ?? string.Empty;
            if (folder.Length == 0)
            {
                error = "No workspace folder is configured. Tell the user to set 'WorkspaceFolder' in settings (beside the app executable) and restart the app.";
                return false;
            }
            try
            {
                root = Path.GetFullPath(folder);
            }
            catch (Exception ex)
            {
                error = $"The configured workspace folder is not a valid path: {ex.Message}";
                return false;
            }
            if (!Directory.Exists(root))
            {
                error = $"The configured workspace folder '{root}' does not exist. Tell the user to fix 'WorkspaceFolder' in FileTools.json.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Resolves a workspace-relative path to an absolute path, strictly contained in the workspace.
        /// Returns false with a human-readable error when the input is rooted, escapes the sandbox
        /// (via traversal or a junction/symlink), or is malformed.
        /// </summary>
        /// <param name="relativePath">Workspace-relative path ('' allowed only when <paramref name="allowRoot"/>).</param>
        /// <param name="allowRoot">Whether the bare workspace root itself is a valid target (listing operations).</param>
        private static bool TryResolvePath(string relativePath, bool allowRoot, out string fullPath, out string root, out string error)
        {
            fullPath = string.Empty;
            if (!TryGetRoot(out root, out error))
                return false;

            var rel = (relativePath ?? string.Empty).Trim();
            if (rel.Length == 0 || rel == ".")
            {
                if (allowRoot)
                {
                    fullPath = root;
                    return true;
                }
                error = "A workspace-relative path is required.";
                return false;
            }

            if (Path.IsPathRooted(rel))
            {
                error = $"'{rel}' is not a workspace-relative path. All paths must be relative to the workspace folder; absolute paths are not allowed.";
                return false;
            }

            string full;
            try
            {
                full = Path.GetFullPath(Path.Combine(root, rel));
            }
            catch (Exception)
            {
                error = $"'{rel}' is not a valid path.";
                return false;
            }

            // Containment: the normalized result must be the root itself or below it.
            var prefix = root.EndsWith(Path.DirectorySeparatorChar) ? root : root + Path.DirectorySeparatorChar;
            if (!full.Equals(root, StringComparison.OrdinalIgnoreCase) && !full.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                error = $"'{rel}' resolves outside the workspace folder, which is not allowed.";
                return false;
            }

            // Reparse-point guard: no segment between the root and the target may be a
            // junction/symlink, otherwise a planted link could redirect us outside.
            var relNorm = full[root.Length..].TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (relNorm.Length > 0)
            {
                var acc = root;
                foreach (var segment in relNorm.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
                {
                    acc = Path.Combine(acc, segment);
                    if (!Directory.Exists(acc))
                        break; // missing component: nothing more to check
                    var linkTarget = new DirectoryInfo(acc).LinkTarget;
                    if (linkTarget != null)
                    {
                        error = $"'{rel}' crosses a link or junction ('{segment}'), which is not allowed.";
                        return false;
                    }
                }
                // A symlinked *file* as the final segment is not covered by the directory walk.
                if (File.Exists(full) && new FileInfo(full).LinkTarget != null)
                {
                    error = $"'{rel}' is a link, which is not allowed.";
                    return false;
                }
            }

            fullPath = full;
            error = string.Empty;
            return true;
        }

        /// <summary>True when the resolved path IS the workspace root itself.</summary>
        private static bool IsRoot(string fullPath, string root) =>
            fullPath.Equals(root, StringComparison.OrdinalIgnoreCase);

        /// <summary>True when any path segment starts with a dot (skipped like the ObsidianToolset does).</summary>
        private static bool HasDotSegment(string relativePath) =>
            relativePath.Split('/', '\\').Any(s => s.Length > 0 && s.StartsWith('.'));

        /// <summary>Heuristic binary detection: a NUL byte in the first bytes of the file.</summary>
        private static bool LooksBinary(string path)
        {
            try
            {
                using var fs = File.OpenRead(path);
                var buf = new byte[Math.Min(BinarySniffBytes, fs.Length)];
                if (buf.Length == 0)
                    return false;
                fs.ReadExactly(buf);
                return buf.Contains((byte)0);
            }
            catch
            {
                return true;
            }
        }

        private static string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:0.#} KB";
            if (bytes < 1024L * 1024 * 1024) return $"{bytes / (1024.0 * 1024):0.#} MB";
            return $"{bytes / (1024.0 * 1024 * 1024):0.#} GB";
        }

        private static string FormatDate(DateTime time) => time.ToString("yyyy-MM-dd HH:mm");

        // ── Glob matching ────────────────────────────────────────────────────

        /// <summary>
        /// Compiles a glob pattern ('*' = any characters within a name, '?' = one character,
        /// '**' = any characters including separators) into a regex matched against
        /// forward-slash-normalized relative paths.
        /// </summary>
        private static Regex GlobToRegex(string glob)
        {
            var sb = new StringBuilder("^");
            for (var i = 0; i < glob.Length; i++)
            {
                var c = glob[i];
                if (c == '*')
                {
                    if (i + 1 < glob.Length && glob[i + 1] == '*')
                    {
                        // '**' crosses separators; swallow an adjacent '/' so 'src/**' feels natural.
                        if (i + 2 < glob.Length && glob[i + 2] == '/')
                            i += 2;
                        else
                            i += 1;
                        sb.Append(".*");
                    }
                    else
                    {
                        sb.Append("[^/]*");
                    }
                }
                else if (c == '?')
                {
                    sb.Append("[^/]");
                }
                else
                {
                    sb.Append(Regex.Escape(c.ToString()));
                }
            }
            sb.Append('$');
            return new Regex(sb.ToString(), RegexOptions.IgnoreCase);
        }

        // ── Tool registration ────────────────────────────────────────────────

        public void LoadTools(bool clearExisting = false)
        {
            toolList.Clear();
            if (clearExisting)
                Tool.ClearRegisteredTools();

            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_ListDirectory), "[File] Lists the subfolders and files (with size and last-modified date) of a workspace folder. Use empty string for the workspace root."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_GetTree), "[Files] Returns a recursive outline of folders and files under a workspace folder so you can get oriented. Use empty string for the whole workspace. Prefer this over walking folders one level at a time."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_Glob), "[File] Finds files by filename pattern and returns workspace-relative paths. Supports * (any characters within a name), ? (one character) and ** (recursion into subfolders), e.g. '**/*.txt' finds all text files at any depth while '*.txt' only matches the top level. Use empty path for the whole workspace."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_Grep), "[File] Searches file CONTENTS with a case-insensitive regular expression, returning 'file:line: snippet' matches. 'include' optionally filters which files to scan by name pattern (e.g. '*.cs'). Use empty path to search the whole workspace."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_ReadFile), "[File] Reads a text file between startLine and startLine+maxLines. The reply reports the file's total line count and whether output was truncated; call again with a higher startLine to read further."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_WriteFile), "[File] Creates a file or overwrites an existing one with the full content provided. WARNING: this replaces the entire file. Parent folders are created as needed. ReadFile first if the file may already exist."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_AppendToFile), "[File] Appends text to the end of an existing file. The file must already exist."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_ReplaceString), "[File] Replaces every occurrence of a literal string with another string inside a text file. Fails if nothing matches; ReadFile the file first to copy the exact text."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_DeletePath), "[File] Moves a file or folder to the recycle bin. Folders are deleted including all their contents. The workspace root itself cannot be deleted."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_MovePath), "[File] Renames or moves a file or folder to another workspace-relative path. Fails if the destination already exists; parent folders are created as needed."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FS_OpenWithShell), "[File] Opens a file, folder or program from the workspace for the user with its default application: launches an .exe, opens a media file in the associated player, a document in its editor, or a folder in Explorer. Requires the user's confirmation."));
        }

        public void UnloadTools()
        {
            foreach (var tool in toolList)
                Tool.TryUnregisterTool(tool);
            toolList.Clear();
        }

        // ── Tools: navigation ────────────────────────────────────────────────

        public async Task<string> FS_ListDirectory(
            [FunctionParameter("Workspace-relative folder to list. Use an empty string for the workspace root.")] string path = "")
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, true, out var full, out var root, out var err))
                return err;
            if (!Directory.Exists(full))
                return $"Folder not found: {path}";

            var dirs = Directory.GetDirectories(full, "*", TopLevelEnumOptions)
                .Select(d => $"{Path.GetFileName(d)}/")
                .OrderBy(d => d, StringComparer.OrdinalIgnoreCase)
                .ToList();
            var files = Directory.GetFiles(full, "*", TopLevelEnumOptions)
                .Select(f =>
                {
                    var fi = new FileInfo(f);
                    return $"{fi.Name} ({FormatSize(fi.Length)}, {FormatDate(fi.LastWriteTime)})";
                })
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var all = dirs.Concat(files).ToList();
            if (all.Count == 0)
                return "The folder is empty.";
            var sb = new StringBuilder();
            var shown = Math.Min(all.Count, MaxListEntries);
            sb.AppendJoin("\n", all.Take(shown));
            if (all.Count > shown)
                sb.Append($"\n(and {all.Count - shown} more entries not shown)");
            if (IsRoot(full, root))
                sb.Append("\n(This is the workspace root.)");
            return sb.ToString();
        }

        public async Task<string> FS_GetTree(
            [FunctionParameter("Workspace-relative folder to start the outline from. Use an empty string for the whole workspace.")] string path = "",
            [FunctionParameter("Maximum folder depth to descend, between 1 and 8. Defaults to 3; use a small number for a high-level overview.")] int maxDepth = 3)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, true, out var full, out var root, out var err))
                return err;
            if (!Directory.Exists(full))
                return $"Folder not found: {path}";
            var depth = Math.Clamp(maxDepth, 1, MaxTreeDepth);

            var sb = new StringBuilder();
            var truncated = false;
            BuildTree(full, 0, depth, sb, ref truncated);
            if (sb.Length == 0)
                return "The folder is empty.";
            if (truncated)
                sb.Append("\n(... output truncated, use a smaller maxDepth or a narrower path)");
            return sb.ToString().TrimEnd();
        }

        private void BuildTree(string dir, int depth, int maxDepth, StringBuilder sb, ref bool truncated)
        {
            if (truncated || sb.Length > 20000)
            {
                truncated = true;
                return;
            }
            var indent = new string(' ', depth * 2);
            foreach (var sub in Directory.GetDirectories(dir, "*", TopLevelEnumOptions).OrderBy(d => d, StringComparer.OrdinalIgnoreCase))
            {
                sb.AppendLine($"{indent}{Path.GetFileName(sub)}/");
                if (depth + 1 < maxDepth)
                    BuildTree(sub, depth + 1, maxDepth, sb, ref truncated);
                else
                    sb.AppendLine($"{indent}  ...");
                if (sb.Length > 20000)
                {
                    truncated = true;
                    return;
                }
            }
            foreach (var file in Directory.GetFiles(dir, "*", TopLevelEnumOptions).OrderBy(f => f, StringComparer.OrdinalIgnoreCase))
                sb.AppendLine($"{indent}{Path.GetFileName(file)}");
        }

        public async Task<string> FS_Glob(
            [FunctionParameter("Filename pattern: '*' matches any characters within a name, '?' a single character, '**' recurses into subfolders. Examples: '*.txt' (top level only), '**/*.md' (every .md at any depth), 'src/**/*.cs'.")] string pattern,
            [FunctionParameter("Workspace-relative folder to search under. Use an empty string for the whole workspace.")] string path = "")
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(pattern))
                return "A pattern is required (e.g. '**/*.txt').";
            if (!TryResolvePath(path, true, out var full, out var root, out var err))
                return err;
            if (!Directory.Exists(full))
                return $"Folder not found: {path}";

            Regex matcher;
            try
            {
                matcher = GlobToRegex(pattern.Trim().Replace('\\', '/'));
            }
            catch (Exception)
            {
                return $"'{pattern}' is not a valid glob pattern.";
            }

            List<string> matches = [];
            foreach (var file in Directory.GetFiles(full, "*", RecursiveEnumOptions))
            {
                var rel = Path.GetRelativePath(full, file).Replace('\\', '/');
                if (HasDotSegment(rel))
                    continue;
                if (matcher.IsMatch(rel))
                {
                    matches.Add(Path.GetRelativePath(root, file).Replace('\\', '/'));
                    if (matches.Count >= MaxGlobResults)
                        break;
                }
            }
            if (matches.Count == 0)
                return $"No files match '{pattern}'.";
            matches.Sort(StringComparer.OrdinalIgnoreCase);
            return matches.Count >= MaxGlobResults
                ? string.Join("\n", matches) + $"\n(results capped at {MaxGlobResults}; refine the pattern)"
                : string.Join("\n", matches);
        }

        public async Task<string> FS_Grep(
            [FunctionParameter("Case-insensitive regular expression to search for inside file contents, e.g. 'TODO|FIXME' or 'error \\d+'.")] string pattern,
            [FunctionParameter("Workspace-relative folder to search under. Use an empty string for the whole workspace.")] string path = "",
            [FunctionParameter("Optional filename filter using glob syntax (e.g. '*.cs' for all C# files anywhere). Leave empty to scan every text file.")] string include = "")
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(pattern))
                return "A search pattern is required.";
            if (!TryResolvePath(path, true, out var full, out var root, out var err))
                return err;
            if (!Directory.Exists(full))
                return $"Folder not found: {path}";

            Regex contentRegex;
            try
            {
                contentRegex = new Regex(pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (ArgumentException)
            {
                return $"'{pattern}' is not a valid regular expression.";
            }

            Regex? includeRegex = null;
            if (!string.IsNullOrWhiteSpace(include))
            {
                try
                {
                    includeRegex = GlobToRegex(include.Trim().Replace('\\', '/'));
                }
                catch (Exception)
                {
                    return $"'{include}' is not a valid filename pattern.";
                }
            }

            var sb = new StringBuilder();
            var matchCount = 0;
            var scanned = 0;
            var skippedBinary = 0;
            var hitCap = false;

            foreach (var file in Directory.GetFiles(full, "*", RecursiveEnumOptions))
            {
                if (hitCap)
                    break;
                var relToBase = Path.GetRelativePath(full, file);
                if (HasDotSegment(relToBase))
                    continue;
                if (includeRegex != null)
                {
                    var relNorm = relToBase.Replace('\\', '/');
                    var name = Path.GetFileName(file);
                    var matched = relNorm.Contains('/')
                        ? includeRegex.IsMatch(relNorm) || includeRegex.IsMatch(name)
                        : includeRegex.IsMatch(name);
                    if (!matched)
                        continue;
                }
                if (++scanned > MaxGrepFiles)
                {
                    sb.Append($"\n(stopped after scanning {MaxGrepFiles} files; narrow the search)");
                    break;
                }
                var fi = new FileInfo(file);
                if (fi.Length == 0 || fi.Length > MaxGrepFileBytes || LooksBinary(file))
                {
                    skippedBinary++;
                    continue;
                }

                string[] lines;
                try
                {
                    lines = File.ReadAllLines(file);
                }
                catch (Exception)
                {
                    skippedBinary++;
                    continue;
                }

                var relResult = Path.GetRelativePath(root, file).Replace('\\', '/');
                for (var i = 0; i < lines.Length; i++)
                {
                    bool hit;
                    try
                    {
                        hit = contentRegex.IsMatch(lines[i]);
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        break; // pathological pattern on this file; skip the rest of it
                    }
                    if (!hit)
                        continue;
                    var snippet = lines[i].Trim();
                    if (snippet.Length > 200)
                        snippet = snippet[..200] + "...";
                    sb.AppendLine($"{relResult}:{i + 1}: {snippet}");
                    if (++matchCount >= MaxGrepMatches)
                    {
                        sb.Append($"(results capped at {MaxGrepMatches}; refine the search)");
                        hitCap = true;
                        break;
                    }
                }
            }

            if (matchCount == 0)
            {
                var note = skippedBinary > 0 ? $" ({skippedBinary} file(s) skipped as binary or unreadable)" : string.Empty;
                return $"No matches for '{pattern}'.{note}";
            }
            return sb.ToString();
        }

        // ── Tools: reading and writing ───────────────────────────────────────

        public async Task<string> FS_ReadFile(
            [FunctionParameter("Workspace-relative path to the text file to read.")] string path,
            [FunctionParameter("Zero-based line number to start reading from. Defaults to 0 (start of file).")] int startLine = 0,
            [FunctionParameter("Maximum number of lines to read, between 1 and 500. Defaults to 200.")] int maxLines = 200)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var full, out var root, out var err))
                return err;
            if (!File.Exists(full))
                return $"File not found: {path}";
            var fi = new FileInfo(full);
            if (fi.Length > MaxTextFileBytes)
                return $"File '{path}' is {FormatSize(fi.Length)}, too large to read (limit {FormatSize(MaxTextFileBytes)}).";
            if (LooksBinary(full))
                return $"File '{path}' appears to be binary and cannot be shown as text.";

            var lines = File.ReadAllLines(full);
            var total = lines.Length;
            if (total == 0)
                return $"File '{path}' exists but is empty.";
            var start = Math.Clamp(startLine, 0, total - 1);
            var count = Math.Clamp(maxLines, 1, MaxReadLines);
            var sb = new StringBuilder();
            var emitted = 0;
            var charTruncated = false;
            for (var i = start; i < total && emitted < count; i++, emitted++)
            {
                var line = lines[i];
                if (sb.Length + line.Length > MaxReadChars)
                {
                    charTruncated = true;
                    break;
                }
                sb.AppendLine(line);
            }
            var last = start + emitted - 1;
            var header = $"File '{path}' has {total} line(s). Showing lines {start + 1}..{last + 1} of {total}.";
            if (start > 0 || last + 1 < total || charTruncated)
            {
                var next = charTruncated ? start + emitted : last + 1;
                header += next < total
                    ? $" Output was truncated; call ReadFile again with startLine={next} to continue."
                    : " Output was truncated.";
            }
            return header + "\n" + sb.ToString().TrimEnd();
        }

        public async Task<string> FS_WriteFile(
            [FunctionParameter("Workspace-relative path to the file to create or overwrite. Parent folders are created as needed.")] string path,
            [FunctionParameter("The full content to write. WARNING: if the file already exists, its entire previous content is replaced.")] string content)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var full, out var root, out var err))
                return err;
            var dir = Path.GetDirectoryName(full);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
            var existed = File.Exists(full);
            File.WriteAllText(full, content ?? string.Empty);
            return existed
                ? $"File '{path}' overwritten ({FormatSize(new FileInfo(full).Length)} written)."
                : $"File '{path}' created ({FormatSize(new FileInfo(full).Length)} written).";
        }

        public async Task<string> FS_AppendToFile(
            [FunctionParameter("Workspace-relative path to the existing file to append to.")] string path,
            [FunctionParameter("The text to append at the end of the file.")] string content)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var full, out var root, out var err))
                return err;
            if (!File.Exists(full))
                return $"File not found: {path}. Use WriteFile to create it first.";
            File.AppendAllText(full, content ?? string.Empty);
            return $"Appended text to '{path}' (file is now {FormatSize(new FileInfo(full).Length)}).";
        }

        public async Task<string> FS_ReplaceString(
            [FunctionParameter("Workspace-relative path to the text file to edit.")] string path,
            [FunctionParameter("The exact literal text to find (not a regular expression). Every occurrence is replaced.")] string find,
            [FunctionParameter("The text to substitute in place of each occurrence of 'find'.")] string replace)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var full, out var root, out var err))
                return err;
            if (!File.Exists(full))
                return $"File not found: {path}";
            if (string.IsNullOrEmpty(find))
                return "The text to find must not be empty.";
            var fi = new FileInfo(full);
            if (fi.Length > MaxTextFileBytes)
                return $"File '{path}' is {FormatSize(fi.Length)}, too large to edit (limit {FormatSize(MaxTextFileBytes)}).";

            var content = File.ReadAllText(full);
            var count = Regex.Matches(content, Regex.Escape(find)).Count;
            if (count == 0)
                return $"The text to replace was not found in '{path}'. ReadFile the file and copy the exact text.";
            File.WriteAllText(full, content.Replace(find, replace));
            return $"Replaced {count} occurrence(s) in '{path}'.";
        }

        // ── Tools: file management ───────────────────────────────────────────

        public async Task<string> FS_DeletePath(
            [FunctionParameter("Workspace-relative path to the file or folder to move to the recycle bin.")] string path)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var full, out var root, out var err))
                return err;
            if (IsRoot(full, root))
                return "The workspace root itself cannot be deleted.";
            if (Directory.Exists(full))
            {
                FileSystem.DeleteDirectory(full, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                return $"Folder '{path}' moved to the recycle bin.";
            }
            if (File.Exists(full))
            {
                FileSystem.DeleteFile(full, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                return $"File '{path}' moved to the recycle bin.";
            }
            return $"Not found: {path}";
        }

        public async Task<string> FS_MovePath(
            [FunctionParameter("Workspace-relative path to the existing file or folder to rename or move.")] string path,
            [FunctionParameter("New workspace-relative path for the file or folder. Fails if the destination already exists; parent folders are created as needed.")] string newPath)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var src, out var root, out var err))
                return err;
            if (!TryResolvePath(newPath, false, out var dst, out _, out err))
                return err;
            if (IsRoot(src, root))
                return "The workspace root itself cannot be moved.";
            if (IsRoot(dst, root) || File.Exists(dst) || Directory.Exists(dst))
                return $"A file or folder already exists at '{newPath}'.";
            var isDir = Directory.Exists(src);
            if (!isDir && !File.Exists(src))
                return $"Not found: {path}";
            if (isDir && dst.StartsWith(src.EndsWith(Path.DirectorySeparatorChar) ? src : src + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                return "A folder cannot be moved inside itself.";

            var dstDir = Path.GetDirectoryName(dst);
            if (!string.IsNullOrEmpty(dstDir))
                Directory.CreateDirectory(dstDir);
            if (isDir)
                Directory.Move(src, dst);
            else
                File.Move(src, dst);
            return $"'{path}' moved to '{newPath}'.";
        }

        // ── Tools: shell execution ───────────────────────────────────────────

        public async Task<string> FS_OpenWithShell(
            [FunctionParameter("Workspace-relative path to the file, folder or program to open for the user.")] string path)
        {
            await Task.Delay(5).ConfigureAwait(false);
            if (!TryResolvePath(path, false, out var full, out var root, out var err))
                return err;
            if (!File.Exists(full) && !Directory.Exists(full))
                return $"Not found: {path}";

            var psi = new ProcessStartInfo
            {
                FileName = full,
                UseShellExecute = true,
            };
            var dir = Path.GetDirectoryName(full);
            if (!string.IsNullOrEmpty(dir))
                psi.WorkingDirectory = dir;

            try
            {
                Process.Start(psi);
            }
            catch (Win32Exception ex)
            {
                return $"Failed to open '{path}': {ex.Message} (no associated application, or the user declined).";
            }
            catch (Exception ex)
            {
                return $"Failed to open '{path}': {ex.Message}";
            }
            return $"Opened '{path}' for the user.";
        }
    }
}
