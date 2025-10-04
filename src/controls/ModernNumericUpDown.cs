using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaifuAI.Controls
{
    [ToolboxItem(true)]
    [DefaultProperty(nameof(Value))]
    [DefaultEvent(nameof(ValueChanged))]
    public class ModernNumericUpDown : UserControl, IThemeAware
    {
        private readonly TextBox _textBox;
        private readonly Panel _buttonPanel;
        private readonly Panel _innerPanel;

        private decimal _value = 0;
        private decimal _minimum = 0;
        private decimal _maximum = 100;
        private decimal _increment = 1;
        private int _decimalPlaces = 0;
        private bool _thousandsSeparator = false;
        private bool _hexadecimal = false;

        private Rectangle _upButtonRect;
        private Rectangle _downButtonRect;
        private bool _upButtonHovered;
        private bool _downButtonHovered;
        private bool _upButtonPressed;
        private bool _downButtonPressed;

        private System.Windows.Forms.Timer? _repeatTimer;
        private bool _repeatUp;
        private int _repeatDelay = 500;
        private int _repeatSpeed = 50;

        // Theme colors
        private Color _panel;
        private Color _border;
        private Color _accent;
        private Color _accentHover;
        private Color _text;
        private Color _textMuted;
        private Color _glyphBack;
        private Color _glyphBackHover;

        public ModernNumericUpDown()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            // Create text box with NO border
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill
            };
            _textBox.KeyPress += TextBox_KeyPress;
            _textBox.KeyDown += TextBox_KeyDown;
            _textBox.Leave += TextBox_Leave;
            _textBox.MouseWheel += TextBox_MouseWheel;

            // Create button panel
            _buttonPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 18
            };
            _buttonPanel.Paint += ButtonPanel_Paint;
            _buttonPanel.MouseMove += ButtonPanel_MouseMove;
            _buttonPanel.MouseLeave += ButtonPanel_MouseLeave;
            _buttonPanel.MouseDown += ButtonPanel_MouseDown;
            _buttonPanel.MouseUp += ButtonPanel_MouseUp;

            // Container panel for textbox with padding
            _innerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(6, 4, 4, 4)
            };
            _innerPanel.Controls.Add(_textBox);

            Controls.Add(_innerPanel);
            Controls.Add(_buttonPanel);

            Size = new Size(120, 24);
            Padding = new Padding(1); // Border width

            ThemeManager.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.CurrentTheme);

            UpdateTextBox();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
                _repeatTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnThemeChanged(object? sender, ThemeManager.Theme t) => ApplyTheme(t);

        public void ApplyTheme(ThemeManager.Theme t)
        {
            _panel = t.Panel;
            _border = t.Border;
            _accent = t.Accent;
            _accentHover = t.AccentHover;
            _text = t.TextPrimary;
            _textMuted = t.TextMuted;
            _glyphBack = t.GlyphBack;
            _glyphBackHover = t.GlyphBackHover;

            Font = t.Base;
            BackColor = _border; // Border via background
            _innerPanel.BackColor = _panel;
            _textBox.BackColor = _panel;
            _textBox.ForeColor = _text;
            _textBox.Font = t.Base;
            _buttonPanel.BackColor = _glyphBack;

            Invalidate();
        }

        // =========================================================
        // Properties matching standard NumericUpDown
        // =========================================================

        [Category("Data")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Value
        {
            get => _value;
            set
            {
                decimal newValue = Constrain(value);
                if (_value != newValue)
                {
                    _value = newValue;
                    UpdateTextBox();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Data")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_value < _minimum)
                    Value = _minimum;
            }
        }

        [Category("Data")]
        [DefaultValue(typeof(decimal), "100")]
        public decimal Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_value > _maximum)
                    Value = _maximum;
            }
        }

        [Category("Data")]
        [DefaultValue(typeof(decimal), "1")]
        public decimal Increment
        {
            get => _increment;
            set => _increment = value > 0 ? value : 1;
        }

        [Category("Data")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set
            {
                if (value < 0 || value > 99)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _decimalPlaces = value;
                UpdateTextBox();
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ThousandsSeparator
        {
            get => _thousandsSeparator;
            set
            {
                _thousandsSeparator = value;
                UpdateTextBox();
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Hexadecimal
        {
            get => _hexadecimal;
            set
            {
                _hexadecimal = value;
                UpdateTextBox();
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _textBox.ReadOnly;
            set => _textBox.ReadOnly = value;
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool InterceptArrowKeys { get; set; } = true;

        [Category("Behavior")]
        public event EventHandler? ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        // =========================================================
        // Methods matching standard NumericUpDown
        // =========================================================

        public void UpButton()
        {
            Value = Constrain(_value + _increment);
        }

        public void DownButton()
        {
            Value = Constrain(_value - _increment);
        }

        private decimal Constrain(decimal value)
        {
            if (value < _minimum) return _minimum;
            if (value > _maximum) return _maximum;
            return value;
        }

        private void UpdateTextBox()
        {
            if (_textBox == null) return;

            string text;
            if (_hexadecimal)
            {
                text = ((long)_value).ToString("X");
            }
            else
            {
                string format = _thousandsSeparator ? "N" : "F";
                format += _decimalPlaces.ToString();
                text = _value.ToString(format);
            }

            if (_textBox.Text != text)
                _textBox.Text = text;
        }

        private bool ParseValue(string text, out decimal result)
        {
            result = 0;

            if (string.IsNullOrWhiteSpace(text))
                return false;

            try
            {
                if (_hexadecimal)
                {
                    // Remove common hex prefixes
                    text = text.Replace("0x", "").Replace("0X", "").Trim();
                    result = Convert.ToInt64(text, 16);
                    return true;
                }
                else
                {
                    return decimal.TryParse(text, out result);
                }
            }
            catch
            {
                return false;
            }
        }

        // =========================================================
        // TextBox Events
        // =========================================================

        private void TextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow control characters (backspace, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow digits
            if (char.IsDigit(e.KeyChar))
                return;

            // Allow decimal point if decimal places > 0 and not hex
            if (e.KeyChar == '.' && _decimalPlaces > 0 && !_hexadecimal && !_textBox.Text.Contains("."))
                return;

            // Allow minus sign at start for negative numbers
            if (e.KeyChar == '-' && _textBox.SelectionStart == 0 && _minimum < 0 && !_textBox.Text.Contains("-"))
                return;

            // Allow hex characters
            if (_hexadecimal && "abcdefABCDEFxX".Contains(e.KeyChar))
                return;

            // Suppress all other characters
            e.Handled = true;
        }

        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (InterceptArrowKeys)
            {
                if (e.KeyCode == Keys.Up)
                {
                    UpButton();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    DownButton();
                    e.Handled = true;
                }
            }

            // Enter key commits the value
            if (e.KeyCode == Keys.Enter)
            {
                ValidateAndUpdateValue();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TextBox_Leave(object? sender, EventArgs e)
        {
            ValidateAndUpdateValue();
        }

        private void TextBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e is HandledMouseEventArgs hme)
            {
                if (e.Delta > 0)
                    UpButton();
                else if (e.Delta < 0)
                    DownButton();

                hme.Handled = true;
            }
        }

        private void ValidateAndUpdateValue()
        {
            if (ParseValue(_textBox.Text, out decimal newValue))
            {
                Value = newValue;
            }
            else
            {
                // Reset to current value if parsing fails
                UpdateTextBox();
            }
        }

        // =========================================================
        // Button Panel Events
        // =========================================================

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateButtonRects();
        }

        private void CalculateButtonRects()
        {
            int halfHeight = _buttonPanel.Height / 2;
            _upButtonRect = new Rectangle(0, 0, _buttonPanel.Width, halfHeight);
            _downButtonRect = new Rectangle(0, halfHeight, _buttonPanel.Width, _buttonPanel.Height - halfHeight);
        }

        private void ButtonPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(_glyphBack);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            CalculateButtonRects();

            // Draw up button
            DrawButton(g, _upButtonRect, _upButtonHovered, _upButtonPressed, true);

            // Draw down button
            DrawButton(g, _downButtonRect, _downButtonHovered, _downButtonPressed, false);

            // Draw separator line
            using var pen = new Pen(_border);
            g.DrawLine(pen, 0, _upButtonRect.Bottom, _buttonPanel.Width, _upButtonRect.Bottom);
        }

        private void DrawButton(Graphics g, Rectangle rect, bool hovered, bool pressed, bool isUp)
        {
            // Background
            Color backColor = pressed ? _accentHover : (hovered ? _glyphBackHover : _glyphBack);
            using (var b = new SolidBrush(backColor))
                g.FillRectangle(b, rect);

            // Arrow
            Color arrowColor = pressed || hovered ? _accent : _textMuted;
            using var pen = new Pen(arrowColor, 1.5f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            int cx = rect.X + rect.Width / 2;
            int cy = rect.Y + rect.Height / 2;
            int arrowSize = 4;

            if (isUp)
            {
                // Up arrow: ^
                Point[] points = new[]
                {
                    new Point(cx - arrowSize, cy + 1),
                    new Point(cx, cy - arrowSize + 1),
                    new Point(cx + arrowSize, cy + 1)
                };
                g.DrawLines(pen, points);
            }
            else
            {
                // Down arrow: v
                Point[] points = new[]
                {
                    new Point(cx - arrowSize, cy - 1),
                    new Point(cx, cy + arrowSize - 1),
                    new Point(cx + arrowSize, cy - 1)
                };
                g.DrawLines(pen, points);
            }
        }

        private void ButtonPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            bool upHovered = _upButtonRect.Contains(e.Location);
            bool downHovered = _downButtonRect.Contains(e.Location);

            if (upHovered != _upButtonHovered || downHovered != _downButtonHovered)
            {
                _upButtonHovered = upHovered;
                _downButtonHovered = downHovered;
                _buttonPanel.Invalidate();
            }
        }

        private void ButtonPanel_MouseLeave(object? sender, EventArgs e)
        {
            if (_upButtonHovered || _downButtonHovered)
            {
                _upButtonHovered = false;
                _downButtonHovered = false;
                _buttonPanel.Invalidate();
            }
        }

        private void ButtonPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_upButtonRect.Contains(e.Location))
            {
                _upButtonPressed = true;
                _repeatUp = true;
                UpButton();
                StartRepeatTimer();
                _buttonPanel.Invalidate();
            }
            else if (_downButtonRect.Contains(e.Location))
            {
                _downButtonPressed = true;
                _repeatUp = false;
                DownButton();
                StartRepeatTimer();
                _buttonPanel.Invalidate();
            }
        }

        private void ButtonPanel_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_upButtonPressed || _downButtonPressed)
            {
                _upButtonPressed = false;
                _downButtonPressed = false;
                StopRepeatTimer();
                _buttonPanel.Invalidate();
            }
        }

        // =========================================================
        // Repeat Timer (hold button to continuously increment/decrement)
        // =========================================================

        private void StartRepeatTimer()
        {
            if (_repeatTimer == null)
            {
                _repeatTimer = new System.Windows.Forms.Timer();
                _repeatTimer.Tick += RepeatTimer_Tick;
            }

            _repeatTimer.Interval = _repeatDelay;
            _repeatTimer.Start();
        }

        private void StopRepeatTimer()
        {
            _repeatTimer?.Stop();
        }

        private void RepeatTimer_Tick(object? sender, EventArgs e)
        {
            // After first tick, speed up
            if (_repeatTimer!.Interval == _repeatDelay)
                _repeatTimer.Interval = _repeatSpeed;

            if (_repeatUp)
                UpButton();
            else
                DownButton();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Border is created by BackColor = _border and Padding
            // Everything else is filled by the child controls
        }

        // =========================================================
        // Focus Management
        // =========================================================

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            _textBox.Focus();
        }

        public void Select(int start, int length)
        {
            _textBox.Select(start, length);
        }

        public new void Select()
        {
            _textBox.Select();
            _textBox.SelectAll();
        }
    }
}