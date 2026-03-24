using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Markdig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LetheChat.Controls;

namespace LetheChat.Forms
{
    public partial class MemoryBrowserForm : Form
    {
        private readonly List<MemoryUnit> _allMemories = [];
        private readonly List<MemoryUnit> _filteredMemories = [];

        // Sorting state (default: sort by Added desc)
        private int _sortColumnIndex = 2;
        private bool _sortAscending = false;

        private readonly List<ExtractedFact> _allFacts = [];
        private readonly List<ExtractedFact> _filteredFacts = [];

        private int _FactsortColumnIndex = 1;
        private bool _FactsortAscending = false;
        private bool _FactClickEventSet = false;

        private static readonly MarkdownPipeline MarkdownPipeline =
            new MarkdownPipelineBuilder()
                .UseSoftlineBreakAsHardlineBreak()
                .UseAdvancedExtensions()
                .UseEmojiAndSmiley()
                .UseAutoLinks()
                .Build();

        public MemoryBrowserForm()
        {
            InitializeComponent();
            KeyPreview = true;

            // Set WebView background to match dark theme (overrides Designer's white)
            try { webView.DefaultBackgroundColor = Color.FromArgb(0x12, 0x14, 0x17); } catch { }

            LoadFromActiveBot();
            PopulateCategories();
            ApplyFilterAndRefreshList();
            //RefreshListView();
        }

        public static void ShowForActiveBot(IWin32Window? owner = null)
        {
            using var f = new MemoryBrowserForm();
            ThemeManager.ApplyToForm(f);
            f.ShowDialog(owner);
        }

        private void LoadFromActiveBot()
        {
            _allMemories.Clear();

            var bot = LLMEngine.Bot;
            if (bot == null)
            {
                MessageBox.Show("No active bot found.");
                Close();
                return;
            }

            // Gather all memories from all categories
            foreach (var value in Enum.GetValues<MemoryType>().Cast<MemoryType>())
            {
                try
                {
                    var list = bot.Brain.GetMemories(value);
                    if (list != null && list.Count > 0)
                        _allMemories.AddRange(list);
                }
                catch
                {
                    // Ignore categories not supported by Brain implementation
                }
            }

            _allFacts.Clear();
            _allFacts.AddRange(bot.Brain.ExtractedFacts);
        }

        private void PopulateCategories()
        {
            cbCategory.Items.Clear();
            cbCategory.Items.Add("All");
            foreach (var value in Enum.GetValues<MemoryType>())
                cbCategory.Items.Add(value);

            cbCategory.SelectedIndexChanged -= cbCategory_SelectedIndexChanged;
            cbCategory.SelectedIndex = 0;
            cbCategory.SelectedIndexChanged += cbCategory_SelectedIndexChanged;
        }

        private void cbCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyFilterAndRefreshList();
        }

        private void ApplyFilterAndRefreshList()
        {
            _filteredMemories.Clear();

            if (cbCategory.SelectedItem is string s && s == "All")
            {
                _filteredMemories.AddRange(_allMemories.OrderByDescending(m => SafeDate(m)));
            }
            else if (cbCategory.SelectedItem is MemoryType mt)
            {
                _filteredMemories.AddRange(
                    _allMemories.Where(m => m.Category == mt)
                                .OrderByDescending(m => SafeDate(m)));
            }
            else
            {
                _filteredMemories.AddRange(_allMemories.OrderByDescending(m => SafeDate(m)));
            }

            RefreshListView();
        }

        private static DateTime SafeDate(MemoryUnit m)
        {
            try
            {
                var prop = m.GetType().GetProperty("Added");
                if (prop?.GetValue(m) is DateTime dt) return dt;
            }
            catch { }
            return DateTime.MinValue;
        }

        private void RefreshListView()
        {
            listMemories.BeginUpdate();
            listMemories.Items.Clear();

            foreach (var m in _filteredMemories)
            {
                var item = new ListViewItem(m.Name ?? "[untitled]");
                item.SubItems.Add(m.Category.ToString());

                var addedStr = "";
                try
                {
                    var prop = m.GetType().GetProperty("Added");
                    if (prop?.GetValue(m) is DateTime dt)
                        addedStr = dt.ToString("yyyy-MM-dd HH:mm");
                }
                catch { }

                item.SubItems.Add(addedStr);
                item.Tag = m;
                listMemories.Items.Add(item);
            }

            listMemories.EndUpdate();

            // Apply current sorting preference
            ApplyListSorting();

            if (listMemories.Items.Count > 0)
            {
                listMemories.Items[0].Selected = true;
                listMemories.Select();
                if (mainTabControl.SelectedTab == tabBrowse)
                    ShowSelectedMemory();
            }
            else
            {
                if (mainTabControl.SelectedTab == tabBrowse)
                    ShowMemoryHtml(null);
            }

            _filteredFacts.Clear();
            _filteredFacts.AddRange(_allFacts.OrderByDescending(f => f.FirstSeen));

            listFacts.BeginUpdate();
            listFacts.Items.Clear();

            foreach (var f in _filteredFacts)
            {
                var factText = f.Fact.Length > 80 ? f.Fact[..80] + "…" : f.Fact;
                var item = new ListViewItem(factText);
                item.SubItems.Add(f.FirstSeen.ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(f.LastSeen.ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(f.ReferenceCount.ToString());
                item.SubItems.Add(f.Superseded ? "✗" : "✓");
                item.Tag = f;
                listFacts.Items.Add(item);
            }

            listFacts.EndUpdate();
            ApplyListSorting();

            if (listFacts.Items.Count > 0)
            {
                listFacts.Items[0].Selected = true;
                listFacts.Select();
                if (mainTabControl.SelectedTab == tabFacts)
                    ShowSelectedFact();
            }
            else
            {
                if (mainTabControl.SelectedTab == tabFacts)
                    ShowFactHtml(null);
            }
        }

        private void ApplyListSorting()
        {
            listMemories.ListViewItemSorter = new MemoryListComparer(_sortColumnIndex, _sortAscending);
            listMemories.Sort();
            listFacts.ListViewItemSorter = new FactListComparer(_FactsortColumnIndex, _FactsortAscending);
            listFacts.Sort();
        }

        private void listMemories_ColumnClick(object? sender, ColumnClickEventArgs e)
        {
            if (e.Column == _sortColumnIndex)
            {
                // Toggle the sort order for the same column
                _sortAscending = !_sortAscending;
            }
            else
            {
                // Switch to a new column, default to ascending
                _sortColumnIndex = e.Column;
                _sortAscending = true;

                // Prefer descending by date when picking Added for the first time
                if (_sortColumnIndex == 2)
                    _sortAscending = false;
            }

            ApplyListSorting();
        }

        private void listMemories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedMemory();
        }

        private void ShowSelectedMemory()
        {
            if (listMemories.SelectedItems.Count == 0)
            {
                ShowMemoryHtml(null);
                return;
            }

            var item = listMemories.SelectedItems[0];
            var mem = item.Tag as MemoryUnit;
            ShowMemoryHtml(mem);
        }

        private static DateTime? TryGetDate(object obj, string propName)
        {
            try
            {
                var p = obj.GetType().GetProperty(propName);
                if (p?.GetValue(obj) is DateTime dt)
                    return dt;
            }
            catch { }
            return null;
        }

        private static int? TryGetInt(object obj, string propName)
        {
            try
            {
                var p = obj.GetType().GetProperty(propName);
                var val = p?.GetValue(obj);
                if (val is int i) return i;
                if (val is long l) return (int)l;
            }
            catch { }
            return null;
        }

        private static string? TryGetString(object obj, string propName)
        {
            try
            {
                var p = obj.GetType().GetProperty(propName);
                var val = p?.GetValue(obj);
                return val?.ToString();
            }
            catch { }
            return null;
        }

        private void ShowMemoryHtml(MemoryUnit? mem)
        {
            if (mem == null)
            {
                NavigateHtml(BuildHtmlPage("<i>No memory selected.</i>"));
                return;
            }

            var content = mem.Content ?? "";
            try
            {
                content = LLMEngine.Bot?.ReplaceMacros(content) ?? content;
            }
            catch { }

            var bodyHtml = Markdig.Markdown.ToHtml(content, MarkdownPipeline);

            var sentiment = "N/A";
            try
            {
                var p = mem.GetType().GetProperty("Sentiment");
                var v = p?.GetValue(mem);
                sentiment = v?.ToString() ?? "N/A";
            }
            catch { }

            var reason = "";
            try
            {
                var p = mem.GetType().GetProperty("Reason");
                var v = p?.GetValue(mem) as string;
                if (!string.IsNullOrWhiteSpace(v))
                    reason = $"<div class='reason'><strong>Reason:</strong> {System.Net.WebUtility.HtmlEncode(v)}</div><hr>";
            }
            catch { }

            // Extra details section: Added, Priority, Insertion, LastTrigger
            var addedDt = TryGetDate(mem, "Added");
            var addedStr = (addedDt.HasValue && addedDt.Value > DateTime.MinValue) ? addedDt.Value.ToString("yyyy-MM-dd HH:mm") : "N/A";

            var lastTrigDt = TryGetDate(mem, "LastTrigger");
            var lastTrigStr = (lastTrigDt.HasValue && lastTrigDt.Value > DateTime.MinValue) ? lastTrigDt.Value.ToString("yyyy-MM-dd HH:mm") : "N/A";
            var cnt = TryGetInt(mem, "TriggerCount");
            if (cnt is null)
                cnt = 0;
            lastTrigStr += $" [{cnt}]";

            var priorityStr = TryGetInt(mem, "Priority")?.ToString() ?? "N/A";
            var insertionStr = TryGetString(mem, "Insertion") ?? "N/A";

            var title = System.Net.WebUtility.HtmlEncode(mem.Name ?? "[untitled]");
            var cat = System.Net.WebUtility.HtmlEncode(mem.Category.ToString());

            var body = new StringBuilder();
            body.Append($"<div class='title'>{title}</div>");
            body.Append($"<div class='meta'><span class='badge'>{cat}</span><span class='sentiment'>Sentiment: {System.Net.WebUtility.HtmlEncode(sentiment)}</span></div>");
            if (!string.IsNullOrEmpty(reason))
                body.Append(reason);

            // Details section (##)
            body.Append("<h2>Details</h2>");
            body.Append("<div class='kv'>");
            body.Append($"<div class='k'>Added</div><div class='v'>{System.Net.WebUtility.HtmlEncode(addedStr)}</div>");
            body.Append($"<div class='k'>Priority</div><div class='v'>{System.Net.WebUtility.HtmlEncode(priorityStr)}</div>");
            body.Append($"<div class='k'>Insertion</div><div class='v'>{System.Net.WebUtility.HtmlEncode(insertionStr)}</div>");
            body.Append($"<div class='k'>Last trigger</div><div class='v'>{System.Net.WebUtility.HtmlEncode(lastTrigStr)}</div>");
            body.Append("</div><hr>");
            body.Append($"<div class='content'>{bodyHtml}</div>");

            NavigateHtml(BuildHtmlPage(body.ToString(), """
                .title { font-size: 20px; font-weight: 600; margin-bottom: 6px; color: var(--fg); }
                .sentiment {
                    display: inline-block; background: var(--sent-bg); color: var(--sent-fg);
                    padding: 2px 8px; border-radius: 10px; font-size: 12px;
                    border: 1px solid #4b2f2a;
                }
                .reason { margin: 12px 0; color: var(--fg); background: var(--bg-panel); padding: 10px 12px; border: 1px solid var(--border); border-radius: 8px; }
                .content { line-height: 1.6; color: var(--fg); }
                blockquote {
                    color: var(--fg);
                    border-left: 4px solid var(--blockquote-border);
                    margin: 8px 0; padding: 6px 12px; background: var(--blockquote-bg);
                    border-radius: 4px;
                }
                code, pre { background: var(--code-bg); border-radius: 6px; color: #e6edf3; }
                pre { padding: 10px; overflow: auto; border: 1px solid var(--border); }
                """));
        }

        private static string BuildHtmlPage(string bodyContent, string extraCss = "")
        {
            return $$"""
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8" />
<meta name="color-scheme" content="dark light" />
<style>
    :root {
        --bg: #0f1117;
        --bg-panel: #111827;
        --fg: #e5e7eb;
        --muted: #9aa4af;
        --border: #1f2937;
        --badge-bg: #1f2937;
        --badge-fg: #cdd6f4;
        --sent-bg: #2b1f1d;
        --sent-fg: #f5e0dc;
        --blockquote-border: #334155;
        --blockquote-bg: #0f172a;
        --code-bg: #0b1220;
        --link: #8ab4f8;
        --link-hover: #a8c7fa;
        --accent: #64748b;
        --superseded-bg: #3b1f1f;
        --active-bg: #1f3b1f;
        --dist-fg: #a3e635;
    }
    html, body { height: 100%; }
    body {
        font-family: "Segoe UI", Arial, sans-serif;
        margin: 0; padding: 16px;
        color: var(--fg);
        background: var(--bg);
    }
    .badge {
        display: inline-block; background: var(--badge-bg); color: var(--badge-fg);
        padding: 2px 8px; border-radius: 10px; margin-right: 8px; font-size: 12px;
        border: 1px solid var(--border);
    }
    .meta { color: var(--muted); margin-bottom: 12px; }
    hr { border: 0; border-top: 1px solid var(--border); margin: 16px 0; }
    h1, h2, h3, h4 { margin-top: 16px; color: var(--fg); }
    a { color: var(--link); text-decoration: none; cursor: pointer; }
    a:hover { color: var(--link-hover); text-decoration: underline; }
    img, video { max-width: 100%; }
    .kv {
        display: grid;
        grid-template-columns: max-content 1fr;
        gap: 6px 12px;
        margin: 12px 0 16px 0;
        background: var(--bg-panel);
        border: 1px solid var(--border);
        border-radius: 8px;
        padding: 10px 12px;
    }
    .k { color: var(--muted); }
    .v { color: var(--fg); }
    .mem-list { list-style: none; padding: 0; margin: 8px 0; }
    .mem-list li {
        background: var(--bg-panel);
        border: 1px solid var(--border);
        border-radius: 6px;
        padding: 8px 12px;
        margin-bottom: 6px;
        cursor: pointer;
        transition: background 0.15s;
    }
    .mem-list li:hover { background: #1a2332; }
    .mem-title { color: var(--link); font-weight: 500; }
    .mem-cat { color: var(--muted); font-size: 12px; margin-left: 8px; }
    .mem-none { color: var(--muted); font-style: italic; }
    {{extraCss}}
</style>
</head>
<body>
{{bodyContent}}
</body>
</html>
""";
        }

        private async void NavigateHtml(string html)
        {
            try
            {
                if (webView.CoreWebView2 == null)
                {
                    await webView.EnsureCoreWebView2Async();
                    try { webView.DefaultBackgroundColor = Color.FromArgb(0x12, 0x14, 0x17); } catch { }
                }
                webView.NavigateToString(html);
            }
            catch
            {
                // WebView2 not available; fail silently
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            LLMEngine.Bot.Brain.ReloadMemories();
            Close();
        }

        private void btDeleteSelected_Click(object sender, EventArgs e)
        {
            // delete the selected memory
            if (listMemories.SelectedItems.Count == 0)
                return;
            var item = listMemories.SelectedItems[0];
            if (item.Tag is not MemoryUnit mem)
                return;
            var confirm = MessageBox.Show(this, $"Are you sure you want to delete the selected memory?\n\nTitle: {mem.Name}\nCategory: {mem.Category}\n\nThis action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;
            try
            {
                LLMEngine.Bot.Brain.Forget(mem);
                _allMemories.Remove(mem);
                ApplyFilterAndRefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to delete memory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEditSelected_Click(object sender, EventArgs e)
        {
            if (listMemories.SelectedItems.Count == 0)
                return;

            var item = listMemories.SelectedItems[0];
            if (item.Tag is not MemoryUnit mem)
                return;

            using var editor = new MemoryEditorForm(mem);
            ThemeManager.ApplyToForm(editor);

            if (editor.ShowDialog(this) == DialogResult.OK && editor.EditedMemory != null)
            {
                try
                {
                    // Update the memory in the brain
                    LLMEngine.Bot.Brain.ReloadMemories();

                    // Refresh the list
                    ApplyFilterAndRefreshList();

                    // Try to reselect the edited item
                    foreach (ListViewItem lvi in listMemories.Items)
                    {
                        if (lvi.Tag is MemoryUnit m && m.Guid == editor.EditedMemory.Guid)
                        {
                            lvi.Selected = true;
                            lvi.EnsureVisible();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to update memory: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btAddNew_Click(object sender, EventArgs e)
        {
            using var editor = new MemoryEditorForm();
            ThemeManager.ApplyToForm(editor);

            if (editor.ShowDialog(this) == DialogResult.OK && editor.EditedMemory != null)
            {
                try
                {
                    // Add the new memory to the brain
                    LLMEngine.Bot.Brain.Memorize(editor.EditedMemory, true);
                    LLMEngine.Bot.Brain.ReloadMemories();

                    // Add to our local list
                    _allMemories.Add(editor.EditedMemory);

                    // Refresh the list
                    ApplyFilterAndRefreshList();

                    // Try to select the new item
                    foreach (ListViewItem lvi in listMemories.Items)
                    {
                        if (lvi.Tag is MemoryUnit m && m.Guid == editor.EditedMemory.Guid)
                        {
                            lvi.Selected = true;
                            lvi.EnsureVisible();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to add memory: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listMemories_DoubleClick(object? sender, EventArgs e)
        {
            // Double-click to edit
            btEditSelected_Click(sender!, e);
        }

        private void MemoryBrowserForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                LLMEngine.Bot.Brain.ReloadMemories();
                Close();
            }
        }

        private void ShowSelectedFact()
        {
            if (listFacts.SelectedItems.Count == 0)
            {
                ShowFactHtml(null);
                return;
            }

            var item = listFacts.SelectedItems[0];
            var fact = item.Tag as ExtractedFact;
            ShowFactHtml(fact);
        }

        private void ShowFactHtml(ExtractedFact? fact)
        {
            if (fact == null)
            {
                FactNavigateHtml(BuildHtmlPage("<i>No fact selected.</i>"));
                return;
            }

            var brain = LLMEngine.Bot?.Brain;
            var factHtml = System.Net.WebUtility.HtmlEncode(fact.Fact);

            var body = new StringBuilder();
            body.Append($"<div class='title'>{factHtml}</div>");

            var statusBadge = fact.Superseded
                ? "<span class='badge-superseded'>Superseded</span>"
                : "<span class='badge-active'>Active</span>";
            body.Append($"<div class='meta'>{statusBadge} <span class='badge'>Refs: {fact.ReferenceCount}</span> <span class='badge'>Score: {fact.GetImportanceScore():F2}</span></div>");

            body.Append("<h2>Details</h2>");
            body.Append("<div class='kv'>");
            body.Append($"<div class='k'>First Seen</div><div class='v'>{System.Net.WebUtility.HtmlEncode(fact.FirstSeen.ToString("yyyy-MM-dd HH:mm"))}</div>");
            body.Append($"<div class='k'>Last Seen</div><div class='v'>{System.Net.WebUtility.HtmlEncode(fact.LastSeen.ToString("yyyy-MM-dd HH:mm"))}</div>");
            body.Append($"<div class='k'>Reference Count</div><div class='v'>{fact.ReferenceCount}</div>");
            body.Append($"<div class='k'>Importance Score</div><div class='v'>{fact.GetImportanceScore():F3}</div>");
            body.Append($"<div class='k'>Has Embedding</div><div class='v'>{(fact.EmbedSummary.Length > 0 ? "Yes" : "No")}</div>");
            body.Append($"<div class='k'>GUID</div><div class='v' style='font-size:11px;'>{System.Net.WebUtility.HtmlEncode(fact.Guid.ToString())}</div>");
            body.Append("</div><hr>");

            // Superseded By section
            if (fact.Superseded && fact.SupersededBy.HasValue)
            {
                body.Append("<h3>Superseded By</h3>");
                var supersedingFact = _allFacts.Find(f => f.Guid == fact.SupersededBy.Value);
                if (supersedingFact != null)
                {
                    var sfText = System.Net.WebUtility.HtmlEncode(supersedingFact.Fact);
                    body.Append($"<ul class='mem-list'><li onclick=\"window.chrome.webview.postMessage('select-fact:{supersedingFact.Guid}')\">");
                    body.Append($"<span class='mem-title'>{sfText}</span>");
                    body.Append($"<span class='mem-cat'>Score: {supersedingFact.GetImportanceScore():F2}</span>");
                    body.Append("</li></ul>");
                }
                else
                {
                    body.Append($"<p class='mem-none'>Fact not found: {System.Net.WebUtility.HtmlEncode(fact.SupersededBy.Value.ToString())}</p>");
                }
                body.Append("<hr>");
            }

            // Facts that this fact supersedes
            var supersededByThis = _allFacts.FindAll(f => f.SupersededBy == fact.Guid);
            if (supersededByThis.Count > 0)
            {
                body.Append("<h3>Supersedes</h3>");
                body.Append("<ul class='mem-list'>");
                foreach (var sf in supersededByThis)
                {
                    var sfText = System.Net.WebUtility.HtmlEncode(sf.Fact);
                    body.Append($"<li onclick=\"window.chrome.webview.postMessage('select-fact:{sf.Guid}')\">");
                    body.Append($"<span class='mem-title'>{sfText}</span>");
                    body.Append($"<span class='mem-cat'>Refs: {sf.ReferenceCount}</span>");
                    body.Append("</li>");
                }
                body.Append("</ul><hr>");
            }

            // Source Memories section
            body.Append("<h3>Source Memories</h3>");
            if (fact.SourceMemories.Count == 0)
            {
                body.Append("<p class='mem-none'>No source memories linked.</p>");
            }
            else
            {
                body.Append("<ul class='mem-list'>");
                foreach (var sourceGuid in fact.SourceMemories)
                {
                    var mem = brain?.GetMemoryByID(sourceGuid);
                    if (mem != null)
                    {
                        var memTitle = System.Net.WebUtility.HtmlEncode(mem.Name ?? "[untitled]");
                        var memCat = System.Net.WebUtility.HtmlEncode(mem.Category.ToString());
                        body.Append($"<li onclick=\"window.chrome.webview.postMessage('open-memory:{sourceGuid}')\">");
                        body.Append($"<span class='mem-title'>{memTitle}</span>");
                        body.Append($"<span class='mem-cat'>{memCat}</span>");
                        body.Append("</li>");
                    }
                    else
                    {
                        body.Append($"<li><span class='mem-none'>Memory not found: {System.Net.WebUtility.HtmlEncode(sourceGuid.ToString())}</span></li>");
                    }
                }
                body.Append("</ul>");
            }

            FactNavigateHtml(BuildHtmlPage(body.ToString(), """
                .title { font-size: 18px; font-weight: 600; margin-bottom: 6px; color: var(--fg); line-height: 1.5; }
                .badge-superseded {
                    display: inline-block; background: var(--superseded-bg); color: #f5a0a0;
                    padding: 2px 8px; border-radius: 10px; font-size: 12px;
                    border: 1px solid #4b2f2a;
                }
                .badge-active {
                    display: inline-block; background: var(--active-bg); color: #a0f5a0;
                    padding: 2px 8px; border-radius: 10px; font-size: 12px;
                    border: 1px solid #2a4b2f;
                }
                """));
        }

        private async void FactNavigateHtml(string html)
        {
            try
            {
                if (webView.CoreWebView2 == null)
                {
                    await webView.EnsureCoreWebView2Async();
                    try { webView.DefaultBackgroundColor = Color.FromArgb(0x12, 0x14, 0x17); } catch { }
                    if (!_FactClickEventSet && webView?.CoreWebView2 is not null)
                    {
                        webView?.CoreWebView2?.WebMessageReceived += CoreWebView2_FactWebMessageReceived;
                        _FactClickEventSet = true;
                    }
                }
                webView?.NavigateToString(html);
            }
            catch
            {
                // WebView2 not available; fail silently
            }
        }

        private void CoreWebView2_FactWebMessageReceived(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var message = e.TryGetWebMessageAsString();
            if (string.IsNullOrEmpty(message))
                return;

            if (message.StartsWith("open-memory:") && Guid.TryParse(message["open-memory:".Length..], out var memGuid))
            {
                var mem = LLMEngine.Bot?.Brain?.GetMemoryByID(memGuid);
                if (mem == null)
                {
                    MessageBox.Show(this, "Memory not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var editor = new MemoryEditorForm(mem);
                ThemeManager.ApplyToForm(editor);

                if (editor.ShowDialog(this) == DialogResult.OK && editor.EditedMemory != null)
                {
                    try
                    {
                        LLMEngine.Bot!.Brain.ReloadMemories();
                        ShowSelectedFact();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Failed to update memory: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (message.StartsWith("select-fact:") && Guid.TryParse(message["select-fact:".Length..], out var factGuid))
            {
                foreach (ListViewItem lvi in listFacts.Items)
                {
                    if (lvi.Tag is ExtractedFact f && f.Guid == factGuid)
                    {
                        lvi.Selected = true;
                        lvi.EnsureVisible();
                        listFacts.Select();
                        break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listFacts.SelectedItems.Count == 0)
                return;

            var item = listFacts.SelectedItems[0];
            if (item.Tag is not ExtractedFact fact)
                return;

            var preview = fact.Fact.Length > 100 ? fact.Fact[..100] + "…" : fact.Fact;
            var confirm = MessageBox.Show(this,
                $"Are you sure you want to delete this extracted fact?\n\n\"{preview}\"\n\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                LLMEngine.Bot.Brain.ExtractedFacts.Remove(fact);
                _allFacts.Remove(fact);
                RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to delete fact: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FactBrowserForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void listFacts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _FactsortColumnIndex)
            {
                _FactsortAscending = !_FactsortAscending;
            }
            else
            {
                _FactsortColumnIndex = e.Column;
                _FactsortAscending = true;

                if (_FactsortColumnIndex == 1 || _FactsortColumnIndex == 2)
                    _FactsortAscending = false;
            }

            ApplyListSorting();
        }

        private void listFacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedFact();
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                var searchstr = edSearch.Text;
                await DoSearch(searchstr);
            }
        }

        private async void btSearch_Click(object sender, EventArgs e)
        {
            var searchstr = edSearch.Text;
            await DoSearch(searchstr);
        }

        private async Task DoSearch(string searchstr)
        {
            if (string.IsNullOrWhiteSpace(searchstr))
                return;

            var baseStr = searchstr;
            if (ck3rdSearch.Checked)
                searchstr = searchstr.ConvertToThirdPerson();

            var found = await LLMEngine.Bot.Brain.Search(searchstr, 100, 1.2f);

            var body = new StringBuilder();
            body.Append("<div class='search-title'>Search Results</div>");
            body.Append($"<div class='search-query'>Query: <strong>{System.Net.WebUtility.HtmlEncode(baseStr)}</strong>");
            if (LLMEngine.Settings.RAGConvertTo3rdPerson)
                body.Append($"&nbsp;&rarr;&nbsp;3rd person: <strong>{System.Net.WebUtility.HtmlEncode(searchstr)}</strong>");
            body.Append("</div><hr>");

            var resultList = found?.ToList() ?? [];
            if (resultList.Count == 0)
            {
                body.Append("<div class='no-results'>No results found.</div>");
            }
            else
            {
                foreach (var item in resultList)
                {
                    var distance = item.Distance.ToString("0.0000");
                    var cat = System.Net.WebUtility.HtmlEncode(item.Memory.Category.ToString());
                    var name = System.Net.WebUtility.HtmlEncode(item.Memory.Name);
                    var content = System.Net.WebUtility.HtmlEncode(LLMEngine.Bot.ReplaceMacros(item.Memory.Content));
                    body.Append("<div class='result-item'>");
                    body.Append($"<div class='result-header'><span class='badge'>{cat}</span><span class='dist'>dist: {distance}</span></div>");
                    body.Append($"<div class='result-name'>{name}</div>");
                    body.Append($"<div class='result-content'>{content}</div>");
                    body.Append("</div>");
                }
            }

            NavigateHtml(BuildHtmlPage(body.ToString(), """
                .search-title { font-size: 18px; font-weight: 600; margin-bottom: 4px; color: var(--fg); }
                .search-query { color: var(--muted); font-size: 13px; margin-bottom: 16px; }
                .dist { display: inline-block; color: var(--dist-fg); font-size: 12px; font-family: monospace; }
                .result-item {
                    background: var(--bg-panel);
                    border: 1px solid var(--border);
                    border-radius: 8px;
                    padding: 10px 12px;
                    margin-bottom: 10px;
                }
                .result-header { margin-bottom: 6px; }
                .result-name { font-weight: 600; color: var(--fg); font-size: 14px; margin-bottom: 4px; }
                .result-content { color: var(--muted); font-size: 13px; line-height: 1.5; white-space: pre-wrap; }
                .no-results { color: var(--muted); font-style: italic; }
                """));
        }
    }

    internal sealed class MemoryListComparer(int columnIndex, bool ascending) : IComparer
    {
        private readonly int _columnIndex = columnIndex;
        private readonly bool _ascending = ascending;

        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem a || y is not ListViewItem b)
                return 0;

            var am = a.Tag as MemoryUnit;
            var bm = b.Tag as MemoryUnit;

            int result;

            switch (_columnIndex)
            {
                case 0: // Title
                    var at = am?.Name ?? a.SubItems[0].Text ?? string.Empty;
                    var bt = bm?.Name ?? b.SubItems[0].Text ?? string.Empty;
                    result = string.Compare(at, bt, StringComparison.OrdinalIgnoreCase);
                    break;

                case 1: // Category
                    var ac = am?.Category.ToString() ?? a.SubItems[1].Text ?? string.Empty;
                    var bc = bm?.Category.ToString() ?? b.SubItems[1].Text ?? string.Empty;
                    result = string.Compare(ac, bc, StringComparison.OrdinalIgnoreCase);
                    break;

                case 2: // Added (Date)
                    var ad = am != null ? MemoryBrowserForm_SafeDate(am) : ParseDateString(a.SubItems.Count > 2 ? a.SubItems[2].Text : "");
                    var bd = bm != null ? MemoryBrowserForm_SafeDate(bm) : ParseDateString(b.SubItems.Count > 2 ? b.SubItems[2].Text : "");
                    result = DateTime.Compare(ad, bd);
                    break;

                default:
                    result = string.Compare(a.SubItems[_columnIndex].Text, b.SubItems[_columnIndex].Text, StringComparison.OrdinalIgnoreCase);
                    break;
            }

            return _ascending ? result : -result;
        }

        private static DateTime ParseDateString(string s)
        {
            if (DateTime.TryParse(s, out var dt))
                return dt;
            return DateTime.MinValue;
        }

        // Access the same SafeDate logic as the form without reflection
        private static DateTime MemoryBrowserForm_SafeDate(MemoryUnit m)
        {
            try
            {
                var prop = m.GetType().GetProperty("Added");
                if (prop?.GetValue(m) is DateTime dt) return dt;
            }
            catch { }
            return DateTime.MinValue;
        }
    }

    internal sealed class FactListComparer(int columnIndex, bool ascending) : IComparer
    {
        private readonly int _columnIndex = columnIndex;
        private readonly bool _ascending = ascending;

        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem a || y is not ListViewItem b)
                return 0;

            var af = a.Tag as ExtractedFact;
            var bf = b.Tag as ExtractedFact;
            var result = _columnIndex switch
            {
                // Fact text
                0 => string.Compare(af?.Fact ?? "", bf?.Fact ?? "", StringComparison.OrdinalIgnoreCase),
                // First Seen
                1 => DateTime.Compare(af?.FirstSeen ?? DateTime.MinValue, bf?.FirstSeen ?? DateTime.MinValue),
                // Last Seen
                2 => DateTime.Compare(af?.LastSeen ?? DateTime.MinValue, bf?.LastSeen ?? DateTime.MinValue),
                // Reference Count
                3 => (af?.ReferenceCount ?? 0).CompareTo(bf?.ReferenceCount ?? 0),
                // Superseded status
                4 => (af?.Superseded ?? false).CompareTo(bf?.Superseded ?? false),
                _ => string.Compare(
                                        a.SubItems.Count > _columnIndex ? a.SubItems[_columnIndex].Text : "",
                                        b.SubItems.Count > _columnIndex ? b.SubItems[_columnIndex].Text : "",
                                        StringComparison.OrdinalIgnoreCase),
            };
            return _ascending ? result : -result;
        }
    }

}