using Markdig;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaifuAI.src.forms;

namespace WaifuAI
{
    public partial class ChatMessageControl : UserControl
    {
        public Guid AssociatedID;

#pragma warning disable CS8618
        public ChatMessageControl()
        {
            InitializeComponent();
            bt_edit!.Click += bt_edit_Click!;
        }
#pragma warning restore CS8618

        public async Task InitializeAsync()
        {
            await webBrowser.EnsureCoreWebView2Async();
        }

        public ChatMessageControl(Image image, string messageText) : this()
        {
            pictureBox.Image = image;
            string htmlContent = Markdown.ToHtml(messageText);
            htmlContent = InjectDialogCSS(htmlContent);
            InitializeAsync().ContinueWith(_ =>
            {
                webBrowser.NavigateToString(htmlContent);
                webBrowser.CoreWebView2.DOMContentLoaded += OnDOMContentLoaded!;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void UpdateMessage(string messageText)
        {
            if (webBrowser.InvokeRequired)
            {
                webBrowser.Invoke(new Action<string>(UpdateMessage), messageText);
            }
            else
            {
                string htmlContent = Markdown.ToHtml(messageText);
                htmlContent = InjectDialogCSS(htmlContent);
                webBrowser.NavigateToString(htmlContent);
            }
        }

        /// <summary>
        /// Resizes the component to fit the content vertically.
        /// </summary>
        public void ResizeVerticallyToFitContent()
        {
            if (webBrowser.CoreWebView2 != null)
            {
                webBrowser.CoreWebView2.DOMContentLoaded -= OnDOMContentLoaded!;
                webBrowser.CoreWebView2.DOMContentLoaded += OnDOMContentLoaded!;
            }
        }

        private async void OnDOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs args)
        {
            webBrowser.Height = 110;
            var heightString = await webBrowser.CoreWebView2.ExecuteScriptAsync("document.body.scrollHeight.toString()");
            if (int.TryParse(heightString.Trim('"'), out int height) && height > 110)
            {
                webBrowser.Height = height;
                Height = height + 6; // Adjust the control height to fit the content
            }
        }

        public async void ForceResizeVerticallyToFitContent()
        {
            if (webBrowser.CoreWebView2 != null)
            {
                webBrowser.Height = 110;
                var heightString = await webBrowser.CoreWebView2.ExecuteScriptAsync("document.body.scrollHeight.toString()");
                if (int.TryParse(heightString.Trim('"'), out int height) && height > 110)
                {
                    webBrowser.Height = height;
                    Height = height + 6; // Adjust the control height to fit the content
                }
            }
        }

        private static string InjectDialogCSS(string htmlContent)
        {
            string css = @"
            <style>
                body { overflow: hidden; }
                em { color: darkblue; }
            </style>";
            return $"<html><head>{css}</head><body>{htmlContent}</body></html>";
        }


        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            webBrowser = new WebView2();
            bt_edit = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webBrowser).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(3, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(50, 67);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // webBrowser
            // 
            webBrowser.AllowExternalDrop = true;
            webBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webBrowser.CreationProperties = null;
            webBrowser.DefaultBackgroundColor = Color.White;
            webBrowser.Location = new Point(59, 3);
            webBrowser.Name = "webBrowser";
            webBrowser.Size = new Size(443, 96);
            webBrowser.TabIndex = 1;
            webBrowser.ZoomFactor = 1D;
            // 
            // bt_edit
            // 
            bt_edit.Location = new Point(3, 76);
            bt_edit.Name = "bt_edit";
            bt_edit.Size = new Size(50, 23);
            bt_edit.TabIndex = 2;
            bt_edit.Text = "Edit";
            bt_edit.UseVisualStyleBackColor = true;
            // 
            // ChatMessageControl
            // 
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(bt_edit);
            Controls.Add(webBrowser);
            Controls.Add(pictureBox);
            Name = "ChatMessageControl";
            Size = new Size(505, 102);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)webBrowser).EndInit();
            ResumeLayout(false);
        }

        public async Task InitializeAsync(string imagePath, string messageText)
        {
            pictureBox.ImageLocation = imagePath;
            string htmlContent = Markdown.ToHtml(messageText);
            await webBrowser.EnsureCoreWebView2Async();
            webBrowser.NavigateToString(htmlContent);
        }

        private System.Windows.Forms.PictureBox pictureBox;
        private Button bt_edit;
        private WebView2 webBrowser;

        private void bt_edit_Click(object sender, EventArgs e)
        {
            var editForm = new EditMessageForm(AssociatedID);
            try
            {
                if (editForm.ShowDialog() == DialogResult.OK && editForm.Message != null)
                {
                    UpdateMessage(LLMSystem.GetMessagePrefix(editForm.Message) + editForm.Message.Message);
                    LLMSystem.InvalidatePromptCache();
                }
            }
            finally
            {
                editForm.Dispose();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (webBrowser != null)
                {
                    webBrowser.Dispose();
                    webBrowser = null!;
                }

                if (pictureBox != null)
                {
                    pictureBox.Dispose();
                    pictureBox = null!;
                }

                if (bt_edit != null)
                {
                    bt_edit.Dispose();
                    bt_edit = null!;
                }
            }
            base.Dispose(disposing);
        }
    }
}
