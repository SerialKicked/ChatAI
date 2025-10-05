using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WaifuAI.Controls
{
    [ToolboxItem(true)]
    [DefaultProperty(nameof(TabPages))]
    [DefaultEvent(nameof(SelectedIndexChanged))]
    public class ModernTabControl : TabControl, IThemeAware
    {
        public bool ThemeProcessInnerComponent => true;

        private const int TabHeight = 36;
        private const int TabMinWidth = 100;
        private const int TabMaxWidth = 250;
        private const int TabPadding = 20;

        // Interaction state
        private int _hoverIndex = -1;
        private int _hoverCloseIndex = -1;

        // Cache the EXACT rectangles we draw - this is the single source of truth
        private readonly List<Rectangle> _drawnTabRects = [];
        private int _lastCalculatedWidth = 0;

        // Theme colors
        private Color _back;
        private Color _panel;
        private Color _border;
        private Color _accent;
        private Color _accentHover;
        private Color _text;
        private Color _textMuted;
        private Color _glyphBack;
        private Color _glyphBackHover;

        private ModernTabStyle _tabStyle = ModernTabStyle.Underline;

        public ModernTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            ItemSize = new Size(0, TabHeight);
            SizeMode = TabSizeMode.Normal;
            Alignment = TabAlignment.Top;
            DrawMode = TabDrawMode.Normal;
            Appearance = TabAppearance.Buttons;

            ThemeManager.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.CurrentTheme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

        private void OnThemeChanged(object? sender, ThemeManager.Theme t) => ApplyTheme(t);

        [Category("Appearance")]
        [DefaultValue(ModernTabStyle.Underline)]
        public ModernTabStyle TabStyle
        {
            get => _tabStyle;
            set
            {
                if (_tabStyle != value)
                {
                    _tabStyle = value;
                    Invalidate();
                }
            }
        }

        public void ApplyTheme(ThemeManager.Theme t)
        {
            _back = t.Back;
            _panel = t.Panel;
            _border = t.Border;
            _accent = t.Accent;
            _accentHover = t.AccentHover;
            _text = t.TextPrimary;
            _textMuted = t.TextMuted;
            _glyphBack = t.GlyphBack;
            _glyphBackHover = t.GlyphBackHover;
            Font = t.Base;

            foreach (TabPage page in TabPages)
            {
                page.BackColor = _panel;
                page.ForeColor = _text;
                page.Font = t.Base;
            }

            Invalidate();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int newHover = GetTabIndexAtPoint(e.Location);

            if (newHover != _hoverIndex)
            {
                _hoverIndex = newHover;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_hoverIndex != -1 || _hoverCloseIndex != -1)
            {
                _hoverIndex = -1;
                _hoverCloseIndex = -1;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Handle tab clicks manually since we're owner-drawing
            if (e.Button == MouseButtons.Left)
            {
                int index = GetTabIndexAtPoint(e.Location);
                if (index >= 0 && index < TabPages.Count)
                {
                    SelectedIndex = index;
                }
            }

            base.OnMouseClick(e);
        }

        private int GetTabIndexAtPoint(Point pt)
        {
            EnsureTabRectsCalculated();
            // Use the exact rectangles we drew
            for (int i = 0; i < _drawnTabRects.Count; i++)
            {
                if (_drawnTabRects[i].Contains(pt))
                    return i;
            }
            return -1;
        }

        private void EnsureTabRectsCalculated()
        {
            if (_drawnTabRects.Count != TabPages.Count || _lastCalculatedWidth != Width)
            {
                CalculateTabRects();
            }
        }

        private void CalculateTabRects()
        {
            _drawnTabRects.Clear();
            _lastCalculatedWidth = Width;

            int x = 2;
            for (int i = 0; i < TabPages.Count; i++)
            {
                string text = TabPages[i].Text;
                Size textSize = TextRenderer.MeasureText(text, Font,
                    new Size(int.MaxValue, int.MaxValue),
                    TextFormatFlags.Left | TextFormatFlags.NoPrefix);

                int tabWidth = textSize.Width + (TabPadding * 2);
                tabWidth = Math.Max(TabMinWidth, Math.Min(TabMaxWidth, tabWidth));

                Rectangle tabRect = new(x, 2, tabWidth, TabHeight - 2);
                _drawnTabRects.Add(tabRect);

                x += tabWidth;
            }
        }

        public new Rectangle GetTabRect(int index)
        {
            EnsureTabRectsCalculated();
            if (index >= 0 && index < _drawnTabRects.Count)
            {
                return _drawnTabRects[index];
            }
            return Rectangle.Empty;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            EnsureTabRectsCalculated(); // Ensure up to date before drawing

            var g = e.Graphics;
            g.Clear(_back);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Draw tab strip background
            Rectangle stripRect = new(0, 0, Width, TabHeight);
            using (var b = new SolidBrush(_panel))
                g.FillRectangle(b, stripRect);

            // Draw tabs using the pre-calculated rectangles
            for (int i = 0; i < _drawnTabRects.Count; i++)
            {
                DrawTab(g, i, _drawnTabRects[i]);
            }

            // Draw content area border
            Rectangle contentRect = new(0, TabHeight, Width - 1, Height - TabHeight - 1);
            using var bp = new Pen(_border);
            g.DrawRectangle(bp, contentRect);

            // Draw bottom border of tab strip
            g.DrawLine(bp, 0, TabHeight - 1, Width, TabHeight - 1);
        }

        private void DrawTab(Graphics g, int index, Rectangle tabRect)
        {
            bool isSelected = index == SelectedIndex;
            bool isHovered = index == _hoverIndex;

            TabPage page = TabPages[index];
            string text = page.Text;

            // Background for selected tab
            if (isSelected && _tabStyle == ModernTabStyle.Filled)
            {
                using var b = new SolidBrush(_glyphBack);
                g.FillRectangle(b, tabRect);
            }
            else if (isHovered && !isSelected)
            {
                using var b = new SolidBrush(Color.FromArgb(20, _accent));
                g.FillRectangle(b, tabRect);
            }

            // Text
            Color textColor = isSelected ? _text : _textMuted;
            if (isHovered && !isSelected)
                textColor = _text;

            int textWidth = tabRect.Width - TabPadding * 2;

            Rectangle textRect = new(
                tabRect.X + TabPadding,
                tabRect.Y,
                textWidth,
                tabRect.Height
            );

            TextRenderer.DrawText(g, text, Font, textRect, textColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);

            // Indicator
            DrawTabIndicator(g, tabRect, isSelected, index);
        }

        private void DrawTabIndicator(Graphics g, Rectangle tabRect, bool isSelected, int index)
        {
            if (_tabStyle == ModernTabStyle.None)
                return;

            Color indicatorColor = _accent;
            float alpha = 1f;

            if (!isSelected)
            {
                return;
            }

            if (alpha <= 0)
                return;

            indicatorColor = Color.FromArgb((int)(255 * alpha), indicatorColor);

            using var pen = new Pen(indicatorColor, 3f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            if (_tabStyle == ModernTabStyle.Underline)
            {
                int margin = 8;
                g.DrawLine(pen,
                    tabRect.X + margin,
                    tabRect.Bottom - 2,
                    tabRect.Right - margin,
                    tabRect.Bottom - 2);
            }
            else if (_tabStyle == ModernTabStyle.LeftBar)
            {
                g.DrawLine(pen,
                    tabRect.X + 2,
                    tabRect.Y + 6,
                    tabRect.X + 2,
                    tabRect.Bottom - 6);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is TabPage page)
            {
                page.BackColor = _panel;
                page.ForeColor = _text;
                page.Font = Font;
            }
            _drawnTabRects.Clear(); // Force recalculation
            Invalidate();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            _drawnTabRects.Clear(); // Force recalculation
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _drawnTabRects.Clear(); // Force recalculation
            Invalidate();
        }

        // Override CreateParams to remove default painting flicker
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }

    public enum ModernTabStyle
    {
        None,
        Underline,
        LeftBar,
        Filled
    }
}