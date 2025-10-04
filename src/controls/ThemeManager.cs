using Markdig.Renderers.Roundtrip.Inlines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WaifuAI.Controls
{
    public interface IThemeAware
    {
        void ApplyTheme(ThemeManager.Theme theme);
    }

    internal static class NativeDarkMode
    {
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_OLD = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);

        public static void ApplyDarkModeIfNeeded(Form form, bool dark)
        {
            if (form.IsDisposed) return;
            try
            {
                int useDark = dark ? 1 : 0;
                _ = DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));
                _ = DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE_OLD, ref useDark, sizeof(int));
            }
            catch { }

            EnumerateDescendants(form, h =>
            {
                try { SetWindowTheme(h, "Explorer", null); } catch { }
            });
        }

        private static void EnumerateDescendants(Control root, Action<IntPtr> action)
        {
            if (root.IsDisposed) return;
            action(root.Handle);
            foreach (Control c in root.Controls)
                EnumerateDescendants(c, action);
        }
    }

    public interface IThemeExclude { }
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ThemeExcludeAttribute : Attribute { }

    public static class ThemeManager
    {
        // Legacy palette fields
        public static Color curthemeBackColor { get; private set; } = Color.FromArgb(0x1E, 0x1F, 0x22);
        public static Color curthemePanelColor { get; private set; } = Color.FromArgb(0x25, 0x26, 0x2A);
        public static Color curthemeBorderColor { get; private set; } = Color.FromArgb(0x37, 0x38, 0x3D);
        public static Color curthemeAccentColor { get; private set; } = Color.FromArgb(0x4D, 0xA3, 0xFF);
        public static Color curthemeAccentHover { get; private set; } = Color.FromArgb(0x60, 0xB4, 0xFF);
        public static Color curthemeDangerColor { get; private set; } = Color.FromArgb(0xE1, 0x63, 0x63);
        public static Color curthemeTextColor { get; private set; } = Color.FromArgb(0xE6, 0xE6, 0xE6);
        public static Color curthemeMutedText { get; private set; } = Color.FromArgb(0x9F, 0xA4, 0xAE);
        public static Color curthemeSuccessColor { get; private set; } = Color.FromArgb(0x57, 0xC4, 0x7B);
        public static Color curthemeFocusOutline { get; private set; } = Color.FromArgb(0x70, 0xB8, 0xFF);
        public static Color curthemeGlyphBack { get; private set; } = Color.FromArgb(45, 46, 50);
        public static Color curthemeGlyphBackHover { get; private set; } = Color.FromArgb(55, 56, 60);

        public static Font curthemeBaseFont { get; private set; } = new("Segoe UI", 9F);
        public static Font curthemeHeaderFont { get; private set; } = new("Segoe UI Semibold", 9.75F);
        public static Font curthemeMonoSmall { get; private set; } = new("Consolas", 8.5F);
        public static Font curthemeButtonFont { get; private set; } = new("Segoe UI Semibold", 9F);   

        public sealed record Theme(
            string Name,
            Color Back,
            Color Panel,
            Color Border,
            Color Accent,
            Color AccentHover,
            Color TextPrimary,
            Color TextMuted,
            Color Danger,
            Color Success,
            Color Focus,
            Color GlyphBack,
            Color GlyphBackHover,
            Font Base,
            Font Header,
            Font Mono,
            Font Button
        );

        public static Theme CurrentTheme { get; private set; } = CreateDark();
        public static event EventHandler<Theme>? ThemeChanged;

        private static readonly HashSet<ComboBox> OwnerDrawnComboBoxes = new();
        private static readonly HashSet<TabControl> OwnerDrawnTabControls = new();

        private static readonly Dictionary<Control, (Color Fore, Color Back, Font? Font)> ExcludedOriginals = new();

        // TabControl config (applies only in Buttons mode now)
        private const bool FillHeaderBackground = true;
        private const bool UsePanelForHeader = false;

        // =========================================================
        // Public API
        // =========================================================
        public static void ApplyDark() => ApplyTheme(CreateDark());
        public static void ApplyLight() => ApplyTheme(CreateLight());
        public static void ToggleTheme()
            => ApplyTheme(CurrentTheme.Name.Equals("Dark", StringComparison.OrdinalIgnoreCase) ? CreateLight() : CreateDark());

        public static void ApplyTheme(Theme t, bool broadcast = true)
        {
            CurrentTheme = t;

            curthemeBackColor = t.Back;
            curthemePanelColor = t.Panel;
            curthemeBorderColor = t.Border;
            curthemeAccentColor = t.Accent;
            curthemeAccentHover = t.AccentHover;
            curthemeTextColor = t.TextPrimary;
            curthemeMutedText = t.TextMuted;
            curthemeDangerColor = t.Danger;
            curthemeSuccessColor = t.Success;
            curthemeFocusOutline = t.Focus;
            curthemeGlyphBack = t.GlyphBack;
            curthemeGlyphBackHover = t.GlyphBackHover;

            curthemeBaseFont = t.Base;
            curthemeHeaderFont = t.Header;
            curthemeMonoSmall = t.Mono;
            curthemeButtonFont = t.Button;

            if (broadcast)
                ThemeChanged?.Invoke(null, t);
        }

        public static void ApplyToForm(Form f)
        {
            if (f == null) return;
            ExcludedOriginals.Clear();

            foreach (Control c in f.Controls)
                CaptureExcludedRecursive(c);

            f.BackColor = curthemeBackColor;
            f.Font = curthemeBaseFont;
            foreach (Control c in f.Controls)
                ApplyRecursive(c);

            RestoreExcluded();
            NativeDarkMode.ApplyDarkModeIfNeeded(f, CurrentTheme.Name.Equals("Dark", StringComparison.OrdinalIgnoreCase));
        }

        public static void ReapplyTheme(Form f) => ApplyToForm(f);

        // =========================================================
        // Theme factories
        // =========================================================
        public static Theme CreateDark(string name = "Dark")
        {
            var accent = GetSystemAccentOrFallback(Color.FromArgb(0x4D, 0xA3, 0xFF));
            return new Theme(
                name,
                Back: Color.FromArgb(0x15, 0x15, 0x15),
                Panel: Color.FromArgb(0x25, 0x25, 0x25),
                Border: Color.FromArgb(0x40, 0x40, 0x40),
                Accent: Color.FromArgb(0x36, 0xA0, 0x36),
                AccentHover: Color.FromArgb(0x36, 0x36, 0x90),
                TextPrimary: Color.FromArgb(0xE6, 0xE6, 0xE6),
                TextMuted: Color.FromArgb(0x9F, 0x9F, 0x9F),
                Danger: Color.FromArgb(0xA0, 0x40, 0x40),
                Success: Color.FromArgb(0x40, 0x40, 0xA0),
                Focus: Color.FromArgb(0x70, 0xB8, 0xFF),
                GlyphBack: Color.FromArgb(50, 50, 50),
                GlyphBackHover: Color.FromArgb(70, 70, 70),
                Base: new Font("Segoe UI", 9.25F),
                Header: new Font("Segoe UI Semibold", 10F),
                Mono: new Font("Consolas", 8.75F),
                Button: new Font("Segoe UI Semibold", 9F)
            );
        }

        public static Theme CreateLight(string name = "Light")
        {
            var accent = GetSystemAccentOrFallback(Color.FromArgb(0x2D, 0x7D, 0xFF));
            return new Theme(
                name,
                Back: Color.FromArgb(245, 246, 247),
                Panel: Color.FromArgb(235, 236, 237),
                Border: Color.FromArgb(180, 180, 185),
                Accent: accent,
                AccentHover: Lighten(accent, 0.18),
                TextPrimary: Color.FromArgb(40, 40, 42),
                TextMuted: Color.FromArgb(100, 102, 106),
                Danger: Color.FromArgb(205, 70, 70),
                Success: Color.FromArgb(60, 160, 95),
                Focus: accent,
                GlyphBack: Color.FromArgb(225, 226, 228),
                GlyphBackHover: Color.FromArgb(210, 211, 213),
                Base: new Font("Segoe UI", 9F),
                Header: new Font("Segoe UI Semibold", 9.75F),
                Mono: new Font("Consolas", 8.5F),
                Button: new Font("Segoe UI Semibold", 9F)
            );
        }

        // =========================================================
        // Capture excluded
        // =========================================================
        private static void CaptureExcludedRecursive(Control c)
        {
            if (IsExcluded(c))
            {
                if (!ExcludedOriginals.ContainsKey(c))
                    ExcludedOriginals[c] = (c.ForeColor, c.BackColor, c.Font);
                return;
            }
            foreach (Control child in c.Controls)
                CaptureExcludedRecursive(child);
        }

        // =========================================================
        // Apply recursively
        // =========================================================
        private static void ApplyRecursive(Control c)
        {
            if (IsExcluded(c))
                return;

            switch (c)
            {
                case Panel:
                    c.BackColor = curthemePanelColor;
                    c.ForeColor = curthemeTextColor;
                    c.Font = curthemeBaseFont;
                    break;

                case GroupBox gb:
                    gb.BackColor = curthemePanelColor;
                    gb.ForeColor = curthemeTextColor;
                    gb.Font = curthemeBaseFont;
                    break;

                case Button bt:
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.FlatAppearance.BorderColor = curthemeBorderColor;
                    bt.BackColor = curthemePanelColor;
                    bt.ForeColor = curthemeTextColor;
                    bt.Font = curthemeBaseFont;
                    break;

                case CheckBox chk:
                    chk.BackColor = curthemePanelColor;
                    chk.ForeColor = curthemeTextColor;
                    chk.FlatStyle = FlatStyle.Flat;
                    chk.FlatAppearance.BorderColor = curthemeBorderColor;
                    chk.FlatAppearance.BorderSize = 1;
                    chk.Font = curthemeBaseFont;
                    break;

                case ComboBox cb:
                    ThemeComboBox(cb);
                    break;

                case TextBox tb:
                    tb.BackColor = curthemePanelColor;
                    tb.ForeColor = curthemeTextColor;
                    tb.Font = curthemeBaseFont;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case Label lbl:
                    if (lbl.BackColor != Color.Transparent)
                        lbl.BackColor = curthemePanelColor;
                    lbl.ForeColor = curthemeTextColor;
                    lbl.Font = lbl.Font.Bold || lbl.Font.Italic
                        ? new Font(curthemeBaseFont, lbl.Font.Style)
                        : curthemeBaseFont;
                    break;

                case ListBox lb:
                    lb.BackColor = curthemePanelColor;
                    lb.ForeColor = curthemeTextColor;
                    lb.Font = curthemeBaseFont;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case ListView lv:
                    lv.BackColor = curthemePanelColor;
                    lv.ForeColor = curthemeTextColor;
                    lv.Font = curthemeBaseFont;
                    lv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case NumericUpDown nud:
                    nud.BackColor = curthemePanelColor;
                    nud.ForeColor = curthemeTextColor;
                    nud.Font = curthemeBaseFont;
                    break;

                case TabControl tc:
                    ThemeTabControl(tc);
                    break;
            }

            if (c is IThemeAware aware)
                aware.ApplyTheme(CurrentTheme);

            foreach (Control child in c.Controls)
                ApplyRecursive(child);
        }

        // =========================================================
        // Restore excluded
        // =========================================================
        private static void RestoreExcluded()
        {
            foreach (var kv in ExcludedOriginals)
            {
                var ctrl = kv.Key;
                if (ctrl.IsDisposed) continue;
                ctrl.ForeColor = kv.Value.Fore;
                ctrl.BackColor = kv.Value.Back;
                if (kv.Value.Font != null && ctrl.Font != kv.Value.Font)
                    ctrl.Font = kv.Value.Font;
            }
        }

        private static bool IsExcluded(Control c)
        {
            if (c.Tag is string ts && ts.IndexOf("no-theme", StringComparison.OrdinalIgnoreCase) >= 0)
                return true;
            if (c is IThemeExclude) return true;
            if (c.GetType().IsDefined(typeof(ThemeExcludeAttribute), true)) return true;
            return false;
        }

        // =========================================================
        // ComboBox theming
        // =========================================================
        private static void ThemeComboBox(ComboBox cb)
        {
            cb.Font = curthemeBaseFont;
            cb.BackColor = curthemePanelColor;
            cb.ForeColor = curthemeTextColor;
            cb.FlatStyle = FlatStyle.Flat;

            if (cb.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                if (!OwnerDrawnComboBoxes.Contains(cb))
                {
                    cb.DrawMode = DrawMode.OwnerDrawFixed;
                    cb.DrawItem += ComboBox_DrawItem;
                    cb.DropDownClosed += (_, _) => cb.Invalidate();
                    OwnerDrawnComboBoxes.Add(cb);
                }
            }
            cb.Invalidate();
        }

        private static void ComboBox_DrawItem(object? sender, DrawItemEventArgs e)
        {
            var cb = (ComboBox)sender!;
            e.DrawBackground();

            bool selected = (e.State & DrawItemState.Selected) != 0;
            bool disabled = (e.State & DrawItemState.Disabled) != 0;

            Color back = selected ? curthemeAccentColor : curthemePanelColor;
            Color fore = disabled ? curthemeMutedText : (selected ? Color.White : curthemeTextColor);

            using (var b = new SolidBrush(back))
                e.Graphics.FillRectangle(b, e.Bounds);

            if (e.Index >= 0 && e.Index < cb.Items.Count)
            {
                string text = cb.GetItemText(cb.Items[e.Index]);
                TextRenderer.DrawText(e.Graphics, text, cb.Font, e.Bounds, fore,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }

            e.DrawFocusRectangle();
        }

        // =========================================================
        // TabControl theming
        // =========================================================
        private static void ThemeTabControl(TabControl tc)
        {
            tc.Font = curthemeBaseFont;
            tc.BackColor = curthemePanelColor;
            tc.ForeColor = curthemeTextColor;

            foreach (TabPage page in tc.TabPages)
            {
                if (IsExcluded(page)) continue;
                page.BackColor = curthemePanelColor;
                page.ForeColor = curthemeTextColor;
                page.Font = curthemeBaseFont;
            }

            bool shouldOwnerDraw = tc.Appearance == TabAppearance.Buttons;
            EnsureTabControlOwnerDraw(tc, shouldOwnerDraw);
            tc.Invalidate();
        }

        private static void EnsureTabControlOwnerDraw(TabControl tc, bool enable)
        {
            if (enable)
            {
                if (!OwnerDrawnTabControls.Contains(tc))
                {
                    tc.DrawMode = TabDrawMode.OwnerDrawFixed;
                    tc.Padding = new Point(16, 4);
                    tc.DrawItem += TabControl_DrawItem;
                    tc.ControlAdded += TabControl_ChildChanged;
                    tc.ControlRemoved += TabControl_ChildChanged;
                    OwnerDrawnTabControls.Add(tc);
                }
            }
            else
            {
                if (OwnerDrawnTabControls.Contains(tc))
                {
                    // Revert to system drawing
                    tc.DrawItem -= TabControl_DrawItem;
                    tc.ControlAdded -= TabControl_ChildChanged;
                    tc.ControlRemoved -= TabControl_ChildChanged;
                    try { tc.DrawMode = TabDrawMode.Normal; } catch { }
                    OwnerDrawnTabControls.Remove(tc);
                }
            }
        }

        private static void TabControl_ChildChanged(object? sender, ControlEventArgs e)
        {
            if (sender is TabControl tc) tc.Invalidate();
        }

        private static void TabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            var tc = (TabControl)sender!;
            if (e.Index < 0 || e.Index >= tc.TabCount) return;

            // Header fill (Buttons mode only)
            if (e.Index == 0 && FillHeaderBackground)
            {
                int headerHeight = GetHeaderHeight(tc);
                var headerRect = new Rectangle(0, 0, tc.Width, headerHeight);
                Color headerColor = UsePanelForHeader ? curthemePanelColor : curthemeBackColor;

                using var hb = new SolidBrush(headerColor);
                e.Graphics.FillRectangle(hb, headerRect);

                using var pen = new Pen(curthemeBorderColor);
                e.Graphics.DrawLine(pen, headerRect.Left, headerRect.Bottom - 1, headerRect.Right, headerRect.Bottom - 1);
            }

            bool selected = (e.State & DrawItemState.Selected) != 0;
            var page = tc.TabPages[e.Index];
            Rectangle r = e.Bounds;

            Color tabBack = selected ? curthemeAccentColor : (UsePanelForHeader ? curthemePanelColor : curthemeBackColor);
            Color tabFore = selected ? Color.White : curthemeTextColor;
            Color tabBorder = selected ? curthemeAccentHover : curthemeBorderColor;

            using (var b = new SolidBrush(tabBack))
                e.Graphics.FillRectangle(b, r);

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                curthemeBaseFont,
                r,
                tabFore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            using var penBorder = new Pen(tabBorder);
            e.Graphics.DrawRectangle(penBorder, r.Left, r.Top, r.Width - 1, r.Height - 1);

            if ((e.State & DrawItemState.Focus) != 0)
            {
                var focusRect = Rectangle.Inflate(r, -4, -4);
                ControlPaint.DrawFocusRectangle(e.Graphics, focusRect, tabFore, tabBack);
            }
        }

        private static int GetHeaderHeight(TabControl tc)
        {
            int h = 0;
            for (int i = 0; i < tc.TabCount; i++)
            {
                var r = tc.GetTabRect(i);
                if (r.Bottom > h) h = r.Bottom;
            }
            return h + 1;
        }

        // =========================================================
        // Utilities
        // =========================================================
        public static Color Lighten(Color c, double amount)
        {
            amount = Math.Max(-1, Math.Min(1, amount));
            int r = Clamp(c.R + (int)(255 * amount));
            int g = Clamp(c.G + (int)(255 * amount));
            int b = Clamp(c.B + (int)(255 * amount));
            return Color.FromArgb(c.A, r, g, b);
        }

        private static int Clamp(int v) => v < 0 ? 0 : (v > 255 ? 255 : v);

        private static Color GetSystemAccentOrFallback(Color fallback)
        {
            try
            {
                if (DwmGetColorizationColor(out uint raw, out _) == 0)
                {
                    byte r = (byte)((raw >> 16) & 0xFF);
                    byte g = (byte)((raw >> 8) & 0xFF);
                    byte b = (byte)(raw & 0xFF);
                    return Color.FromArgb(r, g, b);
                }
            }
            catch { }
            return fallback;
        }

        [DllImport("dwmapi.dll", ExactSpelling = true, PreserveSig = true)]
        private static extern int DwmGetColorizationColor(out uint pcrColorization, out bool pfOpaqueBlend);
    }
}