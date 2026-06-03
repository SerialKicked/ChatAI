using LetheAISharp.Agent.Actions;
using LetheAISharp.Agent.Research;
using LetheAISharp.GBNF;
using Markdig;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LetheChat.Forms
{
    public partial class DeepSearchForm : Form
    {
        private CancellationTokenSource? _cts;
        private bool _isRunning = false;

        public DeepSearchForm()
        {
            InitializeComponent();
        }

        // ── UI helpers ────────────────────────────────────────────────────────

        private void SetRunning(bool running)
        {
            _isRunning = running;
            btSearch.Enabled = !running;
            btCancel.Enabled = running;
            edQuery.Enabled = !running;
            lblStatus.Text = running ? "Running…" : "Ready.";
        }

        private void AppendProgress(DeepResearchProgress p)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(() => AppendProgress(p)); return; }

            var item = new ListViewItem(p.Phase.ToString());
            item.SubItems.Add(p.Round > 0 ? p.Round.ToString() : "—");
            item.SubItems.Add(p.Message ?? string.Empty);

            item.ForeColor = p.Phase switch
            {
                DeepResearchPhase.Error or DeepResearchPhase.Warning    => Color.Goldenrod,
                DeepResearchPhase.Completed                             => Color.LightGreen,
                DeepResearchPhase.Searching or DeepResearchPhase.Reading => Color.LightSkyBlue,
                DeepResearchPhase.Analyzing or DeepResearchPhase.Writing => Color.Plum,
                _                                                       => Color.FromArgb(200, 200, 200),
            };

            listProgress.Items.Add(item);
            listProgress.EnsureVisible(listProgress.Items.Count - 1);
            lblStatus.Text = $"[{p.Phase}] {p.Message}";
        }

        private void PopulateResults(DeepResearchResult result)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(() => PopulateResults(result)); return; }

            NavigateReport(webFinalReport, result.FinalReport);
            NavigateReport(webEvolvingReport, result.EvolvingReport);
            txtPlan.Text = result.ResearchPlan?.ToPlan() ?? string.Empty;

            listSources.Items.Clear();
            foreach (var f in result.Findings)
            {
                var item = new ListViewItem(f.Title);
                item.SubItems.Add(f.Url);
                item.SubItems.Add(f.Summary);
                item.ToolTipText = f.Evidence;
                listSources.Items.Add(item);
            }

            tabResults.SelectedTab = tabFinal;

            var stats = $"Done — {result.CompletedRounds} rounds, {result.Findings.Count} findings, {result.UrlsVisited.Count} URLs · {result.Duration:m\\:ss}";
            lblStatus.Text = result.Success ? stats : $"Failed: {result.Error}";
            lblStatus.ForeColor = result.Success ? Color.LightGreen : Color.IndianRed;
        }

        // ── Search execution ──────────────────────────────────────────────────

        private async void RunSearch()
        {
            var question = edQuery.Text.Trim();
            if (string.IsNullOrWhiteSpace(question))
            {
                lblStatus.Text = "Please enter a query.";
                return;
            }

            listProgress.Items.Clear();
            listSources.Items.Clear();
            NavigateReport(webFinalReport, "");
            NavigateReport(webEvolvingReport, "");
            txtPlan.Clear();
            lblStatus.ForeColor = Color.LightSkyBlue;

            _cts = new CancellationTokenSource();
            SetRunning(true);

            try
            {
                var action = new DeepSearchAction(progress: AppendProgress);
                var result = await action.Execute(question, _cts.Token).ConfigureAwait(false);
                PopulateResults(result);
            }
            catch (OperationCanceledException)
            {
                if (!IsDisposed)
                    BeginInvoke(() =>
                    {
                        lblStatus.Text = "Cancelled.";
                        lblStatus.ForeColor = Color.Goldenrod;
                    });
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    BeginInvoke(() =>
                    {
                        lblStatus.Text = $"Error: {ex.Message}";
                        lblStatus.ForeColor = Color.IndianRed;
                    });
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
                if (!IsDisposed)
                    BeginInvoke(() => SetRunning(false));
            }
        }

        // ── HTML report helpers ───────────────────────────────────────────────

        private static readonly MarkdownPipeline MarkdownPipeline =
            new MarkdownPipelineBuilder()
                .UseSoftlineBreakAsHardlineBreak()
                .UseAdvancedExtensions()
                .UseAutoLinks()
                .Build();

        private static string BuildReportHtml(string markdown)
        {
            var body = string.IsNullOrWhiteSpace(markdown)
                ? "<p class='muted'>No content yet.</p>"
                : Markdown.ToHtml(markdown, MarkdownPipeline);

            return """
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
        --blockquote-border: #334155;
        --blockquote-bg: #0f172a;
        --code-bg: #0b1220;
        --link: #8ab4f8;
        --link-hover: #a8c7fa;
    }
    html, body { height: 100%; margin: 0; }
    body {
        font-family: "Segoe UI", Arial, sans-serif;
        font-size: 14px;
        padding: 16px 20px;
        color: var(--fg);
        background: var(--bg);
        line-height: 1.65;
    }
    h1, h2, h3, h4, h5, h6 { color: var(--fg); margin-top: 20px; }
    hr { border: 0; border-top: 1px solid var(--border); margin: 16px 0; }
    a { color: var(--link); text-decoration: none; }
    a:hover { color: var(--link-hover); text-decoration: underline; }
    p { margin: 8px 0; }
    ul, ol { padding-left: 24px; }
    blockquote {
        border-left: 3px solid var(--blockquote-border);
        background: var(--blockquote-bg);
        margin: 8px 0; padding: 6px 12px;
        color: var(--muted);
    }
    code {
        background: var(--code-bg); padding: 1px 5px;
        border-radius: 4px; font-size: 13px;
    }
    pre {
        background: var(--code-bg); padding: 10px 14px;
        border-radius: 6px; overflow-x: auto;
    }
    pre code { background: none; padding: 0; }
    .muted { color: var(--muted); font-style: italic; }
    strong { color: #f0f0f0; }
</style>
</head>
<body>
""" + body + """
</body>
</html>
""";
        }

        private async void NavigateReport(Microsoft.Web.WebView2.WinForms.WebView2 view, string markdown)
        {
            try
            {
                if (view.CoreWebView2 == null)
                {
                    await view.EnsureCoreWebView2Async();
                    try { view.DefaultBackgroundColor = Color.FromArgb(0x0f, 0x11, 0x17); } catch { }
                    view.CoreWebView2!.Settings.AreDevToolsEnabled = false;
                    view.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                }
                view.NavigateToString(BuildReportHtml(markdown));
            }
            catch
            {
                // WebView2 not available; fail silently
            }
        }

        // ── Event handlers ────────────────────────────────────────────────────

        private void btSearch_Click(object sender, EventArgs e) => RunSearch();

        private void btCancel_Click(object sender, EventArgs e) => _cts?.Cancel();

        private void btClose_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            Close();
        }

        private void edQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !_isRunning)
            {
                e.SuppressKeyPress = true;
                RunSearch();
            }
        }

        private void DeepSearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (_isRunning)
                    _cts?.Cancel();
                else
                    Close();
            }
        }
    }
}
