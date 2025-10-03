using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WaifuAI.Controls
{
    [ToolboxItem(true)]
    [DefaultProperty(nameof(Text))]
    [DefaultEvent(nameof(ExpandedChanged))]
    public class CollapsibleGroupBox : ContainerControl, IThemeAware
    {
        private const int HeaderHeightConst = 28;

        private Rectangle _glyphRect;
        private bool _hover;
        private bool _mouseDown;
        private int _expandedHeight;
        private int _animStart;
        private int _animTarget;
        private readonly Stopwatch _sw = new();
        private readonly Timer _animTimer;

        private bool _expanded = true;
        private int _animationDuration = 140;
        private bool _animate = true;
        private bool _isAnimating;

        // Cached theme colors
        private Color _headerBack;
        private Color _headerHover;
        private Color _panel;
        private Color _border;
        private Color _accent;
        private Color _accentHover;
        private Color _text;

        public CollapsibleGroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true);

            Padding = new Padding(12, HeaderHeightConst + 4, 12, 10);
            Size = new Size(300, 160);
            TabStop = true;

            _animTimer = new Timer { Interval = 15 };
            _animTimer.Tick += AnimTick;

            ThemeManager.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.CurrentTheme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
                _animTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnThemeChanged(object? sender, ThemeManager.Theme t) => ApplyTheme(t);

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded == value) return;
                if (!CanCollapse && !value) return;
                _expanded = value;
                BeginAnimate();
                ExpandedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool CanCollapse { get; set; } = true;

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Animate
        {
            get => _animate;
            set => _animate = value;
        }

        [Category("Behavior")]
        [DefaultValue(140)]
        public int AnimationDuration
        {
            get => _animationDuration;
            set => _animationDuration = Math.Max(16, value);
        }

        [Category("Behavior")]
        [DefaultValue(2)]
        public int CollapsedPaddingHeight { get; set; } = 2;

        [Category("Appearance")]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(HeaderRect); } }
        }

        public event EventHandler? ExpandedChanged;
        public event EventHandler? AnimationFinished;

        private Rectangle HeaderRect => new(0, 0, Width - 1, HeaderHeightConst);

        public void ApplyTheme(ThemeManager.Theme t)
        {
            _panel = t.Panel;
            _headerBack = t.GlyphBack;
            _headerHover = t.GlyphBackHover;
            _border = t.Border;
            _accent = t.Accent;
            _accentHover = t.AccentHover;
            _text = t.TextPrimary;
            if (Font.Bold || Font.Italic)
                Font = new Font(t.Base, Font.Style);
            else
                Font = t.Base;
            BackColor = _panel;
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!DesignMode)
                _expandedHeight = Height;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            // If we resize externally while expanded (not animating), ensure dock recalculated
            if (!_isAnimating && _expanded)
                RelayoutDockedChildren();
        }

        private void RelayoutDockedChildren()
        {
            if (!IsHandleCreated) return;
            SuspendLayout();
            ResumeLayout(true);
            PerformLayout();
            // Force each docked child to invalidate in case it depends on client rect
            foreach (Control c in Controls)
            {
                if (c.Dock != DockStyle.None && c.Visible)
                    c.Invalidate();
            }
            // Also ask parent to re-layout in case stacking container depends on our new size
            Parent?.PerformLayout();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool h = HeaderRect.Contains(e.Location);
            if (h != _hover)
            {
                _hover = h;
                Invalidate(HeaderRect);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_hover)
            {
                _hover = false;
                Invalidate(HeaderRect);
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && HeaderRect.Contains(e.Location))
                _mouseDown = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_mouseDown && e.Button == MouseButtons.Left && HeaderRect.Contains(e.Location))
                Expanded = !Expanded;
            _mouseDown = false;
            base.OnMouseUp(e);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Space || keyData == Keys.Enter)
                return true;
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Space or Keys.Enter)
            {
                Expanded = !Expanded;
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void BeginAnimate()
        {
            if (!_animate || DesignMode)
            {
                if (_expanded)
                {
                    Height = _expandedHeight <= HeaderHeightConst ? 160 : _expandedHeight;
                    foreach (Control c in Controls) c.Visible = true;
                    RelayoutDockedChildren();
                }
                else
                {
                    _expandedHeight = Height;
                    foreach (Control c in Controls) c.Visible = false;
                    Height = HeaderHeightConst + CollapsedPaddingHeight;
                }
                Invalidate();
                AnimationFinished?.Invoke(this, EventArgs.Empty);
                return;
            }

            _isAnimating = true;

            if (_expanded)
            {
                foreach (Control c in Controls) c.Visible = true;
                _animStart = Height;
                if (_expandedHeight < _animStart)
                    _expandedHeight = _animStart;
                _animTarget = _expandedHeight;
                // Immediately layout now that children became visible again
                RelayoutDockedChildren();
            }
            else
            {
                _expandedHeight = Height;
                _animStart = Height;
                _animTarget = HeaderHeightConst + CollapsedPaddingHeight;
            }

            _sw.Restart();
            _animTimer.Start();
        }

        private void AnimTick(object? sender, EventArgs e)
        {
            double raw = _sw.Elapsed.TotalMilliseconds / _animationDuration;
            if (raw >= 1) raw = 1;
            double t = raw < 0.5 ? 2 * raw * raw : -1 + (4 - 2 * raw) * raw;

            int h = (int)(_animStart + (_animTarget - _animStart) * t);
            // Avoid too many nested layout recalcs; just set size
            Height = h;

            if (raw >= 1)
            {
                _animTimer.Stop();
                _isAnimating = false;
                if (!_expanded)
                {
                    foreach (Control c in Controls)
                        c.Visible = false;
                }
                else
                {
                    // After finishing expansion ensure docking adjusts final positions
                    RelayoutDockedChildren();
                }
                Invalidate();
                AnimationFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(_panel);

            using (var hb = new SolidBrush(_hover ? _headerHover : _headerBack))
                g.FillRectangle(hb, HeaderRect);

            _glyphRect = new Rectangle(8, (HeaderHeightConst - 16) / 2, 16, 16);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var caccent = Enabled ? _accent : ThemeManager.Lighten(_accent, 0.5);
            var caccenth = Enabled ? _accentHover : ThemeManager.Lighten(_accentHover, 0.5);

            using (var p = new Pen(_hover ? caccenth : caccent, 2f)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            })
            {
                if (Expanded)
                {
                    g.DrawLines(p,
                    [
                        new Point(_glyphRect.X + 3, _glyphRect.Y + 6),
                        new Point(_glyphRect.X + _glyphRect.Width/2, _glyphRect.Bottom - 4),
                        new Point(_glyphRect.Right - 3, _glyphRect.Y + 6)
                    ]);
                }
                else
                {
                    g.DrawLines(p,
                    [
                        new Point(_glyphRect.X + 5, _glyphRect.Y + 3),
                        new Point(_glyphRect.Right - 5, _glyphRect.Y + _glyphRect.Height/2),
                        new Point(_glyphRect.X + 5, _glyphRect.Bottom - 3)
                    ]);
                }
            }

            TextRenderer.DrawText(g, Text, Font,
                new Rectangle(_glyphRect.Right + 6, 0, Width - _glyphRect.Right - 12, HeaderHeightConst),
                _text, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            if (Focused)
                ControlPaint.DrawFocusRectangle(g,
                    new Rectangle(2, 2, Width - 5, HeaderHeightConst - 4),
                    _accentHover, _headerBack);

            using var bp = new Pen(_border);
            g.DrawRectangle(bp, 0, 0, Width - 1, Height - 1);
        }
    }
}