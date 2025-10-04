using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WaifuAI.Controls
{
    public enum ModernCheckVisual { Box, Switch }

    [ToolboxItem(true)]
    [DefaultProperty(nameof(Text))]
    [DefaultEvent(nameof(CheckedChanged))]
    public class ModernCheckBox : CheckBox, IThemeAware
    {
        // (Animation + state fields omitted here for brevity – keep your existing ones.)
        private const int BoxSize = 18;
        private const int SwitchWidth = 42;
        private const int SwitchHeight = 22;

        // Animation fields ...
        private readonly Stopwatch _animWatch = new();
        private Timer? _animTimer;
        private double _animStartValue;
        private double _animTargetValue;
        private double _animProgress;
        private bool _hover;
        private bool _mouseDown;

        // Theme-backed colors
        private Color _accent;
        private Color _accentHover;
        private Color _border;
        private Color _panel;
        private Color _glyphBack;
        private Color _glyphBackHover;
        private Color _text;

        private bool _useAnimation = false;
        private int _animationDuration = 140;
        private ModernCheckVisual _visualStyle = ModernCheckVisual.Box;

        public ModernCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            TabStop = true;
            AutoSize = false;
            Size = new Size(150, 26);

            ThemeManager.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.CurrentTheme);

            _animProgress = Checked ? 1.0 : 0.0;
            _animTargetValue = _animProgress;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.ThemeChanged -= OnThemeChanged;
            base.Dispose(disposing);
        }

        private void OnThemeChanged(object? sender, ThemeManager.Theme t) => ApplyTheme(t);

        // Basic exposed properties (retain what you already had; example subset)

        [Category("Appearance")]
        [DefaultValue(ModernCheckVisual.Box)]
        public ModernCheckVisual VisualStyle
        {
            get => _visualStyle;
            set
            {
                if (_visualStyle != value)
                {
                    _visualStyle = value;
                    if (_visualStyle == ModernCheckVisual.Switch)
                        Height = Math.Max(Height, SwitchHeight + 4);
                    Invalidate();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool UseAnimation
        {
            get => _useAnimation;
            set => _useAnimation = value;
        }

        [Category("Behavior")]
        [DefaultValue(140)]
        public int AnimationDuration
        {
            get => _animationDuration;
            set => _animationDuration = Math.Max(16, value);
        }

        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }

        // Theme Application
        public void ApplyTheme(ThemeManager.Theme t)
        {
            _accent = t.Accent;
            _accentHover = t.AccentHover;
            _border = t.Border;
            _panel = t.Panel;
            _glyphBack = t.GlyphBack;
            _glyphBackHover = t.GlyphBackHover;
            _text = t.TextPrimary;
            Font = t.Base;
            Invalidate();
        }

        private Color AccentEvaluated()
        {
            if (!Enabled) return ThemeManager.curthemeMutedText;
            return _hover ? _accentHover : _accent;
        }

        // -------------- Input / animation (reuse your prior implementation) --------------
        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _mouseDown = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) { _mouseDown = true; Invalidate(); } base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { if (_mouseDown && e.Button == MouseButtons.Left) { _mouseDown = false; Invalidate(); } base.OnMouseUp(e); }

        protected override void OnCheckedChanged(EventArgs e)
        {
            StartAnimation(Checked ? 1.0 : 0.0);
            base.OnCheckedChanged(e);
        }

        private void EnsureTimer()
        {
            _animTimer ??= new Timer { Interval = 15 };
            _animTimer.Tick -= AnimTick;
            _animTimer.Tick += AnimTick;
        }

        private void StartAnimation(double target)
        {
            if (!_useAnimation || DesignMode)
            {
                _animProgress = target;
                Invalidate();
                return;
            }
            EnsureTimer();
            _animStartValue = _animProgress;
            _animTargetValue = target;
            _animWatch.Restart();
            _animTimer!.Start();
        }

        private void AnimTick(object? _, EventArgs __)
        {
            double raw = _animWatch.Elapsed.TotalMilliseconds / _animationDuration;
            if (raw >= 1) raw = 1;
            double t = raw < 0.5 ? 2 * raw * raw : -1 + (4 - 2 * raw) * raw;
            _animProgress = _animStartValue + (_animTargetValue - _animStartValue) * t;
            Invalidate();
            if (raw >= 1) _animTimer?.Stop();
        }

        // -------------- Painting --------------
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent?.BackColor ?? _panel);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle glyph = GetGlyphRect();
            if (_visualStyle == ModernCheckVisual.Box)
                DrawBoxStyle(g, glyph);
            else
                DrawSwitchStyle(g, glyph);

            DrawLabel(g, glyph);
        }

        private Rectangle GetGlyphRect()
        {
            if (_visualStyle == ModernCheckVisual.Switch)
            {
                int h = SwitchHeight;
                int y = (Height - h) / 2;
                return new Rectangle(0, y, SwitchWidth, h);
            }
            else
            {
                int y = (Height - BoxSize) / 2;
                return new Rectangle(0, y, BoxSize, BoxSize);
            }
        }

        private void DrawBoxStyle(Graphics g, Rectangle r)
        {
            using (var b = new SolidBrush(_hover ? _glyphBackHover : _glyphBack))
                g.FillRectangle(b, r);
            using (var p = new Pen(_border))
                g.DrawRectangle(p, r);

            if (_animProgress > 0)
            {
                using var pen = new Pen(AccentEvaluated(), 2.2f)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                };
                int x1 = r.X + (int)(r.Width * 0.23);
                int y1 = r.Y + (int)(r.Height * 0.55);
                int x2 = r.X + (int)(r.Width * 0.45);
                int y2 = r.Y + (int)(r.Height * 0.72);
                int x3 = r.X + (int)(r.Width * 0.77);
                int y3 = r.Y + (int)(r.Height * 0.30);

                double p = _animProgress;
                if (p < 0.5)
                {
                    double t = p / 0.5;
                    int mx = x1 + (int)((x2 - x1) * t);
                    int my = y1 + (int)((y2 - y1) * t);
                    g.DrawLine(pen, x1, y1, mx, my);
                }
                else
                {
                    g.DrawLine(pen, x1, y1, x2, y2);
                    double t = (p - 0.5) / 0.5;
                    int mx = x2 + (int)((x3 - x2) * t);
                    int my = y2 + (int)((y3 - y2) * t);
                    g.DrawLine(pen, x2, y2, mx, my);
                }
            }
        }

        private void DrawSwitchStyle(Graphics g, Rectangle r)
        {
            //int radius = r.Height / 2;
            using (var path = new GraphicsPath())
            {
                path.AddArc(r.X, r.Y, r.Height, r.Height, 90, 180);
                path.AddArc(r.Right - r.Height, r.Y, r.Height, r.Height, 270, 180);
                path.CloseFigure();

                Color track = _glyphBack;
                if (_hover) track = _glyphBackHover;
                if (_animProgress > 0.5)
                {
                    // Blend accent as it moves
                    track = Blend(track, AccentEvaluated(), (_animProgress - 0.5) * 2);
                }

                using var tb = new SolidBrush(track);
                g.FillPath(tb, path);
                using var pb = new Pen(_border);
                g.DrawPath(pb, path);
            }

            int thumbDiameter = r.Height - 4;
            int minX = r.X + 2;
            int maxX = r.Right - 2 - thumbDiameter;
            int thumbX = minX + (int)((maxX - minX) * _animProgress);
            var thumb = new Rectangle(thumbX, r.Y + 2, thumbDiameter, thumbDiameter);
            using (var sb = new SolidBrush(AccentEvaluated()))
                g.FillEllipse(sb, thumb);
            using var pen = new Pen(Color.FromArgb(60, Color.Black));
            g.DrawEllipse(pen, thumb);
        }

        private void DrawLabel(Graphics g, Rectangle glyph)
        {
            int textOffset = glyph.Right + 8;
            var rect = new Rectangle(textOffset, 0, Width - textOffset - 2, Height);

            TextRenderer.DrawText(g, Text, Font, rect, _text,
                TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        private static Color Blend(Color a, Color b, double t)
        {
            t = Math.Max(0, Math.Min(1, t));
            int r = a.R + (int)((b.R - a.R) * t);
            int g = a.G + (int)((b.G - a.G) * t);
            int bb = a.B + (int)((b.B - a.B) * t);
            return Color.FromArgb(r, g, bb);
        }
    }
}