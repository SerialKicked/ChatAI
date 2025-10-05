using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.Agent.Actions;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Markdig;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.Diagnostics;
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

        public static Character? Bot => LLMEngine.Bot as Character;
        public static Character? User => LLMEngine.User as Character;

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
                bt_deleteAllHistory_Click(this, EventArgs.Empty);
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
            }
            var dialogs = session.GetRawDialogs(int.MaxValue, false).Replace("\n", "\n\n");

            var res = new StringBuilder();
            res.AppendLinuxLine($"## {session.Name}").AppendLinuxLine();
            res.AppendLinuxLine("### Summary:").AppendLinuxLine().AppendLinuxLine(session.Content).AppendLinuxLine();
            res.AppendLinuxLine("### Keywords: ");
            foreach (var item in session.MetaData.Keywords)
            {
                res.Append(item + ", ");
            }
            res.AppendLinuxLine();
            res.AppendLinuxLine("### Goals: ");
            foreach (var item in session.MetaData.FutureGoals)
            {
                res.AppendLinuxLine("- " + item);
            }
            res.AppendLinuxLine("### Relevance: " + session.MetaData.Relevance.ToString());
            res.AppendLinuxLine().AppendLinuxLine();
            if (session.Sentiments.Count > 0)
            {
                res.AppendLinuxLine("### Sentiments: " + session.MetaData.Relevance.ToString());
                foreach (var item in session.Sentiments)
                {
                    res.AppendLine($"{item.Label}: {item.Probability}");
                }
                res.AppendLinuxLine().AppendLinuxLine();

            }
            res.AppendLinuxLine("### Dialogs:").AppendLinuxLine().AppendLinuxLine(dialogs);
            web_sessioncontent.NavigateToString(Markdown.ToHtml(res.ToString(), CustomMarkDownPipeline));
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
                (LLMEngine.Bot as Character)?.SaveChatHistory();
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
            (LLMEngine.Bot as Character)?.SaveChatHistory();
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
                (LLMEngine.Bot as Character)?.SaveChatHistory(true);
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
                var message = new SingleMessage(AuthorRole.Assistant, DateTime.Now, LLMEngine.Bot.GetWelcomeLine(LLMEngine.User.Name), LLMEngine.Bot.UniqueName, LLMEngine.Bot.UniqueName);
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
                    strb.AppendLine($"Cheer: {result.Cheer.ToString()}");
                    strb.AppendLine($"Curiosity: {result.Curiosity.ToString()}");
                    strb.AppendLine($"Sanity: {result.Sanity.ToString()}");
                    MessageBox.Show(strb.ToString(), "Mood Analysis Results");
                }
                else
                {
                    MessageBox.Show("Mood analysis failed or returned no data.", "Mood Analysis Results");
                }
            }


            //await _selectedSession.UpdateSentiment();
            //var strb = new StringBuilder();
            //foreach (var item in _selectedSession.Sentiments)
            //{
            //    strb.AppendLine($"{item.Label}: {item.Probability}");
            //}
            //MessageBox.Show(strb.ToString(), "Sentiment Analysis Results");
        }
    }
}