using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Markup;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;
using Wpf = System.Windows.Controls;
using WpfInput = System.Windows.Input;
using Media = System.Windows.Media;

namespace WaifuAI.Controls
{
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    [DesignerCategory("Code")]
    public class SpellCheckedTextBox : ElementHost
    {
        private readonly Wpf.TextBox _tb;
        private readonly Wpf.Border _border;

        // Explicit WinForms event surface so "ed_input.KeyPress += ..." compiles and fires
        public new event KeyPressEventHandler? KeyPress;
        public new event KeyEventHandler? KeyDown;
        public new event KeyEventHandler? KeyUp;
        public new event PreviewKeyDownEventHandler? PreviewKeyDown;

        private bool InDesigner =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            (Site?.DesignMode ?? false);

        public SpellCheckedTextBox()
        {
            AllowDrop = true;
            BackColorTransparent = false; // avoid click-through on transparent host

            _tb = new Wpf.TextBox
            {
                AcceptsReturn = false,
                AcceptsTab = false,
                SpellCheck = { IsEnabled = true },
                TextWrapping = TextWrapping.NoWrap,
                VerticalScrollBarVisibility = Wpf.ScrollBarVisibility.Disabled,
                HorizontalScrollBarVisibility = Wpf.ScrollBarVisibility.Disabled
            };

            _border = new Wpf.Border
            {
                BorderBrush = new Media.SolidColorBrush(Media.Colors.Gray),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(0),
                Child = _tb
            };

            Child = _border;

            // Bridge common behaviors/events
            _tb.TextChanged += (_, __) => OnTextChanged(EventArgs.Empty);

            // Bridge keyboard events: PreviewKeyDown -> KeyDown -> KeyPress -> KeyUp
            _tb.PreviewKeyDown += OnWpfPreviewKeyDown;
            _tb.PreviewTextInput += OnWpfPreviewTextInput;   // characters/IME text
            _tb.PreviewKeyUp += OnWpfPreviewKeyUp;

            // Make design-time selection practical right away
            ApplyDesignTimeMode();
            Size = new System.Drawing.Size(200, 23);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tb.PreviewKeyDown -= OnWpfPreviewKeyDown;
                _tb.PreviewTextInput -= OnWpfPreviewTextInput;
                _tb.PreviewKeyUp -= OnWpfPreviewKeyUp;
                // Can't remove the lambda used for TextChanged; harmless on dispose.
            }
            base.Dispose(disposing);
        }

        private static WpfInput.Key EffectiveKey(WpfInput.KeyEventArgs e)
            => e.Key == WpfInput.Key.System ? e.SystemKey : e.Key;

        private static Keys ToWinFormsKeys(WpfInput.Key key)
        {
            // Map WPF key + current modifiers to WinForms Keys
            var vk = System.Windows.Input.KeyInterop.VirtualKeyFromKey(key);
            var result = (Keys)vk;
            var mods = WpfInput.Keyboard.Modifiers;
            if ((mods & WpfInput.ModifierKeys.Shift) != 0) result |= Keys.Shift;
            if ((mods & WpfInput.ModifierKeys.Control) != 0) result |= Keys.Control;
            if ((mods & WpfInput.ModifierKeys.Alt) != 0) result |= Keys.Alt;
            return result;
        }

        private void OnWpfPreviewKeyDown(object? sender, WpfInput.KeyEventArgs e)
        {
            var key = EffectiveKey(e);
            var keyData = ToWinFormsKeys(key);

            // Raise WinForms PreviewKeyDown then KeyDown
            OnPreviewKeyDown(new PreviewKeyDownEventArgs(keyData));
            OnKeyDown(new KeyEventArgs(keyData));

            // Always emit KeyPress for Enter so handlers see it.
            // Suppress WPF default only if someone is listening to KeyPress.
            if (key == WpfInput.Key.Enter)
            {
                var hasSubscribers = KeyPress is not null;
                if (hasSubscribers)
                    e.Handled = true; // consumer will decide (e.Handled) what to do
                OnKeyPress(new KeyPressEventArgs('\r'));
                return;
            }

            // Synthesize WinForms-like KeyPress for control keys
            if (key == WpfInput.Key.Back)
            {
                OnKeyPress(new KeyPressEventArgs('\b'));
            }
            else if (key == WpfInput.Key.Tab && _tb.AcceptsTab)
            {
                OnKeyPress(new KeyPressEventArgs('\t'));
                // Let WPF insert the tab when AcceptsTab = true
            }
        }

        private void OnWpfPreviewTextInput(object? sender, WpfInput.TextCompositionEventArgs e)
        {
            // Raise WinForms KeyPress for each typed character (supports IME/multi-char)
            if (!string.IsNullOrEmpty(e.Text))
            {
                foreach (var ch in e.Text)
                    OnKeyPress(new KeyPressEventArgs(ch));
            }
        }

        private void OnWpfPreviewKeyUp(object? sender, WpfInput.KeyEventArgs e)
        {
            var key = EffectiveKey(e);
            var keyData = ToWinFormsKeys(key);
            OnKeyUp(new KeyEventArgs(keyData));
        }

        // Ensure WinForms events are fired for subscribers
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            KeyDown?.Invoke(this, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            KeyUp?.Invoke(this, e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            KeyPress?.Invoke(this, e);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            PreviewKeyDown?.Invoke(this, e);
        }

        public override ISite? Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                ApplyDesignTimeMode();
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ApplyDesignTimeMode();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            _tb.Focus();
            WpfInput.Keyboard.Focus(_tb);
        }

        private void ApplyDesignTimeMode()
        {
            if (InDesigner)
            {
                // Completely remove WPF content in designer
                Child = null;

                // Disable WPF-related behaviors that might interfere
                _tb.IsHitTestVisible = false;
                _tb.IsEnabled = false;

                // Force WinForms-style behavior for designer
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.Selectable |
                         ControlStyles.StandardClick, true);

                // Make it selectable in designer
                BackColor = Color.FromArgb(32, Color.SteelBlue);
                TabStop = true;
                Cursor = Cursors.Default;
                Invalidate();
            }
            else
            {
                Child ??= _border;
                _tb.IsHitTestVisible = true;
                _tb.IsEnabled = true;
                _tb.IsReadOnly = _readOnly;
                Cursor = Cursors.IBeam;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (InDesigner)
            {
                pevent.Graphics.Clear(Parent?.BackColor ?? System.Drawing.SystemColors.Control);
                using var brush = new SolidBrush(Color.FromArgb(24, Color.SteelBlue));
                pevent.Graphics.FillRectangle(brush, ClientRectangle);
            }
            else
            {
                base.OnPaintBackground(pevent);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (InDesigner)
            {
                // Draw placeholder frame and label
                var r = ClientRectangle;
                r.Width -= 1; r.Height -= 1;
                using var pen = new Pen(Color.SteelBlue) { DashStyle = DashStyle.Dash };
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRectangle(pen, r);
                TextRenderer.DrawText(e.Graphics, "SpellCheckedTextBox", Font, new System.Drawing.Point(6, 6), Color.SteelBlue);
            }
        }

        // Essential: Provide visual feedback for selection
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (InDesigner)
            {
                Select();
                Invalidate();
            }
        }

#pragma warning disable CS8765
        public override string Text
        {
            get => _tb.Text ?? string.Empty;
            set => _tb.Text = value ?? string.Empty;
        }
#pragma warning restore CS8765

        // SelectionStart for Shift+Enter logic in MainForm
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => _tb.CaretIndex;
            set
            {
                var len = _tb.Text?.Length ?? 0;
                _tb.CaretIndex = Math.Max(0, Math.Min(value, len));
            }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get => _tb.AcceptsReturn;
            set
            {
                _tb.AcceptsReturn = value;
                var wantsHorizontal = (_scrollBars & ScrollBars.Horizontal) != 0;
                _tb.TextWrapping = value && !wantsHorizontal ? TextWrapping.Wrap : TextWrapping.NoWrap;
                _tb.VerticalScrollBarVisibility = value && (_scrollBars & ScrollBars.Vertical) != 0
                    ? Wpf.ScrollBarVisibility.Auto
                    : (value ? Wpf.ScrollBarVisibility.Auto : Wpf.ScrollBarVisibility.Disabled);
                Height = value ? Math.Max(60, Height) : 23;
            }
        }

        [DefaultValue(false)]
        public bool AcceptsTab
        {
            get => _tb.AcceptsTab;
            set => _tb.AcceptsTab = value;
        }

        // ReadOnly (backed so runtime value is restored after design-time)
        private bool _readOnly;

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                _tb.IsReadOnly = value;
            }
        }

        // ScrollBars mapping (e.g., ed_input.ScrollBars = ScrollBars.Vertical)
        private ScrollBars _scrollBars = ScrollBars.None;

        [DefaultValue(ScrollBars.None)]
        public ScrollBars ScrollBars
        {
            get => _scrollBars;
            set
            {
                _scrollBars = value;

                // Vertical
                _tb.VerticalScrollBarVisibility =
                    (value & ScrollBars.Vertical) != 0
                        ? Wpf.ScrollBarVisibility.Auto
                        : Wpf.ScrollBarVisibility.Disabled;

                // Horizontal (if requested, disable wrapping)
                var enableHorizontal = (value & ScrollBars.Horizontal) != 0;
                _tb.HorizontalScrollBarVisibility = enableHorizontal
                    ? Wpf.ScrollBarVisibility.Auto
                    : Wpf.ScrollBarVisibility.Disabled;

                if (enableHorizontal)
                    _tb.TextWrapping = TextWrapping.NoWrap;
                else if (Multiline)
                    _tb.TextWrapping = TextWrapping.Wrap;
            }
        }

        // Emulate WinForms BorderStyle so the designer line compiles
        private BorderStyle _borderStyle = BorderStyle.FixedSingle;

        [DefaultValue(BorderStyle.FixedSingle)]
        public BorderStyle BorderStyle
        {
            get => _borderStyle;
            set
            {
                _borderStyle = value;
                switch (value)
                {
                    case BorderStyle.None:
                        _border.BorderThickness = new Thickness(0);
                        break;
                    case BorderStyle.Fixed3D:
                        _border.BorderThickness = new Thickness(2);
                        _border.BorderBrush = new Media.SolidColorBrush(Media.Colors.DimGray);
                        break;
                    default:
                        _border.BorderThickness = new Thickness(1);
                        _border.BorderBrush = new Media.SolidColorBrush(Media.Colors.Gray);
                        break;
                }
            }
        }

        // Spell check language (BCP-47 tag, e.g., "en-US", "en-GB", "fr-FR")
        private string? _spellLang;

        [Category("Behavior")]
        [Description("BCP-47 language for spell checking (e.g., en-US, en-GB, fr-FR). Leave empty to use thread language.")]
        [DefaultValue(null)]
        public string? SpellCheckLanguage
        {
            get => _spellLang;
            set
            {
                _spellLang = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                if (_spellLang is null)
                {
                    _tb.ClearValue(FrameworkElement.LanguageProperty);
                }
                else
                {
                    try { _tb.Language = XmlLanguage.GetLanguage(_spellLang); } catch { /* ignore invalid */ }
                }

                // Refresh underlines after language change
                var wasOn = _tb.SpellCheck.IsEnabled;
                if (wasOn) { _tb.SpellCheck.IsEnabled = false; _tb.SpellCheck.IsEnabled = true; }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CultureInfo? SpellCheckCulture
        {
            get => string.IsNullOrWhiteSpace(_spellLang) ? null : new CultureInfo(_spellLang);
            set => SpellCheckLanguage = value?.Name;
        }

        // Map WinForms Font to WPF Font
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (Font is { } f)
            {
                _tb.FontFamily = new Media.FontFamily(f.Name);
                _tb.FontSize = f.SizeInPoints;
                _tb.FontWeight = f.Bold ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
                _tb.FontStyle = f.Italic ? System.Windows.FontStyles.Italic : System.Windows.FontStyles.Normal;
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            var c = BackColor;
            _tb.Background = new Media.SolidColorBrush(Media.Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            var c = ForeColor;
            _tb.Foreground = new Media.SolidColorBrush(Media.Color.FromArgb(c.A, c.R, c.G, c.B));
            _caretBrush ??= new Media.SolidColorBrush(Media.Colors.White);
            _tb.CaretBrush = _caretBrush;
        }

        private Media.Brush? _caretBrush;
    }
}