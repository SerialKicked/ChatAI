using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Markdig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaifuAI.src.forms
{
    public partial class MemoryBrowserForm : Form
    {
        private readonly List<MemoryUnit> _allMemories = new();
        private readonly List<MemoryUnit> _filteredMemories = new();

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

            LoadFromActiveBot();
            PopulateCategories();
            ApplyFilterAndRefreshList();
        }

        public static void ShowForActiveBot(IWin32Window? owner = null)
        {
            using var f = new MemoryBrowserForm();
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
            foreach (var value in Enum.GetValues(typeof(MemoryType)).Cast<MemoryType>())
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
            foreach (var value in Enum.GetValues(typeof(MemoryType)))
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
                NavigateHtml("<html><body style='font-family:Segoe UI; color:#333;'><i>No memory selected.</i></body></html>");
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
<style>
    body { font-family: "Segoe UI", Arial, sans-serif; margin: 0; padding: 16px; color: #222; background: #fff; }
    .title { font-size: 20px; font-weight: 600; margin-bottom: 4px; }
    .meta { color: #666; margin-bottom: 12px; }
    .badge { display: inline-block; background: #eef1f7; color: #334; padding: 2px 8px; border-radius: 10px; margin-right: 8px; font-size: 12px; }
    .sentiment { display: inline-block; background: #f7f1ee; color: #433; padding: 2px 8px; border-radius: 10px; font-size: 12px; }
    .reason { margin: 12px 0; color: #444; }
    .content { line-height: 1.5; }
    blockquote { color: #555; border-left: 4px solid #ddd; margin: 8px 0; padding: 4px 12px; background: #fafafa; }
    code, pre { background: #f6f8fa; border-radius: 4px; }
    pre { padding: 8px; overflow: auto; }
    h1, h2, h3, h4 { margin-top: 16px; }
    img, video { max-width: 100%; }
    .kv { display: grid; grid-template-columns: max-content 1fr; gap: 6px 12px; margin: 12px 0 16px 0; }
    .k { color: #666; }
    .v { color: #222; }
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
    }

    internal sealed class MemoryListComparer : IComparer
    {
        private readonly int _columnIndex;
        private readonly bool _ascending;

        public MemoryListComparer(int columnIndex, bool ascending)
        {
            _columnIndex = columnIndex;
            _ascending = ascending;
        }

        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem a || y is not ListViewItem b)
                return 0;

            var am = a.Tag as MemoryUnit;
            var bm = b.Tag as MemoryUnit;

            int result = 0;

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