using LetheAISharp.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetheChat.src.forms
{
    public partial class SamplerForm : Form
    {
        public SamplerSettings SelectedSamplerEditor = new();
        private bool disableevents = false;

        public SamplerForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        public void SetupSamplerEditor(string Forceid = "")
        {
            disableevents = true;
            listSampler.Items.Clear();
            foreach (var item in DataFiles.Inference)
            {
                listSampler.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = listSampler.Items.IndexOf(Forceid);
            if (listSampler.Items.Count > 0 && idwant != -1)
            {
                listSampler.SelectedIndex = idwant;
                SelectedSamplerEditor = DataFiles.Inference[listSampler.SelectedItem!.ToString()!].GetCopy();
            }
            LoadSamplerSettings(SelectedSamplerEditor);
            disableevents = false;
        }

        private void LoadSamplerSettings(SamplerSettings selected)
        {
            num_temp.Value = (decimal)selected.Temperature;
            num_seed.Value = selected.Sampler_seed;
            num_topk.Value = selected.Top_k;
            num_topp.Value = (decimal)selected.Top_p;
            num_typical.Value = (decimal)selected.Typical;
            num_minp.Value = (decimal)selected.Min_p;
            num_topa.Value = (decimal)selected.Top_a;
            num_tfs.Value = (decimal)selected.Tfs;
            num_reppen.Value = (decimal)selected.Rep_pen;
            num_reppenrange.Value = selected.Rep_pen_range;
            cb_miro.SelectedIndex = (int)selected.Mirostat;
            num_meta.Value = (decimal)selected.Mirostat_eta;
            num_mtau.Value = (decimal)selected.Mirostat_tau;
            num_xtcthres.Value = (decimal)selected.Xtc_threshold;
            num_xtcprob.Value = (decimal)selected.Xtc_probability;
            num_drybase.Value = (decimal)selected.Dry_base;
            num_drymul.Value = (decimal)selected.Dry_multiplier;
            num_dryrange.Value = selected.Dry_allowed_length;
            num_dynexpo.Value = (decimal)selected.Dynatemp_exponent;
            num_dynrange.Value = (decimal)selected.Dynatemp_range;
            num_smoothfac.Value = (decimal)selected.Smoothing_factor;
            ck_ignoreeos.Checked = selected.Bypass_eos;
            ck_renderspecial.Checked = selected.Render_special;
            ck_trimstop.Checked = selected.Trim_stop;
            edFileName.Text = selected.UniqueName;
        }

        private SamplerSettings SaveSamplerUIToSettiongs()
        {
            return new SamplerSettings()
            {
                Temperature = (double)num_temp.Value,
                Sampler_seed = (int)num_seed.Value,
                Top_k = (int)num_topk.Value,
                Top_p = (double)num_topp.Value,
                Typical = (double)num_typical.Value,
                Min_p = (double)num_minp.Value,
                Top_a = (double)num_topa.Value,
                Tfs = (double)num_tfs.Value,
                Rep_pen = (double)num_reppen.Value,
                Rep_pen_range = (int)num_reppenrange.Value,
                Mirostat = (double)cb_miro.SelectedIndex,
                Mirostat_eta = (double)num_meta.Value,
                Mirostat_tau = (int)num_mtau.Value,
                Xtc_threshold = (double)num_xtcthres.Value,
                Xtc_probability = (double)num_xtcprob.Value,
                Dry_base = (double)num_drybase.Value,
                Dry_multiplier = (double)num_drymul.Value,
                Dry_allowed_length = (int)num_dryrange.Value,
                Dynatemp_exponent = (double)num_dynexpo.Value,
                Dynatemp_range = (double)num_dynrange.Value,
                Smoothing_factor = (double)num_smoothfac.Value,
                Bypass_eos = ck_ignoreeos.Checked,
                Render_special = ck_renderspecial.Checked,
                Trim_stop = ck_trimstop.Checked,
                Sampler_order = [6, 0, 1, 3, 4, 2, 5],
                Dry_sequence_breakers = ["\n", ":", "\"", "*", "<|im_end|>", "<|im_start|>", "[INST]", "[/INST]"],
                Max_context_length = 8192,
                Max_length = 512,
                Prompt = "",
                Memory = "",
                Images = []
            };
        }

        private void listSampler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSampler.SelectedItem == null || disableevents)
                return;
            SelectedSamplerEditor = DataFiles.Inference[listSampler.SelectedItem.ToString()!].GetCopy();
            edFileName.Text = listSampler.SelectedItem.ToString();
            LoadSamplerSettings(SelectedSamplerEditor);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var NewName = edFileName.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new sampler");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Inference.ContainsKey(NewName) && (MessageBox.Show("This sampler already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedSamplerEditor = SaveSamplerUIToSettiongs();
            SelectedSamplerEditor.UniqueName = NewName;
            DataFiles.Inference[NewName] = SelectedSamplerEditor;
            (SelectedSamplerEditor as IFile).SaveToFile("data/params/" + NewName + ".json");
            SetupSamplerEditor(NewName);
        }

        private void SamplerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
