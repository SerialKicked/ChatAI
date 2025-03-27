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
