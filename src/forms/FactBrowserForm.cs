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

namespace LetheChat.src.forms
{
    public partial class FactBrowserForm : Form
    {
        private readonly List<ExtractedFact> _allFacts = [];
        private readonly List<ExtractedFact> _filteredFacts = [];

        private int _sortColumnIndex = 1;
        private bool _sortAscending = false;

        private static readonly MarkdownPipeline MarkdownPipeline =
            new MarkdownPipelineBuilder()
                .UseSoftlineBreakAsHardlineBreak()
                .UseAdvancedExtensions()
                .UseEmojiAndSmiley()
                .UseAutoLinks()
                .Build();

        public FactBrowserForm()
        {
            InitializeComponent();
            Text = "Extracted Facts Browser";
            KeyPreview = true;
            KeyDown += FactBrowserForm_KeyDown;

            try { webView.DefaultBackgroundColor = Color.FromArgb(0x12, 0x14, 0x17); } catch { }

            LoadFromActiveBot();
            RefreshListView();
        }

        public static void ShowForActiveBot(IWin32Window? owner = null)
        {
            using var f = new FactBrowserForm();
            ThemeManager.ApplyToForm(f);
            f.ShowDialog(owner);
        }

        private void FactBrowserForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void LoadFromActiveBot()
        {
            _allFacts.Clear();

            var bot = LLMEngine.Bot;
            if (bot == null)
            {
                MessageBox.Show("No active bot found.");
                Close();
                return;
            }

            _allFacts.AddRange(bot.Brain.ExtractedFacts);
        }

        private void RefreshListView()
        {
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
                ShowSelectedFact();
            }
            else
            {
                ShowFactHtml(null);
            }
        }

        private void ApplyListSorting()
        {
            listFacts.ListViewItemSorter = new FactListComparer(_sortColumnIndex, _sortAscending);
            listFacts.Sort();
        }

        private void listFacts_ColumnClick(object? sender, ColumnClickEventArgs e)
        {
            if (e.Column == _sortColumnIndex)
            {
                _sortAscending = !_sortAscending;
            }
            else
            {
                _sortColumnIndex = e.Column;
                _sortAscending = true;

                if (_sortColumnIndex == 1 || _sortColumnIndex == 2)
                    _sortAscending = false;
            }

            ApplyListSorting();
        }

        private void listFacts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ShowSelectedFact();
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
                NavigateHtml("<html><body style='font-family:Segoe UI; color:#ddd; background:#0f1117;'><i>No fact selected.</i></body></html>");
                return;
            }

            var brain = LLMEngine.Bot?.Brain;
            var factHtml = System.Net.WebUtility.HtmlEncode(fact.Fact);

            var html = new StringBuilder();
            html.Append("""
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
        --code-bg: #0b1220;
        --link: #8ab4f8;
        --link-hover: #a8c7fa;
        --superseded-bg: #3b1f1f;
        --active-bg: #1f3b1f;
    }

    html, body { height: 100%; }
    body {
        font-family: "Segoe UI", Arial, sans-serif;
        margin: 0; padding: 16px;
        color: var(--fg);
        background: var(--bg);
    }
    .title { font-size: 18px; font-weight: 600; margin-bottom: 6px; color: var(--fg); line-height: 1.5; }
    .meta { color: var(--muted); margin-bottom: 12px; }
    .badge {
        display: inline-block; background: var(--badge-bg); color: var(--badge-fg);
        padding: 2px 8px; border-radius: 10px; margin-right: 8px; font-size: 12px;
        border: 1px solid var(--border);
    }
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
    hr { border: 0; border-top: 1px solid var(--border); margin: 16px 0; }
    h2, h3 { margin-top: 16px; color: var(--fg); }

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

    .mem-list {
        list-style: none;
        padding: 0;
        margin: 8px 0;
    }
    .mem-list li {
        background: var(--bg-panel);
        border: 1px solid var(--border);
        border-radius: 6px;
        padding: 8px 12px;
        margin-bottom: 6px;
        cursor: pointer;
        transition: background 0.15s;
    }
    .mem-list li:hover {
        background: #1a2332;
    }
    .mem-title { color: var(--link); font-weight: 500; }
    .mem-cat { color: var(--muted); font-size: 12px; margin-left: 8px; }
    .mem-none { color: var(--muted); font-style: italic; }

    a { color: var(--link); text-decoration: none; cursor: pointer; }
    a:hover { color: var(--link-hover); text-decoration: underline; }
</style>
</head>
<body>
""");

            html.Append($"<div class='title'>{factHtml}</div>");

            var statusBadge = fact.Superseded
                ? "<span class='badge-superseded'>Superseded</span>"
                : "<span class='badge-active'>Active</span>";
            html.Append($"<div class='meta'>{statusBadge} <span class='badge'>Refs: {fact.ReferenceCount}</span> <span class='badge'>Score: {fact.GetImportanceScore():F2}</span></div>");

            html.Append("<h2>Details</h2>");
            html.Append("<div class='kv'>");
            html.Append($"<div class='k'>First Seen</div><div class='v'>{System.Net.WebUtility.HtmlEncode(fact.FirstSeen.ToString("yyyy-MM-dd HH:mm"))}</div>");
            html.Append($"<div class='k'>Last Seen</div><div class='v'>{System.Net.WebUtility.HtmlEncode(fact.LastSeen.ToString("yyyy-MM-dd HH:mm"))}</div>");
            html.Append($"<div class='k'>Reference Count</div><div class='v'>{fact.ReferenceCount}</div>");
            html.Append($"<div class='k'>Importance Score</div><div class='v'>{fact.GetImportanceScore():F3}</div>");
            html.Append($"<div class='k'>Has Embedding</div><div class='v'>{(fact.EmbedSummary.Length > 0 ? "Yes" : "No")}</div>");
            html.Append($"<div class='k'>GUID</div><div class='v' style='font-size:11px;'>{System.Net.WebUtility.HtmlEncode(fact.Guid.ToString())}</div>");
            html.Append("</div><hr>");

            // Superseded By section
            if (fact.Superseded && fact.SupersededBy.HasValue)
            {
                html.Append("<h3>Superseded By</h3>");
                var supersedingFact = _allFacts.Find(f => f.Guid == fact.SupersededBy.Value);
                if (supersedingFact != null)
                {
                    var sfText = System.Net.WebUtility.HtmlEncode(supersedingFact.Fact);
                    html.Append($"<ul class='mem-list'><li onclick=\"window.chrome.webview.postMessage('select-fact:{supersedingFact.Guid}')\">");
                    html.Append($"<span class='mem-title'>{sfText}</span>");
                    html.Append($"<span class='mem-cat'>Score: {supersedingFact.GetImportanceScore():F2}</span>");
                    html.Append("</li></ul>");
                }
                else
                {
                    html.Append($"<p class='mem-none'>Fact not found: {System.Net.WebUtility.HtmlEncode(fact.SupersededBy.Value.ToString())}</p>");
                }
                html.Append("<hr>");
            }

            // Facts that this fact supersedes
            var supersededByThis = _allFacts.FindAll(f => f.SupersededBy == fact.Guid);
            if (supersededByThis.Count > 0)
            {
                html.Append("<h3>Supersedes</h3>");
                html.Append("<ul class='mem-list'>");
                foreach (var sf in supersededByThis)
                {
                    var sfText = System.Net.WebUtility.HtmlEncode(sf.Fact);
                    html.Append($"<li onclick=\"window.chrome.webview.postMessage('select-fact:{sf.Guid}')\">");
                    html.Append($"<span class='mem-title'>{sfText}</span>");
                    html.Append($"<span class='mem-cat'>Refs: {sf.ReferenceCount}</span>");
                    html.Append("</li>");
                }
                html.Append("</ul><hr>");
            }

            // Source Memories section
            html.Append("<h3>Source Memories</h3>");
            if (fact.SourceMemories.Count == 0)
            {
                html.Append("<p class='mem-none'>No source memories linked.</p>");
            }
            else
            {
                html.Append("<ul class='mem-list'>");
                foreach (var sourceGuid in fact.SourceMemories)
                {
                    var mem = brain?.GetMemoryByID(sourceGuid);
                    if (mem != null)
                    {
                        var memTitle = System.Net.WebUtility.HtmlEncode(mem.Name ?? "[untitled]");
                        var memCat = System.Net.WebUtility.HtmlEncode(mem.Category.ToString());
                        html.Append($"<li onclick=\"window.chrome.webview.postMessage('open-memory:{sourceGuid}')\">");
                        html.Append($"<span class='mem-title'>{memTitle}</span>");
                        html.Append($"<span class='mem-cat'>{memCat}</span>");
                        html.Append("</li>");
                    }
                    else
                    {
                        html.Append($"<li><span class='mem-none'>Memory not found: {System.Net.WebUtility.HtmlEncode(sourceGuid.ToString())}</span></li>");
                    }
                }
                html.Append("</ul>");
            }

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
                    webView?.CoreWebView2?.WebMessageReceived += CoreWebView2_WebMessageReceived;
                }
                webView?.NavigateToString(html);
            }
            catch
            {
                // WebView2 not available; fail silently
            }
        }

        private void CoreWebView2_WebMessageReceived(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
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

        private void btClose_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btDeleteSelected_Click(object? sender, EventArgs e)
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
