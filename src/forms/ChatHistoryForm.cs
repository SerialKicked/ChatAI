using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.Agent.Actions;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Markdig;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using WaifuAI.AgentPlugins;
using WaifuAI.Files;
using WaifuAI.GBNF;
using WaifuAI.src.forms;

namespace WaifuAI.src.forms
{
    public partial class ChatHistoryForm : Form
    {
        private ChatSession? _selectedSession = null;
        private bool _isinitloading = true;
        
        // Sorting state
        private int _sortColumnIndex = 1; // Default: sort by date (column 1)
        private bool _sortAscending = false; // Default: newest first

        public static MarkdownPipeline CustomMarkDownPipeline { get; } = new MarkdownPipelineBuilder()
            .UseSoftlineBreakAsHardlineBreak().UseAdvancedExtensions()
            .UseEmojiAndSmiley()
            .UseAutoLinks()
            .Use(new QuoteColorExtension())
            .Build();

        public ChatHistoryForm()
        {
            InitializeComponent();
            SetupListSessionContextMenu();
            
            // Setup column click sorting
            listSession.ColumnClick += ListSession_ColumnClick;
            
            _isinitloading = false;
        }

        private void SetupListSessionContextMenu()
        {
            // Create context menu
            ContextMenuStrip contextMenu = new();

            // Add menu items
            ToolStripMenuItem switchSessionItem = new("Set Session As Active");
            switchSessionItem.Click += (sender, e) => SwitchToSelectedSession();

            ToolStripMenuItem insertSessionItem = new("Insert New Session Below");
            insertSessionItem.Click += async (sender, e) => await InsertSessionAfterSelected();

            ToolStripMenuItem deleteSessionItem = new("Delete Selected Session");
            deleteSessionItem.Click += (sender, e) => DeleteSelectedSession();

            ToolStripMenuItem CheckRPItem = new("Recheck if RP");
            CheckRPItem.Click += async (sender, e) => await RefreshRPInfo();

            // Add items to menu
            contextMenu.Items.Add(switchSessionItem);
            contextMenu.Items.Add(insertSessionItem);
            contextMenu.Items.Add(deleteSessionItem);
            contextMenu.Items.Add(CheckRPItem);

            // Attach opening event to control items' visibility based on selection
            contextMenu.Opening += (sender, e) =>
            {
                bool hasSelection = listSession.SelectedItems.Count > 0;
                switchSessionItem.Enabled = hasSelection;
                insertSessionItem.Enabled = hasSelection;
                deleteSessionItem.Enabled = hasSelection;
                CheckRPItem.Enabled = hasSelection;

                // Cancel opening if no items selected
                if (!hasSelection)
                    e.Cancel = true;
            };

            // Assign menu to ListView
            listSession.ContextMenuStrip = contextMenu;
        }

        private void SwitchToSelectedSession()
        {
            if (_selectedSession == null)
                return;

            LLMEngine.Bot.History.CurrentSessionID = LLMEngine.Bot.History.Sessions.IndexOf(_selectedSession);
            LoadChatHistoryTab();
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task InsertSessionAfterSelected()
        {
            if (_selectedSession == null)
                return;

            LLMEngine.Bot.History.CurrentSessionID = LLMEngine.Bot.History.Sessions.IndexOf(_selectedSession);
            var id = LLMEngine.Bot.History.CurrentSessionID;
            if (id == LLMEngine.Bot.History.Sessions.Count - 1)
            {
                LLMEngine.Bot.History.Sessions.Add(new ChatSession());
            }
            else
            {
                LLMEngine.Bot.History.Sessions.Insert(id + 1, new ChatSession());
            }
            LLMEngine.Bot.History.CurrentSessionID++;
            _selectedSession = LLMEngine.History.CurrentSession;
            await LLMEngine.History.StartNewChatSession(true, false);
            LoadChatHistoryTab();
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task RefreshRPInfo()
        {
            if (_selectedSession == null)
                return;
            _selectedSession.MetaData.IsRoleplaySession = await _selectedSession.IsRoleplay();
            DisplaySessionDetails(_selectedSession);
        }

        private void DeleteSelectedSession()
        {
            if (_selectedSession == null)
                return;

            if (LLMEngine.History.Sessions.Count <= 1)
            {
                // If only one session, delete all history instead
                if (MessageBox.Show("This will delete all chat history with this character permanently. Are you sure?", "Delete All History?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LLMEngine.History.DeleteAll(true);
                    var message = new SingleMessage(AuthorRole.Assistant, LLMEngine.Bot.GetWelcomeLine(LLMEngine.User.Name));
                    LLMEngine.History.LogMessage(message);
                    LoadChatHistoryTab();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                return;
            }

            if (MessageBox.Show("This will delete the selected session permanently. Are you sure?",
                              "Delete Session?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var needchangesession = LLMEngine.History.CurrentSession == _selectedSession;
                LLMEngine.History.Sessions.Remove(_selectedSession);
                if (needchangesession)
                {
                    LLMEngine.History.CurrentSessionID = LLMEngine.History.Sessions.Count - 1;
                }
            }

            _selectedSession = LLMEngine.History.CurrentSession;
            DisplaySessionDetails(_selectedSession);
            LoadChatHistoryTab();
            DialogResult = DialogResult.OK;
            Close();
        }

        public void LoadChatHistoryTab()
        {
            listSession.Items.Clear();
            if (LLMEngine.History.Sessions.Count == 0)
                return;
            foreach (var session in LLMEngine.History.Sessions)
            {
                var item = new ListViewItem([session.Name, session.StartTime.ToString("g")])
                {
                    Tag = session
                };
                if (LLMEngine.History.Sessions.IndexOf(session) == LLMEngine.History.CurrentSessionID)
                {
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }
                if (session.Sticky)
                {
                    item.ForeColor = Color.Red;
                }
                listSession.Items.Add(item);
            }
            
            // Apply current sorting after loading
            ApplyListSorting();
        }

        private void ListSession_ColumnClick(object? sender, ColumnClickEventArgs e)
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

                // Prefer descending by date when picking date column for the first time
                if (_sortColumnIndex == 1)
                    _sortAscending = false;
            }

            ApplyListSorting();
        }

        private void ApplyListSorting()
        {
            listSession.ListViewItemSorter = new SessionListComparer(_sortColumnIndex, _sortAscending);
            listSession.Sort();
        }

        private void listSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSession.SelectedItems.Count <= 0)
                return;
            var selectedItem = listSession.SelectedItems[0];
            _selectedSession = (ChatSession)selectedItem.Tag!;
            DisplaySessionDetails(_selectedSession);
        }

        private async void DisplaySessionDetails(ChatSession session)
        {
            var sv = _isinitloading;
            _isinitloading = true;
            ed_sessiontitle.Text = session.Name;
            ed_sessioninfo.Text = session.Content.ToWinFormat();
            lbl_sessiondata.Text = session.StartTime.ToString("g") + " - " + session.EndTime.ToString("g") + " - " + session.Messages.Count + " messages";

            ed_hist_kw1.Text = string.Join(",", session.KeyWordsMain);
            ed_hist_kw2.Text = string.Join(",", session.KeyWordsSecondary);
            cb_hist_kwlink.SelectedIndex = (int)session.WordLink;
            ck_hist_casesensitive.Checked = session.CaseSensitive;
            ck_hist_kw.Checked = session.Enabled;
            ck_hist_sticky.Checked = session.Sticky;
            ck_hist_isrp.Checked = session.MetaData.IsRoleplaySession;
            _isinitloading = sv;

            if (web_sessioncontent.CoreWebView2 == null)
            {
                await web_sessioncontent.EnsureCoreWebView2Async();
                web_sessioncontent.CoreWebView2!.Settings.AreDevToolsEnabled = false;
                web_sessioncontent.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                web_sessioncontent.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "appassets.test",
                    Path.Combine(AppContext.BaseDirectory, "data"),
                    CoreWebView2HostResourceAccessKind.Allow);
            }

            // Build the HTML content
            var htmlContent = BuildSessionHtml(session);
            web_sessioncontent.NavigateToString(htmlContent);
        }

        private string BuildSessionHtml(ChatSession session)
        {
            var sb = new StringBuilder();

            // Add session metadata section
            sb.Append("<div class='session-metadata'>");
            sb.Append($"<h2>{session.Name}</h2>");
            sb.Append($"<p><strong>Summary:</strong> {session.Content}</p>");
            
            if (session.MetaData.Keywords.Count > 0)
            {
                sb.Append($"<p><strong>Keywords:</strong> {string.Join(", ", session.MetaData.Keywords)}</p>");
            }
            
            if (session.MetaData.FutureGoals.Count > 0)
            {
                sb.Append("<p><strong>Goals:</strong></p><ul>");
                foreach (var goal in session.MetaData.FutureGoals)
                {
                    sb.Append($"<li>{goal}</li>");
                }
                sb.Append("</ul>");
            }
            if (!string.IsNullOrWhiteSpace(session.Scenario))
                sb.Append($"<p><strong>Scenario:</strong> {session.Scenario}</p>");
            sb.Append($"<p><strong>Relevance:</strong> {session.MetaData.Relevance}</p>");
            
            if (session.Sentiments.Count > 0)
            {
                sb.Append("<p><strong>Sentiments:</strong></p><ul>");
                foreach (var sentiment in session.Sentiments)
                {
                    sb.Append($"<li>{sentiment.Label}: {sentiment.Probability:F2}</li>");
                }
                sb.Append("</ul>");
            }
            
            sb.Append("</div>");

            // Add divider
            sb.Append("<hr class='metadata-divider' />");

            // Add chat messages
            sb.Append("<div id='chatContainer'>");
            foreach (var msg in session.Messages)
            {
                if (!msg.Hidden || Program.Settings.ShowHiddenMessages)
                {
                    sb.Append(RenderChatMessage(msg));
                }
            }
            sb.Append("</div>");

            return InjectSessionCSS(sb.ToString());
        }

        private static string RenderChatMessage(SingleMessage message)
        {
            string img = "gears.png";
            switch (message.Role)
            {
                case AuthorRole.User:
                    img = (message.User as ICharacter)?.Icon ?? MainForm.User?.Icon ?? "gears.png";
                    break;
                case AuthorRole.Assistant:
                    img = (message.Bot as ICharacter)?.Icon ?? MainForm.Bot?.Icon ?? "gears.png";
                    break;
            }

            var html = Markdown.ToHtml(ChatRender.GetMessagePrefix(message) + message.Message, CustomMarkDownPipeline);
            
            return $@"
        <div class='chat-message' data-message-guid='{message.Guid}'>
            <div class='portrait'>
                <img src='https://appassets.test/img/{img}' alt='Portrait' width='60'>
            </div>
            <div class='message-content'>
                <div class='message-raw'>
                    {html}
                </div>
            </div>
        </div>";
        }

        private static string InjectSessionCSS(string htmlContent)
        {
            string css = $@"
    <style>
        body {{ 
            max-height: 100%;
            overflow-y: auto;
            overflow-x: hidden;
            padding: 16px;
            font-size: {Program.Settings.FontSize}px;
            width: 100%;
            box-sizing: border-box;
            background-image: url('https://appassets.test/background/{Program.Settings.BackgroundFile}');
            background-size: cover;
            background-attachment: fixed;
            background-position: center;
            background-repeat: no-repeat;
        }}
        em {{ color: yellow; }}
        strong {{ color: Tomato }}
        a {{ color: gold }}
        h1 {{ font-size: 1.3em; }}
        h2 {{ font-size: 1.25em; }}
        h3 {{ font-size: 1.2em; }}
        h4 {{ font-size: 1.15em; }}
        h5 {{ font-size: 1.1em; }}

        .session-metadata {{
            background-color: rgba(0, 0, 0, 0.85);
            color: rgb(200, 200, 200);
            padding: 15px;
            margin-bottom: 10px;
            border: 1px solid gray;
            border-radius: 4px;
        }}

        .session-metadata h2 {{
            margin-top: 0;
            color: rgb(220, 220, 220);
        }}

        .session-metadata ul {{
            margin: 5px 0;
            padding-left: 20px;
        }}

        .metadata-divider {{
            border: none;
            border-top: 2px solid rgba(128, 128, 128, 0.5);
            margin: 20px 0;
        }}

        .chat-message {{
            display: flex;
            align-items: flex-start;
            margin-bottom: 10px;
            border: 1px solid gray;
            background-color: rgba(0, 0, 0, 0.75);
            color: rgb(200, 200, 200);
        }}

        .portrait {{
            flex: 0 0 70px;
            padding: 10px;
            margin-right: 0px;
        }}

        .message-content {{
            flex: 1;
            word-wrap: break-word;
            padding-right: 10px;
        }}
    </style>";

            return $"<html><head>{css}</head><body>{htmlContent}</body></html>";
        }

        private void UpdateHistoryEntryEvent(object sender, EventArgs e)
        {
            if (_isinitloading || _selectedSession == null)
                return;
            if (!string.IsNullOrWhiteSpace(ed_hist_kw1.Text))
            {
                _selectedSession.KeyWordsMain = ed_hist_kw1.Text.Split(',')?.ToList() ?? [];
            }
            else
            {
                _selectedSession.KeyWordsMain = [];
            }
            if (!string.IsNullOrWhiteSpace(ed_hist_kw2.Text))
            {
                _selectedSession.KeyWordsSecondary = ed_hist_kw2.Text.Split(',')?.ToList() ?? [];
            }
            else
            {
                _selectedSession.KeyWordsSecondary = [];
            }
            _selectedSession.WordLink = (KeyWordLink)cb_hist_kwlink.SelectedIndex;
            _selectedSession.CaseSensitive = ck_hist_casesensitive.Checked;
            _selectedSession.Enabled = ck_hist_kw.Checked;
            _selectedSession.Sticky = ck_hist_sticky.Checked;
            _selectedSession.MetaData.IsRoleplaySession = ck_hist_isrp.Checked;
        }

        private async void bt_sessionrefresh_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
            this.Enabled = false;
            using var loadingForm = new LoadingForm() { Owner = this, StartPosition = FormStartPosition.Manual };
            loadingForm.CenterToParent();
            loadingForm.Show();
            loadingForm.BringToFront();
            loadingForm.Refresh();
            try
            {
                loadingForm.SetMessage("Updating session summary and meta-data. Depending on your computer, the model, and the context window, it might take a while.");
                loadingForm.SetProgress(5);
                LLMEngine.OnQuickInferenceEnded += (s, e) =>
                {
                    loadingForm.AddProgress(20);
                };
                _selectedSession.StartTime = _selectedSession.Messages.First().Date;
                // if the first message has a default date, try to find a message with a valid date
                if (_selectedSession.StartTime == default)
                {
                    foreach (var item in _selectedSession.Messages)
                    {
                        if (item.Date != default)
                        {
                            _selectedSession.StartTime = item.Date;
                            break;
                        }
                    }
                }
                await _selectedSession.UpdateSession();
                loadingForm.SetMessage("Finalizing.");
                loadingForm.SetProgress(95);
                DisplaySessionDetails(_selectedSession);
                LoadChatHistoryTab();
                MainForm.Bot?.SaveChatHistory();
            }
            finally
            {
                LLMEngine.RemoveQuickInferenceEventHandler();
                loadingForm.Close();
                this.Enabled = true;
            }
        }

        private void ed_sessiontitle_TextChanged(object sender, EventArgs e)
        {
            if (_isinitloading || _selectedSession == null)
                return;
            _selectedSession.MetaData.Title = ed_sessiontitle.Text;
        }

        private void ed_sessioninfo_TextChanged(object sender, EventArgs e)
        {
            if (_isinitloading || _selectedSession == null)
                return;
            _selectedSession.MetaData.Summary = ed_sessioninfo.Text.ToLinuxFormat();
        }

        private async void bt_historyupdate_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
            await _selectedSession.EmbedText();
            DisplaySessionDetails(_selectedSession);
            LoadChatHistoryTab();
            MainForm.Bot?.SaveChatHistory();
        }

        private async void btEmbedAll_Click(object sender, EventArgs e)
        {
            if (!LLMEngine.Settings.RAGEnabled)
            {
                MessageBox.Show("The RAG System is not enabled. Operation cancelled.");
                return;
            }
            this.Enabled = false;

            using var loadingForm = new LoadingForm() { Owner = this };

            // Set the position in a way that doesn't affect the internal layout
            loadingForm.StartPosition = FormStartPosition.Manual;
            loadingForm.CenterToParent();
            loadingForm.Show();
            loadingForm.BringToFront();
            loadingForm.Refresh();

            try
            {
                loadingForm.SetMessage("Embedding all chat sessions. This might take a moment.");
                loadingForm.SetProgress(0);
                loadingForm.SetMax(LLMEngine.History.Sessions.Count);
                EmbedTools.OnEmbedText += (s, e) =>
                {
                    loadingForm.AddProgress(1);
                };
                await LLMEngine.History.EmbedChatSessions();
                loadingForm.SetMessage("Saving history...");
                MainForm.Bot?.SaveChatHistory(true);
                loadingForm.SetMessage("Brain Embedding...");
                await LLMEngine.Bot.Brain.RegenEmbeds();
                loadingForm.SetMessage("Loading Updated Vector Database...");
                LLMEngine.Bot.Brain.ReloadMemories();
            }
            finally
            {
                EmbedTools.RemoveEmbedEventHandler();
                loadingForm.Close();
                this.Enabled = true;
                MessageBox.Show("All sessions have been embedded successfully.");
            }
        }

        private void bt_deleteAllHistory_Click(object sender, EventArgs e)
        {
            // Confirm before deleting
            if (MessageBox.Show("This will delete all chat history with this character permanently. Are you sure?", "Delete All History?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LLMEngine.History.DeleteAll(true);
                var message = new SingleMessage(AuthorRole.Assistant, LLMEngine.Bot.GetWelcomeLine(LLMEngine.User.Name));
                LLMEngine.History.LogMessage(message);
                LoadChatHistoryTab();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ChatHistoryForm_Load(object sender, EventArgs e)
        {
            LoadChatHistoryTab();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;

            var action = AgentRuntime.GetAction<MoodAnalysis?, SessionMoodCheckParams>("SessionMoodCheckAction");
            if (action is not null)
            {
                var result = await action.Execute(new SessionMoodCheckParams(_selectedSession), CancellationToken.None);
                if (result is not null)
                {
                    var strb = new StringBuilder();
                    strb.AppendLine($"Horniness: {result.Horniness.ToString()}");
                    strb.AppendLine($"Submission: {result.Submission.ToString()}");
                    strb.AppendLine($"Energy: {result.Energy.ToString()}");
                    strb.AppendLine($"Cheer: {result.Happy.ToString()}");
                    strb.AppendLine($"Curiosity: {result.Curiosity.ToString()}");
                    strb.AppendLine($"Sanity: {result.Sanity.ToString()}");
                    MessageBox.Show(strb.ToString(), "Mood Analysis Results");
                }
                else
                {
                    MessageBox.Show("Mood analysis failed or returned no data.", "Mood Analysis Results");
                }
            }
        }

        private void lbl_sessiondata_Click(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Comparer for sorting ChatSession items in the ListView
    /// </summary>
    internal sealed class SessionListComparer(int columnIndex, bool ascending) : IComparer
    {
        private readonly int _columnIndex = columnIndex;
        private readonly bool _ascending = ascending;

        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem a || y is not ListViewItem b)
                return 0;

            var sessionA = a.Tag as ChatSession;
            var sessionB = b.Tag as ChatSession;

            int result;

            switch (_columnIndex)
            {
                case 0: // Title/Name
                    var titleA = sessionA?.Name ?? a.SubItems[0].Text ?? string.Empty;
                    var titleB = sessionB?.Name ?? b.SubItems[0].Text ?? string.Empty;
                    result = string.Compare(titleA, titleB, StringComparison.OrdinalIgnoreCase);
                    break;

                case 1: // Date
                    var dateA = sessionA?.StartTime ?? ParseDateString(a.SubItems.Count > 1 ? a.SubItems[1].Text : "");
                    var dateB = sessionB?.StartTime ?? ParseDateString(b.SubItems.Count > 1 ? b.SubItems[1].Text : "");
                    result = DateTime.Compare(dateA, dateB);
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
    }
}