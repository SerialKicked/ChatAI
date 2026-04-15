using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LetheChat.Controls
{
    [ToolboxItem(true)]
    [DefaultProperty(nameof(Items))]
    [DefaultEvent(nameof(SelectedIndexChanged))]
    public class ModernComboBox : UserControl, IThemeAware
    {
        public bool ThemeProcessInnerComponent => false;

        private readonly TextBox _textBox;
        private readonly Panel _buttonPanel;
        private readonly ListBox _dropDownList;
        private readonly Form _dropDownForm;

        private ComboBoxStyle _dropDownStyle = ComboBoxStyle.DropDownList;
        private int _selectedIndex = -1;
        private int _dropDownHeight = 180;
        private int _maxDropDownItems = 10;
        private bool _droppedDown = false;

        private bool _buttonHovered;
        private bool _buttonPressed;

        // Theme colors
        private Color _panel;
        private Color _border;
        private Color _accent;
        private Color _accentHover;
        private Color _text;
        private Color _textMuted;
        private Color _glyphBack;
        private Color _glyphBackHover;

        public ModernComboBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            Items = new ObjectCollection(this);

            // Create text box
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                //TextAlign = HorizontalAlignment.Right,
                ReadOnly = true
            };
            _textBox.MouseDown += TextBox_MouseDown;
            _textBox.KeyDown += TextBox_KeyDown;

            // Create dropdown button panel
            _buttonPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 20
            };
            _buttonPanel.Paint += ButtonPanel_Paint;
            _buttonPanel.MouseMove += ButtonPanel_MouseMove;
            _buttonPanel.MouseLeave += ButtonPanel_MouseLeave;
            _buttonPanel.MouseDown += ButtonPanel_MouseDown;
            _buttonPanel.MouseUp += ButtonPanel_MouseUp;

            // Container for textbox with padding
            var innerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(4, 2, 2, 0),
                BorderStyle = BorderStyle.None
            };
            innerPanel.Controls.Add(_textBox);

            Controls.Add(innerPanel);
            Controls.Add(_buttonPanel);

            // Create dropdown listbox
            _dropDownList = new ListBox
            {
                BorderStyle = BorderStyle.None,
                IntegralHeight = false,
                DrawMode = DrawMode.OwnerDrawFixed,
                Dock = DockStyle.Fill
            };
            _dropDownList.DrawItem += DropDownList_DrawItem;
            _dropDownList.Click += DropDownList_Click;
            _dropDownList.KeyDown += DropDownList_KeyDown;

            // Create dropdown form
            _dropDownForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                TopMost = true,
                Padding = new Padding(1) // Border
            };
            _dropDownForm.Deactivate += DropDownForm_Deactivate;
            _dropDownForm.Controls.Add(_dropDownList);

            Size = new Size(150, 24);
            Padding = new Padding(1); // Border width

            ThemeManager.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.CurrentTheme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
                _dropDownForm?.Dispose();
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
            BackColor = _border;
            _textBox.BackColor = _panel;
            _textBox.ForeColor = _text;
            _textBox.Font = t.Base;
            _buttonPanel.BackColor = _glyphBack;

            // Add this line - find the innerPanel and set its background
            foreach (Control c in Controls)
            {
                if (c is Panel p && c != _buttonPanel)
                {
                    p.BackColor = _panel;
                    break;
                }
            }

            _dropDownList.BackColor = _panel;
            _dropDownList.ForeColor = _text;
            _dropDownList.Font = t.Base;
            _dropDownList.ItemHeight = 28;

            _dropDownForm.BackColor = _border;

            Invalidate();
        }

        // =========================================================
        // Properties
        // =========================================================

        [Category("Behavior")]
        [DefaultValue(ComboBoxStyle.DropDownList)]
        public ComboBoxStyle DropDownStyle
        {
            get => _dropDownStyle;
            set
            {
                if (_dropDownStyle != value)
                {
                    _dropDownStyle = value;
                    _textBox.ReadOnly = _dropDownStyle == ComboBoxStyle.DropDownList;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < -1 || value >= Items.Count)
                    value = -1;

                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    UpdateTextBox();
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? SelectedItem
        {
            get => _selectedIndex >= 0 && _selectedIndex < Items.Count
                ? Items[_selectedIndex]
                : null;
            set
            {
                if (value == null)
                {
                    SelectedIndex = -1;
                    return;
                }

                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i]?.Equals(value) == true)
                    {
                        SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? SelectedText
        {
            get => _textBox.Text;
            set => _textBox.Text = value ?? string.Empty;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public ObjectCollection Items { get; }

        [Category("Behavior")]
        [DefaultValue(150)]
        public int DropDownHeight
        {
            get => _dropDownHeight;
            set => _dropDownHeight = Math.Max(50, value);
        }

        [Category("Behavior")]
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get => _maxDropDownItems;
            set => _maxDropDownItems = Math.Max(1, Math.Min(30, value));
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool DroppedDown
        {
            get => _droppedDown;
            set
            {
                if (_droppedDown != value)
                {
                    if (value)
                        ShowDropDown();
                    else
                        HideDropDown();
                }
            }
        }

        [Category("Behavior")]
        public event EventHandler? SelectedIndexChanged;

        [Category("Behavior")]
        public event EventHandler? DropDown;

        [Category("Behavior")]
        public event EventHandler? DropDownClosed;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        protected virtual void OnDropDown(EventArgs e)
        {
            DropDown?.Invoke(this, e);
        }

        protected virtual void OnDropDownClosed(EventArgs e)
        {
            DropDownClosed?.Invoke(this, e);
        }

        // =========================================================
        // Methods
        // =========================================================

        private void UpdateTextBox()
        {
            if (_selectedIndex >= 0 && _selectedIndex < Items.Count)
            {
                _textBox.Text = Items[_selectedIndex]?.ToString() ?? string.Empty;
            }
            else
            {
                _textBox.Text = string.Empty;
            }
        }

        public void ShowDropDown()
        {
            if (_droppedDown || Items.Count == 0)
                return;

            _droppedDown = true;
            _buttonPressed = true;
            _buttonPanel.Invalidate();

            // Synchronize listbox with current items
            _dropDownList.Items.Clear();
            foreach (var item in Items)
                _dropDownList.Items.Add(item);

            _dropDownList.SelectedIndex = _selectedIndex;

            // Calculate dropdown size
            int itemCount = Math.Min(Items.Count, _maxDropDownItems);
            int height = Math.Min(_dropDownHeight, itemCount * _dropDownList.ItemHeight + 2);

            // Position dropdown below control
            var screenPos = PointToScreen(new Point(0, Height));
            _dropDownForm.Location = screenPos;
            _dropDownForm.Width = Width;
            _dropDownForm.Height = height;

            OnDropDown(EventArgs.Empty);

            // Show without taking focus from parent form
            NativeMethods.ShowWindow(_dropDownForm.Handle, NativeMethods.SW_SHOWNOACTIVATE);
            _dropDownForm.Visible = true;

            // Focus list to enable keyboard nav
            _dropDownList.Focus();
        }

        public void HideDropDown()
        {
            if (!_droppedDown)
                return;

            _droppedDown = false;
            _buttonPressed = false;
            _buttonPanel.Invalidate();

            _dropDownForm.Hide();
            OnDropDownClosed(EventArgs.Empty);
        }

        // =========================================================
        // Events
        // =========================================================

        private void TextBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_dropDownStyle != ComboBoxStyle.DropDownList || e.Button != MouseButtons.Left)
                return;

            if (_droppedDown)
                HideDropDown();
            else
                ShowDropDown();
        }

        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && !e.Alt)
            {
                if (_selectedIndex < Items.Count - 1)
                {
                    SelectedIndex++;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Up && !e.Alt)
            {
                if (_selectedIndex > 0)
                {
                    SelectedIndex--;
                    e.Handled = true;
                }
            }
            else if ((e.KeyCode == Keys.Down && e.Alt) || e.KeyCode == Keys.F4)
            {
                if (!_droppedDown)
                    ShowDropDown();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape && _droppedDown)
            {
                HideDropDown();
                e.Handled = true;
            }
        }

        private void ButtonPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(_glyphBack);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Background
            Color backColor = _buttonPressed ? _accentHover : (_buttonHovered ? _glyphBackHover : _glyphBack);
            using (var b = new SolidBrush(backColor))
                g.FillRectangle(b, _buttonPanel.ClientRectangle);

            // Arrow
            Color arrowColor = _buttonPressed || _buttonHovered ? _accent : _textMuted;
            using var pen = new Pen(arrowColor, 1.5f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            int cx = _buttonPanel.Width / 2;
            int cy = _buttonPanel.Height / 2;
            int arrowSize = 4;

            // Down arrow
            Point[] points =
            [
                new Point(cx - arrowSize, cy - 2),
                new Point(cx, cy + arrowSize - 2),
                new Point(cx + arrowSize, cy - 2)
            ];
            g.DrawLines(pen, points);
        }

        private void ButtonPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!_buttonHovered)
            {
                _buttonHovered = true;
                _buttonPanel.Invalidate();
            }
        }

        private void ButtonPanel_MouseLeave(object? sender, EventArgs e)
        {
            if (_buttonHovered && !_buttonPressed)
            {
                _buttonHovered = false;
                _buttonPanel.Invalidate();
            }
        }

        private void ButtonPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _buttonPressed = true;
            _buttonPanel.Invalidate();

            if (_droppedDown)
                HideDropDown();
            else
                ShowDropDown();
        }

        private void ButtonPanel_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_droppedDown && _buttonPressed)
            {
                _buttonPressed = false;
                _buttonPanel.Invalidate();
            }
        }

        private void DropDownForm_Deactivate(object? sender, EventArgs e)
        {
            if (!_droppedDown)
                return;

            // Check if the click that caused deactivation was on our control
            var cursorPos = Control.MousePosition;
            var ourBounds = RectangleToScreen(ClientRectangle);

            // If mouse is over our control, the click handler will manage state - ignore this deactivate
            if (ourBounds.Contains(cursorPos))
                return;

            // Otherwise, user clicked elsewhere - close the dropdown
            HideDropDown();
        }

        private void DropDownList_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            bool selected = (e.State & DrawItemState.Selected) != 0;
            Color backColor = selected ? _accent : _panel;
            Color foreColor = selected ? Color.White : _text;

            using (var b = new SolidBrush(backColor))
                e.Graphics.FillRectangle(b, e.Bounds);

            string text = _dropDownList.Items[e.Index]?.ToString() ?? string.Empty;
            TextRenderer.DrawText(
                e.Graphics,
                text,
                _dropDownList.Font,
                new Rectangle(e.Bounds.X + 8, e.Bounds.Y, e.Bounds.Width - 16, e.Bounds.Height),
                foreColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        private void DropDownList_Click(object? sender, EventArgs e)
        {
            if (_dropDownList.SelectedIndex >= 0)
            {
                SelectedIndex = _dropDownList.SelectedIndex;
                HideDropDown();
            }
        }

        private void DropDownList_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (_dropDownList.SelectedIndex >= 0)
                {
                    SelectedIndex = _dropDownList.SelectedIndex;
                    HideDropDown();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideDropDown();
                e.Handled = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            _textBox.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F4 || (keyData == (Keys.Alt | Keys.Down)))
            {
                if (!_droppedDown)
                    ShowDropDown();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =========================================================
        // Native Methods for ShowNoActivate
        // =========================================================

        private static class NativeMethods
        {
            public const int SW_SHOWNOACTIVATE = 4;

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        }

        // =========================================================
        // Item Collection
        // =========================================================

        public class ObjectCollection : System.Collections.IList
        {
            private readonly ModernComboBox _owner;
            private readonly System.Collections.ArrayList _items = [];

            internal ObjectCollection(ModernComboBox owner)
            {
                _owner = owner;
            }

            public int Count => _items.Count;
            public bool IsReadOnly => false;
            public bool IsFixedSize => false;
            public object SyncRoot => _items.SyncRoot;
            public bool IsSynchronized => false;

            public object? this[int index]
            {
                get => _items[index];
                set
                {
                    _items[index] = value;
                    _owner.UpdateTextBox();
                    _owner.Invalidate();
                }
            }

            public int Add(object? item)
            {
                int result = _items.Add(item);
                _owner.Invalidate();
                return result;
            }

            public void Clear()
            {
                _items.Clear();
                _owner._selectedIndex = -1;
                _owner.UpdateTextBox();
                _owner.Invalidate();
            }

            public bool Contains(object? item) => _items.Contains(item);
            public int IndexOf(object? item) => _items.IndexOf(item);

            public void Insert(int index, object? item)
            {
                _items.Insert(index, item);
                if (_owner._selectedIndex >= index)
                    _owner._selectedIndex++;
                _owner.Invalidate();
            }

            public void Remove(object? item)
            {
                int index = _items.IndexOf(item);
                if (index >= 0)
                    RemoveAt(index);
            }

            public void RemoveAt(int index)
            {
                _items.RemoveAt(index);
                if (_owner._selectedIndex == index)
                    _owner._selectedIndex = -1;
                else if (_owner._selectedIndex > index)
                    _owner._selectedIndex--;
                _owner.UpdateTextBox();
                _owner.Invalidate();
            }

            public void CopyTo(Array array, int index) => _items.CopyTo(array, index);
            public System.Collections.IEnumerator GetEnumerator() => _items.GetEnumerator();

            public void AddRange(object[] items)
            {
                _items.AddRange(items);
                _owner.Invalidate();
            }
        }
    }
}