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
using WaifuAI.Controls;

namespace WaifuAI.src.forms
{
    public partial class MemoryBrowserForm : Form
    {
        private readonly List<MemoryUnit> _allMemories = [];
        private readonly List<MemoryUnit> _filteredMemories = [];

        // Sorting state (default: sort by Added desc)
        private int _sortColumnIndex = 2;
        private bool _sortAscending = false;

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
            Text = "Memory Browser";
            KeyPreview = true;
            KeyDown += MemoryBrowserForm_KeyDown;

            // Set WebView background to match dark theme (overrides Designer's white)
            try { webView.DefaultBackgroundColor = Color.FromArgb(0x12, 0x14, 0x17); } catch { }

            LoadFromActiveBot();
            PopulateCategories();
            ApplyFilterAndRefreshList();
        }

        public static void ShowForActiveBot(IWin32Window? owner = null)
        {
            using var f = new MemoryBrowserForm();
            ThemeManager.ApplyToForm(f);
            f.ShowDialog(owner);
        }

        private void MemoryBrowserForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
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
                ShowSelectedMemory();
            }
            else
            {
                ShowMemoryHtml(null);
            }
        }

        private void ApplyListSorting()
        {
            listMemories.ListViewItemSorter = new MemoryListComparer(_sortColumnIndex, _sortAscending);
            listMemories.Sort();
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
                NavigateHtml("<html><body style='font-family:Segoe UI; color:#ddd; background:#0f1117;'><i>No memory selected.</i></body></html>");
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

            var html = new StringBuilder();
            html.Append("""
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8" />
<meta name="color-scheme" content="dark light" />
<style>
    :root {
        --bg: #0f1117;          /* panel background */
        --bg-panel: #111827;    /* slightly lighter than bg for blocks */
        --fg: #e5e7eb;          /* primary text */
        --muted: #9aa4af;       /* secondary text */
        --border: #1f2937;      /* borders/separators */
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
    }

    html, body { height: 100%; }
    body {
        font-family: "Segoe UI", Arial, sans-serif;
        margin: 0; padding: 16px;
        color: var(--fg);
        background: var(--bg);
    }
    .title { font-size: 20px; font-weight: 600; margin-bottom: 6px; color: var(--fg); }
    .meta { color: var(--muted); margin-bottom: 12px; }
    .badge {
        display: inline-block; background: var(--badge-bg); color: var(--badge-fg);
        padding: 2px 8px; border-radius: 10px; margin-right: 8px; font-size: 12px;
        border: 1px solid var(--border);
    }
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
    hr { border: 0; border-top: 1px solid var(--border); margin: 16px 0; }
    h1, h2, h3, h4 { margin-top: 16px; color: var(--fg); }
    a { color: var(--link); text-decoration: none; }
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
</style>
</head>
<body>
""");

            html.Append($"<div class='title'>{title}</div>");
            html.Append($"<div class='meta'><span class='badge'>{cat}</span><span class='sentiment'>Sentiment: {System.Net.WebUtility.HtmlEncode(sentiment)}</span></div>");
            if (!string.IsNullOrEmpty(reason))
                html.Append(reason);

            // Details section (##)
            html.Append("<h2>Details</h2>");
            html.Append("<div class='kv'>");
            html.Append($"<div class='k'>Added</div><div class='v'>{System.Net.WebUtility.HtmlEncode(addedStr)}</div>");
            html.Append($"<div class='k'>Priority</div><div class='v'>{System.Net.WebUtility.HtmlEncode(priorityStr)}</div>");
            html.Append($"<div class='k'>Insertion</div><div class='v'>{System.Net.WebUtility.HtmlEncode(insertionStr)}</div>");
            html.Append($"<div class='k'>Last trigger</div><div class='v'>{System.Net.WebUtility.HtmlEncode(lastTrigStr)}</div>");
            html.Append("</div><hr>");

            html.Append($"<div class='content'>{bodyHtml}</div>");
            html.Append("</body></html>");

            NavigateHtml(html.ToString());
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
}