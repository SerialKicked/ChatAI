using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaifuAI.src.forms
{
    public partial class LoadingForm: Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        public void SetProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => progress_bar.Value = progress));
            }
            else
            {
                progress_bar.Value = progress;
            }
        }

        public void SetMax(int max)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => progress_bar.Maximum = max));
            }
            else
            {
                progress_bar.Maximum = max;
            }
        }

        public new void CenterToParent()
        {
            if (Owner != null)
            {
                this.StartPosition = FormStartPosition.Manual;

                // Get the owner's center point
                Point ownerCenter = new Point(
                    Owner.Left + (Owner.Width / 2),
                    Owner.Top + (Owner.Height / 2)
                );

                // Calculate where to place this form
                this.Location = new Point(
                    ownerCenter.X - (this.Width / 2),
                    ownerCenter.Y - (this.Height / 2)
                );

                // Ensure the form is fully visible on screen
                Rectangle screenBounds = Screen.FromControl(Owner).Bounds;
                if (this.Left < screenBounds.Left)
                    this.Left = screenBounds.Left + 10;
                else if (this.Right > screenBounds.Right)
                    this.Left = screenBounds.Right - this.Width - 10;

                if (this.Top < screenBounds.Top)
                    this.Top = screenBounds.Top + 10;
                else if (this.Bottom > screenBounds.Bottom)
                    this.Top = screenBounds.Bottom - this.Height - 10;
            }
        }

        public void AddProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => progress_bar.Value += progress));
            }
            else
            { 
                if (progress_bar.Value + progress > progress_bar.Maximum)
                    progress_bar.Value = progress_bar.Maximum;
                else
                    progress_bar.Value += progress;
            }
        }

        public void SetMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lbl_info.Text = message));
            }
            else
            {
                lbl_info.Text = message;
            }
        }
    }
}
