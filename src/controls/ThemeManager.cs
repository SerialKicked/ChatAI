using System.Drawing;

namespace WaifuAI.Controls
{
    public static class ThemeManager
    {
        // Core palette (adjust once, everything updates)
        public static Color BackColor = Color.FromArgb(0x1E, 0x1F, 0x22);
        public static Color PanelColor = Color.FromArgb(0x25, 0x26, 0x2A);
        public static Color BorderColor = Color.FromArgb(0x37, 0x38, 0x3D);
        public static Color AccentColor = Color.FromArgb(0x4D, 0xA3, 0xFF);
        public static Color AccentHover = Color.FromArgb(0x60, 0xB4, 0xFF);
        public static Color DangerColor = Color.FromArgb(0xE1, 0x63, 0x63);
        public static Color TextColor = Color.FromArgb(0xE6, 0xE6, 0xE6);
        public static Color MutedText = Color.FromArgb(0x9F, 0xA4, 0xAE);
        public static Color SuccessColor = Color.FromArgb(0x57, 0xC4, 0x7B);
        public static Color FocusOutline = Color.FromArgb(0x70, 0xB8, 0xFF);

        public static Font BaseFont = new("Segoe UI", 9F, FontStyle.Regular);
        public static Font HeaderFont = new("Segoe UI Semibold", 9.75F);
        public static Font MonoSmall = new("Consolas", 8.5F);

        public static void ApplyToForm(Form f)
        {
            f.BackColor = BackColor;
            f.Font = BaseFont;
            foreach (Control c in f.Controls)
                ApplyRecursive(c);
        }

        private static void ApplyRecursive(Control c)
        {
            switch (c)
            {
                case GroupBox gb:
                    gb.ForeColor = TextColor;
                    break;
                case Button bt:
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.FlatAppearance.BorderColor = BorderColor;
                    bt.BackColor = PanelColor;
                    bt.ForeColor = TextColor;
                    break;
                case CheckBox chk:
                    chk.ForeColor = TextColor;
                    break;
                case ComboBox cb:
                    cb.BackColor = PanelColor;
                    cb.ForeColor = TextColor;
                    break;
                case TextBox tb:
                    tb.BackColor = PanelColor;
                    tb.ForeColor = TextColor;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    break;
            }
            foreach (Control child in c.Controls)
                ApplyRecursive(child);
        }
    }
}