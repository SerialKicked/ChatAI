using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WaifuAI.Controls
{
    public enum CollapseGlyphStyle
    {
        PlusMinus,
        Chevron
    }

    [ToolboxItem(true)]
    [DefaultProperty(nameof(Text))]
    [DefaultEvent(nameof(ExpandedChanged))]
    public class CollapsibleGroupBox : ContainerControl
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

        private Color _headerBackColor = Color.FromArgb(45, 46, 50);
        private Color _headerHoverColor = Color.FromArgb(55, 56, 60);
        private Color _accentColor = Color.FromArgb(0x4D, 0xA3, 0xFF);
        private Color _borderColor = Color.FromArgb(55, 56, 61);
        private Color _panelBackColor = Color.FromArgb(37, 38, 42);
        private CollapseGlyphStyle _glyphStyle = CollapseGlyphStyle.PlusMinus;

        public CollapsibleGroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true);

            BackColor = _panelBackColor;
            Padding = new Padding(12, HeaderHeightConst + 4, 12, 10);
            Size = new Size(300, 160);
            TabStop = true;

            _animTimer = new Timer { Interval = 15 };
            _animTimer.Tick += AnimTick;
        }

        // ========== Properties ==========

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded == value) return;
                _expanded = value;
                BeginAnimate();
                ExpandedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Animate { get; set; } = true;

        [Category("Behavior")]
        [DefaultValue(140)]
        public int AnimationDuration
        {
            get => _animationDuration;
            set => _animationDuration = Math.Max(16, value);
        }

        [Category("Appearance")]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(HeaderRect); } }
        }

        [Category("Appearance")]
        public Color HeaderBackColor
        {
            get => _headerBackColor;
            set { if (_headerBackColor != value) { _headerBackColor = value; Invalidate(HeaderRect); } }
        }
        public bool ShouldSerializeHeaderBackColor() => _headerBackColor != Color.FromArgb(45, 46, 50);
        public void ResetHeaderBackColor() => HeaderBackColor = Color.FromArgb(45, 46, 50);

        [Category("Appearance")]
        public Color HeaderHoverColor
        {
            get => _headerHoverColor;
            set { if (_headerHoverColor != value) { _headerHoverColor = value; Invalidate(HeaderRect); } }
        }
        public bool ShouldSerializeHeaderHoverColor() => _headerHoverColor != Color.FromArgb(55, 56, 60);
        public void ResetHeaderHoverColor() => HeaderHoverColor = Color.FromArgb(55, 56, 60);

        [Category("Appearance")]
        public Color AccentColor
        {
            get => _accentColor;
            set { if (_accentColor != value) { _accentColor = value; Invalidate(_glyphRect); } }
        }
        public bool ShouldSerializeAccentColor() => _accentColor != Color.FromArgb(0x4D, 0xA3, 0xFF);
        public void ResetAccentColor() => AccentColor = Color.FromArgb(0x4D, 0xA3, 0xFF);

        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set { if (_borderColor != value) { _borderColor = value; Invalidate(); } }
        }
        public bool ShouldSerializeBorderColor() => _borderColor != Color.FromArgb(55, 56, 61);
        public void ResetBorderColor() => BorderColor = Color.FromArgb(55, 56, 61);

        [Category("Appearance")]
        public Color PanelBackColor
        {
            get => _panelBackColor;
            set { if (_panelBackColor != value) { _panelBackColor = value; Invalidate(); } }
        }
        public bool ShouldSerializePanelBackColor() => _panelBackColor != Color.FromArgb(37, 38, 42);
        public void ResetPanelBackColor() => PanelBackColor = Color.FromArgb(37, 38, 42);

        [Category("Appearance")]
        [DefaultValue(CollapseGlyphStyle.PlusMinus)]
        public CollapseGlyphStyle GlyphStyle
        {
            get => _glyphStyle;
            set { if (_glyphStyle != value) { _glyphStyle = value; Invalidate(_glyphRect); } }
        }

        [Category("Behavior")]
        [DefaultValue(2)]
        public int CollapsedPaddingHeight { get; set; } = 2;

        [Category("Behavior")]
        [Description("Optional persistence key you can use externally to save/restore Expanded state.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PersistenceKey { get; set; } = string.Empty;

        // ========== Events ==========

        [Category("Behavior")]
        public event EventHandler? ExpandedChanged;

        [Category("Behavior")]
        public event EventHandler? AnimationFinished;

        // ========== Public API convenience ==========

        public void Collapse() => Expanded = false;
        public void Expand() => Expanded = true;
        public void Toggle() => Expanded = !Expanded;

        // ========== Internal Helpers ==========

        private Rectangle HeaderRect => new(0, 0, Width - 1, HeaderHeightConst);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!DesignMode)
                _expandedHeight = Height;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool h = _glyphRect.Contains(e.Location) || HeaderRect.Contains(e.Location);
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
            {
                _mouseDown = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_mouseDown && e.Button == MouseButtons.Left && HeaderRect.Contains(e.Location))
            {
                Toggle();
            }
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
            if (e.KeyCode is Keys.Space or Keys.Return)
            {
                Toggle();
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void BeginAnimate()
        {
            if (!Animate || DesignMode)
            {
                if (_expanded)
                {
                    Height = _expandedHeight <= HeaderHeightConst ? 160 : _expandedHeight;
                    foreach (Control c in Controls) c.Visible = true;
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

            if (_expanded)
            {
                foreach (Control c in Controls) c.Visible = true;
                _animStart = Height;
                if (_expandedHeight < _animStart)
                    _expandedHeight = _animStart;
                _animTarget = _expandedHeight;
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
            double raw = _sw.Elapsed.TotalMilliseconds / AnimationDuration;
            if (raw >= 1) raw = 1;
            double t = raw < 0.5 ? 2 * raw * raw : -1 + (4 - 2 * raw) * raw;

            int h = (int)(_animStart + (_animTarget - _animStart) * t);
            SuspendLayout();
            Height = h;
            ResumeLayout(false);

            if (raw >= 1)
            {
                _animTimer.Stop();
                if (!_expanded)
                {
                    foreach (Control c in Controls)
                        c.Visible = false;
                }
                AnimationFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(PanelBackColor);

            // Header
            using (var hb = new SolidBrush(_hover ? _headerHoverColor : _headerBackColor))
                g.FillRectangle(hb, HeaderRect);

            // Focus rectangle (keyboard accessibility)
            if (Focused)
            {
                var focusRect = new Rectangle(2, 2, Width - 5, HeaderHeightConst - 4);
                ControlPaint.DrawFocusRectangle(g, focusRect, Color.White, _headerHoverColor);
            }

            // Glyph
            _glyphRect = new Rectangle(8, (HeaderHeightConst - 16) / 2, 16, 16);
            DrawGlyph(g, _glyphRect);

            // Text
            TextRenderer.DrawText(g, Text, Font,
                new Rectangle(_glyphRect.Right + 6, 0, Width - _glyphRect.Right - 12, HeaderHeightConst),
                Color.White,
                TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            // Border
            using (var pen = new Pen(_borderColor))
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        private void DrawGlyph(Graphics g, Rectangle r)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_glyphStyle == CollapseGlyphStyle.PlusMinus)
            {
                using var p = new Pen(_accentColor, 2f)
                {
                    StartCap = System.Drawing.Drawing2D.LineCap.Round,
                    EndCap = System.Drawing.Drawing2D.LineCap.Round
                };
                int cx = r.X + r.Width / 2;
                int cy = r.Y + r.Height / 2;
                g.DrawLine(p, cx - 5, cy, cx + 5, cy);
                if (!Expanded)
                    g.DrawLine(p, cx, cy - 5, cx, cy + 5);
            }
            else
            {
                // Chevron
                using var p = new Pen(_accentColor, 2f)
                {
                    StartCap = System.Drawing.Drawing2D.LineCap.Round,
                    EndCap = System.Drawing.Drawing2D.LineCap.Round
                };
                // Direction depends on state
                if (Expanded)
                {
                    // Down
                    g.DrawLines(p, new[]
                    {
                        new Point(r.X + 3, r.Y + 6),
                        new Point(r.X + r.Width/2, r.Y + r.Height - 4),
                        new Point(r.Right - 3, r.Y + 6)
                    });
                }
                else
                {
                    // Right
                    g.DrawLines(p, new[]
                    {
                        new Point(r.X + 5, r.Y + 3),
                        new Point(r.Right - 5, r.Y + r.Height/2),
                        new Point(r.X + 5, r.Bottom - 3)
                    });
                }
            }
        }
    }
}