using Newtonsoft.Json.Linq;
using System.Text;

namespace LetheChat.Forms
{
    /// <summary>
    /// Asks the user to approve or deny a tool call requested by the model. Built in code, no designer file.
    /// </summary>
    public class ToolConfirmForm : Form
    {
        private const int MaxArgChars = 6000;

        public ToolConfirmForm(string toolName, string argumentsJson)
        {
            Text = "Tool call confirmation";
            Size = new Size(640, 460);
            MinimumSize = new Size(480, 320);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            KeyPreview = true;

            var lbl = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 44,
                Padding = new Padding(10, 10, 10, 0),
                Text = $"The model wants to run the tool '{toolName}'. Review the details below before allowing it."
            };

            var txt = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 9.75F),
                Text = FormatArguments(argumentsJson)
            };

            var panText = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 6, 10, 6) };
            panText.Controls.Add(txt);

            var panButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10, 8, 10, 8)
            };
            var btDeny = new Button { Text = "Deny", Width = 110, Height = 28, DialogResult = DialogResult.Cancel, BackColor = Color.IndianRed, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Tag = "no-theme" };
            var btAllow = new Button { Text = "Allow", Width = 110, Height = 28, DialogResult = DialogResult.OK, BackColor = Color.DarkSeaGreen, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Tag = "no-theme" };
            panButtons.Controls.Add(btDeny);
            panButtons.Controls.Add(btAllow);

            Controls.Add(panText);
            Controls.Add(lbl);
            Controls.Add(panButtons);
            AcceptButton = btAllow;
            CancelButton = btDeny;
        }

        private static string FormatArguments(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return "(no arguments)";
            var sb = new StringBuilder();
            try
            {
                var obj = JObject.Parse(json);
                foreach (var prop in obj.Properties())
                {
                    sb.Append(prop.Name).AppendLine(":");
                    var value = prop.Value.Type == JTokenType.String ? prop.Value.ToString() : prop.Value.ToString(Newtonsoft.Json.Formatting.None);
                    sb.AppendLine(value.Replace("\r\n", "\n").Replace("\n", Environment.NewLine));
                    sb.AppendLine();
                }
            }
            catch
            {
                sb.Append(json);
            }
            var text = sb.ToString().TrimEnd();
            if (text.Length > MaxArgChars)
                text = text[..MaxArgChars] + Environment.NewLine + "[... truncated for display ...]";
            return text;
        }
    }
}
