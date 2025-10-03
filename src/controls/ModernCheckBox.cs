using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WaifuAI.Controls
{
    public enum ModernCheckVisual
    {
        Box,
        Switch
    }

    [ToolboxItem(true)]
    [DefaultProperty(nameof(Text))]
    [DefaultEvent(nameof(CheckedChanged))]
    public class ModernCheckBox : CheckBox
    {
        private const int BoxSize = 18;
        private const int SwitchWidth = 42;
        private const int SwitchHeight = 22;

        // Animation state
        private readonly Stopwatch _animWatch = new();
        private Timer? _animTimer;
        private double _animStartValue;
        private double _animTargetValue;
        private double _animProgress; // 0..1 eased
        private double _rawT;
        private bool _hover;
        private bool _mouseDown;
        private Point _lastMouse;
        private DateTime _lastRippleStart;
        private bool _rippleActive;

        // Cached fonts/metrics
        private StringFormat? _sf;

        // Backing fields
        private Color _accentColor = Color.FromArgb(0x4D, 0xA3, 0xFF);
        private Color _accentHoverColor = Color.FromArgb(0x60, 0xB4, 0xFF);
        private Color _borderColor = Color.FromArgb(70, 72, 78);
        private Color _backColorFill = Color.FromArgb(37, 38, 42);
        private Color _glyphBack = Color.FromArgb(45, 46, 50);
        private Color _glyphBackHover = Color.FromArgb(55, 56, 60);
        private Color _textColor = Color.White;

        private bool _useAnimation = true;
        private int _animationDuration = 140;
        private bool _useRipple = true;
        private int _rippleDuration = 280;
        private ModernCheckVisual _visualStyle = ModernCheckVisual.Box;
        private bool _drawFocusRect = true;

        public ModernCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            TabStop = true;
            AutoSize = false;
            Font = new Font("Segoe UI", 9f);
            Size = new Size(150, 26);
            _animProgress = Checked ? 1.0 : 0.0;
            _animTargetValue = _animProgress;
        }

        // ---------------------
        //  Public Properties
        // ---------------------

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "77,163,255")]
        public Color AccentColor
        {
            get => _accentColor;
            set { if (_accentColor != value) { _accentColor = value; Invalidate(); } }
        }
        public bool ShouldSerializeAccentColor() => _accentColor != Color.FromArgb(0x4D, 0xA3, 0xFF);
        public void ResetAccentColor() => AccentColor = Color.FromArgb(0x4D, 0xA3, 0xFF);

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "96,180,255")]
        public Color AccentHoverColor
        {
            get => _accentHoverColor;
            set { if (_accentHoverColor != value) { _accentHoverColor = value; Invalidate(); } }
        }
        public bool ShouldSerializeAccentHoverColor() => _accentHoverColor != Color.FromArgb(0x60, 0xB4, 0xFF);
        public void ResetAccentHoverColor() => AccentHoverColor = Color.FromArgb(0x60, 0xB4, 0xFF);

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "70,72,78")]
        public Color BorderColor
        {
            get => _borderColor;
            set { if (_borderColor != value) { _borderColor = value; Invalidate(); } }
        }
        public bool ShouldSerializeBorderColor() => _borderColor != Color.FromArgb(70, 72, 78);
        public void ResetBorderColor() => BorderColor = Color.FromArgb(70, 72, 78);

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "37,38,42")]
        public Color PanelBackColor
        {
            get => _backColorFill;
            set { if (_backColorFill != value) { _backColorFill = value; Invalidate(); } }
        }
        public bool ShouldSerializePanelBackColor() => _backColorFill != Color.FromArgb(37, 38, 42);
        public void ResetPanelBackColor() => PanelBackColor = Color.FromArgb(37, 38, 42);

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "45,46,50")]
        public Color GlyphBackColor
        {
            get => _glyphBack;
            set { if (_glyphBack != value) { _glyphBack = value; Invalidate(); } }
        }
        public bool ShouldSerializeGlyphBackColor() => _glyphBack != Color.FromArgb(45, 46, 50);
        public void ResetGlyphBackColor() => GlyphBackColor = Color.FromArgb(45, 46, 50);

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "55,56,60")]
        public Color GlyphBackHoverColor
        {
            get => _glyphBackHover;
            set { if (_glyphBackHover != value) { _glyphBackHover = value; Invalidate(); } }
        }
        public bool ShouldSerializeGlyphBackHoverColor() => _glyphBackHover != Color.FromArgb(55, 56, 60);
        public void ResetGlyphBackHoverColor() => GlyphBackHoverColor = Color.FromArgb(55, 56, 60);

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "White")]
        public Color TextColor
        {
            get => _textColor;
            set { if (_textColor != value) { _textColor = value; Invalidate(); } }
        }
        public bool ShouldSerializeTextColor() => _textColor != Color.White;
        public void ResetTextColor() => TextColor = Color.White;

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
        [DefaultValue(true)]
        public bool UseAnimation
        {
            get => _useAnimation;
            set { _useAnimation = value; }
        }

        [Category("Behavior")]
        [DefaultValue(140)]
        public int AnimationDuration
        {
            get => _animationDuration;
            set => _animationDuration = Math.Max(16, value);
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool UseRipple
        {
            get => _useRipple;
            set { _useRipple = value; Invalidate(); }
        }

        [Category("Behavior")]
        [DefaultValue(280)]
        public int RippleDuration
        {
            get => _rippleDuration;
            set => _rippleDuration = Math.Max(60, value);
        }

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool DrawFocusRectangle
        {
            get => _drawFocusRect;
            set { if (_drawFocusRect != value) { _drawFocusRect = value; Invalidate(); } }
        }

        // ---------------------
        //  Overridden Standard
        // ---------------------

        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animTimer?.Dispose();
                _sf?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (_sf == null)
            {
                _sf = new StringFormat(StringFormatFlags.NoWrap)
                {
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
            }
        }

        // ---------------------
        //  Input / State
        // ---------------------

        protected override void OnMouseEnter(EventArgs e)
        {
            _hover = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hover = false;
            _mouseDown = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _lastMouse = e.Location;
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDown = true;
                if (_useRipple)
                {
                    _rippleActive = true;
                    _lastRippleStart = DateTime.UtcNow;
                }
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_mouseDown && e.Button == MouseButtons.Left)
            {
                _mouseDown = false;
                // base.OnClick triggers Checked change (since CheckBox handles it),
                // but we still produce ripple & redraw
                Invalidate();
            }
            base.OnMouseUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Space or Keys.Enter)
            {
                if (_useRipple)
                {
                    _rippleActive = true;
                    _lastRippleStart = DateTime.UtcNow;
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            StartAnimation(Checked);
            base.OnCheckedChanged(e);
        }

        protected override void OnCheckStateChanged(EventArgs e)
        {
            // For indeterminate state, animate to mid (0.5)
            if (ThreeState && CheckState == CheckState.Indeterminate)
                StartAnimation(null);
            else
                StartAnimation(Checked);
            base.OnCheckStateChanged(e);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Space || keyData == Keys.Enter)
                return true;
            return base.IsInputKey(keyData);
        }

        // ---------------------
        //  Animation
        // ---------------------

        private void EnsureTimer()
        {
            if (_animTimer != null) return;
            _animTimer = new Timer { Interval = 15 };
            _animTimer.Tick += (_, _) => AnimateTick();
        }

        private void StartAnimation(bool? isChecked)
        {
            if (!_useAnimation || DesignMode)
            {
                _animProgress = isChecked == null ? 0.5 : (isChecked.Value ? 1.0 : 0.0);
                Invalidate();
                return;
            }

            EnsureTimer();
            _animStartValue = _animProgress;
            _animTargetValue = isChecked == null ? 0.5 : (isChecked.Value ? 1.0 : 0.0);
            _animWatch.Restart();
            _animTimer!.Start();
        }

        private void AnimateTick()
        {
            double dur = _animationDuration;
            _rawT = Math.Min(1.0, _animWatch.Elapsed.TotalMilliseconds / dur);
            // smooth ease in/out
            double t = _rawT < 0.5 ? 2 * _rawT * _rawT : -1 + (4 - 2 * _rawT) * _rawT;
            _animProgress = _animStartValue + (_animTargetValue - _animStartValue) * t;
            Invalidate();

            if (_rawT >= 1.0)
            {
                _animTimer?.Stop();
                _animProgress = _animTargetValue;
                Invalidate();
            }
        }

        // ---------------------
        //  Painting
        // ---------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent?.BackColor ?? _backColorFill);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle glyphRect = GetGlyphRectangle();
            DrawGlyph(g, glyphRect);
            DrawTextAndFocus(g, glyphRect);
            DrawRipple(g, glyphRect);
        }

        private Rectangle GetGlyphRectangle()
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

        private void DrawGlyph(Graphics g, Rectangle r)
        {
            if (_visualStyle == ModernCheckVisual.Box)
            {
                // background
                Color bg = _hover || Focused ? _glyphBackHover : _glyphBack;
                using (var b = new SolidBrush(bg))
                    g.FillRectangle(b, r);

                using (var pen = new Pen(_borderColor))
                    g.DrawRectangle(pen, r);

                // Indeterminate / Check mark
                double p = _animProgress;
                if (p > 0)
                {
                    // if tri-state midpoint ~0.5 treat as indeterminate
                    if (ThreeState && Math.Abs(p - 0.5) < 0.15)
                    {
                        int pad = 4;
                        var mid = new Rectangle(r.X + pad, r.Y + pad, r.Width - pad * 2, r.Height - pad * 2);
                        using var ib = new SolidBrush(AccentEvaluated());
                        g.FillRectangle(ib, mid);
                    }
                    else
                    {
                        // animate stroke thickness or path length
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

                        // Draw partial segments based on progress
                        if (p < 0.5)
                        {
                            // first leg
                            double segT = p / 0.5;
                            int mx = x1 + (int)((x2 - x1) * segT);
                            int my = y1 + (int)((y2 - y1) * segT);
                            g.DrawLine(pen, x1, y1, mx, my);
                        }
                        else
                        {
                            g.DrawLine(pen, x1, y1, x2, y2);
                            double segT = (p - 0.5) / 0.5;
                            int mx = x2 + (int)((x3 - x2) * segT);
                            int my = y2 + (int)((y3 - y2) * segT);
                            g.DrawLine(pen, x2, y2, mx, my);
                        }
                    }
                }
            }
            else
            {
                // Switch style
                // Track
                double p = _animProgress;
                // For tri-state: map 0..0.5..1   middle=indeterminate tinted track
                bool isIndet = ThreeState && Math.Abs(p - 0.5) < 0.15;

                var trackRect = r;
                int radius = trackRect.Height / 2;
                using (var path = RoundedRect(trackRect, radius))
                {
                    Color trackColor = isIndet
                        ? BlendColors(_glyphBack, AccentEvaluated(), 0.35)
                        : (_hover ? _glyphBackHover : _glyphBack);

                    if (p > 0.5) // lighten as it turns on
                        trackColor = BlendColors(trackColor, AccentEvaluated(), (p - 0.5) * 1.4);

                    using var tb = new SolidBrush(trackColor);
                    g.FillPath(tb, path);
                    using var tp = new Pen(_borderColor, 1);
                    g.DrawPath(tp, path);
                }

                // Thumb
                int thumbDiameter = trackRect.Height - 4;
                double logical = isIndet ? 0.5 : p;
                int minX = trackRect.X + 2;
                int maxX = trackRect.Right - 2 - thumbDiameter;
                int thumbX = minX + (int)((maxX - minX) * logical);
                var thumbRect = new Rectangle(thumbX, trackRect.Y + 2, thumbDiameter, thumbDiameter);

                using (var sb = new SolidBrush(AccentEvaluated()))
                {
                    g.FillEllipse(sb, thumbRect);
                }
                using (var pen = new Pen(Color.FromArgb(60, Color.Black), 1))
                    g.DrawEllipse(pen, thumbRect);
            }
        }

        private void DrawTextAndFocus(Graphics g, Rectangle glyphRect)
        {
            int textOffset = glyphRect.Right + 8;
            var textRect = new Rectangle(textOffset, 0, Width - textOffset - 2, Height);

            using var tb = new SolidBrush(_textColor);
            TextRenderer.DrawText(
                g,
                Text,
                Font,
                textRect,
                _textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

            if (Focused && _drawFocusRect)
            {
                var fr = new Rectangle(textRect.X, textRect.Y + 4, textRect.Width, textRect.Height - 8);
                ControlPaint.DrawFocusRectangle(g, fr, _textColor, Color.Transparent);
            }
        }

        private void DrawRipple(Graphics g, Rectangle glyphRect)
        {
            if (!_useRipple || !_rippleActive) return;

            double elapsed = (DateTime.UtcNow - _lastRippleStart).TotalMilliseconds;
            if (elapsed > _rippleDuration)
            {
                _rippleActive = false;
                return;
            }

            double t = elapsed / _rippleDuration; // 0..1
            // accelerate fade
            double alphaT = 1.0 - t;
            int alpha = (int)(alphaT * 80);

            // center ripple on glyph or last mouse within glyph zone
            Point origin;
            if (glyphRect.Contains(_lastMouse))
                origin = _lastMouse;
            else
                origin = new Point(glyphRect.X + glyphRect.Width / 2, glyphRect.Y + glyphRect.Height / 2);

            double maxRadius = Math.Max(glyphRect.Width, glyphRect.Height) * 1.6;
            double radius = maxRadius * t;

            using var rp = new SolidBrush(Color.FromArgb(alpha, AccentEvaluated()));
            g.SetClip(glyphRect);
            g.FillEllipse(rp, (float)(origin.X - radius), (float)(origin.Y - radius),
                (float)(radius * 2), (float)(radius * 2));
            g.ResetClip();

            Invalidate(glyphRect);
        }

        // ---------------------
        //  Helpers
        // ---------------------

        private Color AccentEvaluated()
        {
            if (_hover || _mouseDown)
                return _accentHoverColor;
            return _accentColor;
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static Color BlendColors(Color a, Color b, double t)
        {
            t = Math.Min(1, Math.Max(0, t));
            int r = a.R + (int)((b.R - a.R) * t);
            int g = a.G + (int)((b.G - a.G) * t);
            int bb = a.B + (int)((b.B - a.B) * t);
            int aa = a.A + (int)((b.A - a.A) * t);
            return Color.FromArgb(aa, r, g, bb);
        }

        // Allow user to request re-synchronization (e.g., after programmatic bulk changes)
        public void SyncVisualImmediate()
        {
            _animProgress = ThreeState && CheckState == CheckState.Indeterminate
                ? 0.5
                : (Checked ? 1.0 : 0.0);
            _animTargetValue = _animProgress;
            Invalidate();
        }
    }
}