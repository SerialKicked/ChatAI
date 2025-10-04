using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaifuAI.Controls
{
    [ToolboxItem(true)]
    [DefaultProperty(nameof(Items))]
    [DefaultEvent(nameof(ItemCheck))]
    public class ModernCheckedListBox : UserControl, IThemeAware
    {
        public bool ThemeProcessInnerComponent => true;

        private readonly Panel _itemPanel;
        private readonly Panel _scrollPanel;

        private int _itemHeight = 32;
        private int _scrollOffset = 0;
        private int _hoverIndex = -1;
        private int _selectedIndex = -1;
        private bool _integralHeight = true;
        private bool _alwaysscrollbar = false;

        // Scrollbar state
        private const int ScrollBarWidth = 10;
        private int _scrollBarMaxValue;
        private int _scrollBarLargeChange;
        private bool _scrollThumbHovered;
        private bool _scrollThumbDragging;
        private int _scrollDragStartY;
        private int _scrollDragStartValue;
        private Rectangle _scrollThumbRect;
        private Rectangle _scrollTrackRect;

        // Checkbox state
        private int _hoverCheckIndex = -1;
        private const int CheckBoxSize = 18;
        private const int CheckBoxMargin = 8;

        // Theme colors
        private Color _back;
        private Color _panel;
        private Color _border;
        private Color _accent;
        private Color _accentHover;
        private Color _text;
        private Color _textMuted;
        private Color _scrollTrack;
        private Color _scrollThumb;
        private Color _scrollThumbHover;
        private Color _glyphBack;
        private Color _glyphBackHover;

        public ModernCheckedListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            Items = new CheckedListBoxItemCollection(this);

            // Create item display panel
            _itemPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            _itemPanel.Paint += ItemPanel_Paint;
            _itemPanel.MouseMove += ItemPanel_MouseMove;
            _itemPanel.MouseLeave += ItemPanel_MouseLeave;
            _itemPanel.MouseDown += ItemPanel_MouseDown;
            _itemPanel.MouseWheel += ItemPanel_MouseWheel;

            // Create custom scrollbar panel
            _scrollPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = ScrollBarWidth,
                Visible = false,
                BackColor = Color.Transparent
            };
            _scrollPanel.Paint += ScrollPanel_Paint;
            _scrollPanel.MouseMove += ScrollPanel_MouseMove;
            _scrollPanel.MouseLeave += ScrollPanel_MouseLeave;
            _scrollPanel.MouseDown += ScrollPanel_MouseDown;
            _scrollPanel.MouseUp += ScrollPanel_MouseUp;

            Controls.Add(_itemPanel);
            Controls.Add(_scrollPanel);

            Size = new Size(200, 150);
            Padding = new Padding(1); // Border width

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

        public void ApplyTheme(ThemeManager.Theme t)
        {
            _back = t.Back;
            _panel = t.Panel;
            _border = t.Border;
            _accent = t.Accent;
            _accentHover = t.AccentHover;
            _text = t.TextPrimary;
            _textMuted = t.TextMuted;
            _scrollTrack = t.GlyphBack;
            _scrollThumb = t.Border;
            _scrollThumbHover = t.Accent;
            _glyphBack = t.GlyphBack;
            _glyphBackHover = t.GlyphBackHover;

            Font = t.Base;
            BackColor = _border; // Border via background
            _itemPanel.BackColor = _panel;
            _scrollPanel.BackColor = _scrollTrack;

            Invalidate();
        }

        [Category("Appearance")]
        [DefaultValue(32)]
        public int ItemHeight
        {
            get => _itemHeight;
            set
            {
                if (value < 16) value = 16;
                if (_itemHeight != value)
                {
                    _itemHeight = value;
                    UpdateScrollBar();
                    Invalidate();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool IntegralHeight
        {
            get => _integralHeight;
            set
            {
                if (_integralHeight != value)
                {
                    _integralHeight = value;
                    if (_integralHeight)
                        AdjustHeightToIntegral();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool AlwaysScrollbar
        {
            get => _alwaysscrollbar;
            set
            {
                if (_alwaysscrollbar != value)
                {
                    _alwaysscrollbar = value;
                    UpdateScrollBar();
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
                    EnsureVisible(_selectedIndex);
                    Invalidate();
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? SelectedItem
        {
            get => _selectedIndex >= 0 && _selectedIndex < Items.Count
                ? Items[_selectedIndex].Item
                : null;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public CheckedListBoxItemCollection Items { get; }

        [Category("Behavior")]
        public event EventHandler? SelectedIndexChanged;

        [Category("Behavior")]
        public event ItemCheckEventHandler? ItemCheck;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        protected virtual void OnItemCheck(ItemCheckEventArgs e)
        {
            ItemCheck?.Invoke(this, e);
        }

        public bool GetItemChecked(int index)
        {
            if (index < 0 || index >= Items.Count)
                return false;
            return Items[index].Checked;
        }

        public void SetItemChecked(int index, bool value)
        {
            if (index < 0 || index >= Items.Count)
                return;

            if (Items[index].Checked != value)
            {
                var oldState = Items[index].Checked ? CheckState.Checked : CheckState.Unchecked;
                var newState = value ? CheckState.Checked : CheckState.Unchecked;

                var args = new ItemCheckEventArgs(index, newState, oldState);
                OnItemCheck(args);

                if (args.NewValue != oldState)
                {
                    Items[index].Checked = args.NewValue == CheckState.Checked;
                    _itemPanel.Invalidate();
                }
            }
        }

        public CheckedIndexCollection CheckedIndices => new CheckedIndexCollection(this);
        public CheckedItemCollection CheckedItems => new CheckedItemCollection(this);

        private void AdjustHeightToIntegral()
        {
            if (!_integralHeight || _itemHeight <= 0) return;

            int availableHeight = Height - Padding.Vertical;
            int wholeItems = Math.Max(1, availableHeight / _itemHeight);
            int newHeight = wholeItems * _itemHeight + Padding.Vertical;

            if (newHeight != Height)
                Height = newHeight;
        }

        private void UpdateScrollBar()
        {
            if (_itemHeight <= 0 || Items.Count == 0)
            {
                _scrollPanel.Visible = AlwaysScrollbar;
                _scrollOffset = 0;
                _scrollBarMaxValue = 0;
                return;
            }

            int totalHeight = Items.Count * _itemHeight;
            int viewHeight = _itemPanel.Height;

            if (totalHeight <= viewHeight)
            {
                _scrollPanel.Visible = AlwaysScrollbar;
                _scrollBarMaxValue = 0;
                _scrollOffset = 0;
            }
            else
            {
                _scrollPanel.Visible = true;
                _scrollBarMaxValue = Math.Max(0, totalHeight - viewHeight);
                _scrollBarLargeChange = viewHeight;

                if (_scrollOffset > _scrollBarMaxValue)
                    _scrollOffset = _scrollBarMaxValue;

                CalculateScrollThumbRect();
            }

            _scrollPanel.Invalidate();
        }

        private void CalculateScrollThumbRect()
        {
            if (_scrollBarMaxValue <= 0)
            {
                _scrollThumbRect = Rectangle.Empty;
                return;
            }

            _scrollTrackRect = new Rectangle(2, 2, _scrollPanel.Width - 4, _scrollPanel.Height - 4);

            int totalContentHeight = Items.Count * _itemHeight;
            int viewHeight = _itemPanel.Height;

            float ratio = (float)viewHeight / totalContentHeight;
            int thumbHeight = Math.Max(30, (int)(_scrollTrackRect.Height * ratio));

            float scrollRatio = _scrollBarMaxValue > 0 ? (float)_scrollOffset / _scrollBarMaxValue : 0f;
            int availableTravel = _scrollTrackRect.Height - thumbHeight;
            int thumbY = _scrollTrackRect.Y + (int)(availableTravel * scrollRatio);

            _scrollThumbRect = new Rectangle(_scrollTrackRect.X, thumbY, _scrollTrackRect.Width, thumbHeight);
        }

        private void ItemPanel_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (!_scrollPanel.Visible || _scrollBarMaxValue <= 0) return;

            int delta = -e.Delta / 120 * _itemHeight;
            int newOffset = _scrollOffset + delta;

            _scrollOffset = Math.Max(0, Math.Min(_scrollBarMaxValue, newOffset));
            CalculateScrollThumbRect();
            _itemPanel.Invalidate();
            _scrollPanel.Invalidate();
        }

        private void ItemPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            int index = GetItemIndexAtPoint(e.Location);
            var checkRect = GetCheckBoxRect(index);

            int newHoverIndex = index;
            int newHoverCheckIndex = checkRect.Contains(e.Location) ? index : -1;

            if (newHoverIndex != _hoverIndex || newHoverCheckIndex != _hoverCheckIndex)
            {
                _hoverIndex = newHoverIndex;
                _hoverCheckIndex = newHoverCheckIndex;
                _itemPanel.Invalidate();
            }
        }

        private void ItemPanel_MouseLeave(object? sender, EventArgs e)
        {
            if (_hoverIndex != -1 || _hoverCheckIndex != -1)
            {
                _hoverIndex = -1;
                _hoverCheckIndex = -1;
                _itemPanel.Invalidate();
            }
        }

        private void ItemPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int index = GetItemIndexAtPoint(e.Location);
            if (index >= 0 && index < Items.Count)
            {
                var checkRect = GetCheckBoxRect(index);

                if (checkRect.Contains(e.Location))
                {
                    // Toggle checkbox
                    SetItemChecked(index, !Items[index].Checked);
                }
                else
                {
                    // Select item
                    SelectedIndex = index;
                    _hoverIndex = -1; // Clear hover on click
                    _itemPanel.Invalidate();
                }

                _itemPanel.Focus();
            }
        }

        // =========================================================
        // Custom Scrollbar Interaction
        // =========================================================

        private void ScrollPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(_scrollTrack);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_scrollBarMaxValue <= 0 || _scrollThumbRect.IsEmpty)
                return;

            Color thumbColor = _scrollThumbDragging
                ? _accentHover
                : (_scrollThumbHovered ? _scrollThumbHover : _scrollThumb);

            using var thumbBrush = new SolidBrush(thumbColor);
            var thumbPath = GetRoundedRect(_scrollThumbRect, 3);
            g.FillPath(thumbBrush, thumbPath);
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void ScrollPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_scrollThumbDragging)
            {
                int deltaY = e.Y - _scrollDragStartY;
                int availableTravel = _scrollTrackRect.Height - _scrollThumbRect.Height;

                if (availableTravel > 0)
                {
                    float ratio = (float)deltaY / availableTravel;
                    int newOffset = _scrollDragStartValue + (int)(_scrollBarMaxValue * ratio);
                    _scrollOffset = Math.Max(0, Math.Min(_scrollBarMaxValue, newOffset));

                    CalculateScrollThumbRect();
                    _itemPanel.Invalidate();
                    _scrollPanel.Invalidate();
                }
            }
            else
            {
                bool wasHovered = _scrollThumbHovered;
                _scrollThumbHovered = _scrollThumbRect.Contains(e.Location);

                if (wasHovered != _scrollThumbHovered)
                    _scrollPanel.Invalidate();
            }
        }

        private void ScrollPanel_MouseLeave(object? sender, EventArgs e)
        {
            if (!_scrollThumbDragging && _scrollThumbHovered)
            {
                _scrollThumbHovered = false;
                _scrollPanel.Invalidate();
            }
        }

        private void ScrollPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_scrollThumbRect.Contains(e.Location))
            {
                _scrollThumbDragging = true;
                _scrollDragStartY = e.Y;
                _scrollDragStartValue = _scrollOffset;
                _scrollPanel.Capture = true;
                _scrollPanel.Invalidate();
            }
            else if (_scrollTrackRect.Contains(e.Location))
            {
                int thumbCenter = _scrollThumbRect.Y + _scrollThumbRect.Height / 2;
                int delta = e.Y > thumbCenter ? _scrollBarLargeChange : -_scrollBarLargeChange;

                _scrollOffset = Math.Max(0, Math.Min(_scrollBarMaxValue, _scrollOffset + delta));
                CalculateScrollThumbRect();
                _itemPanel.Invalidate();
                _scrollPanel.Invalidate();
            }
        }

        private void ScrollPanel_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_scrollThumbDragging)
            {
                _scrollThumbDragging = false;
                _scrollPanel.Capture = false;

                _scrollThumbHovered = _scrollThumbRect.Contains(e.Location);
                _scrollPanel.Invalidate();
            }
        }

        // =========================================================
        // Item Rendering & Helpers
        // =========================================================

        private int GetItemIndexAtPoint(Point pt)
        {
            if (_itemHeight <= 0) return -1;

            int adjustedY = pt.Y + _scrollOffset;
            int index = adjustedY / _itemHeight;

            return (index >= 0 && index < Items.Count) ? index : -1;
        }

        private Rectangle GetCheckBoxRect(int index)
        {
            if (index < 0 || index >= Items.Count)
                return Rectangle.Empty;

            int y = index * _itemHeight - _scrollOffset;
            int checkBoxY = y + (_itemHeight - CheckBoxSize) / 2;

            return new Rectangle(CheckBoxMargin, checkBoxY, CheckBoxSize, CheckBoxSize);
        }

        private void EnsureVisible(int index)
        {
            if (index < 0 || index >= Items.Count || !_scrollPanel.Visible)
                return;

            int itemTop = index * _itemHeight;
            int itemBottom = itemTop + _itemHeight;
            int viewTop = _scrollOffset;
            int viewBottom = _scrollOffset + _itemPanel.Height;

            if (itemTop < viewTop)
            {
                _scrollOffset = itemTop;
                CalculateScrollThumbRect();
                _scrollPanel.Invalidate();
            }
            else if (itemBottom > viewBottom)
            {
                _scrollOffset = Math.Min(_scrollBarMaxValue, itemBottom - _itemPanel.Height);
                CalculateScrollThumbRect();
                _scrollPanel.Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateScrollBar();
            if (_integralHeight)
                AdjustHeightToIntegral();
        }

        private void ItemPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(_panel);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (Items.Count == 0 || _itemHeight <= 0)
                return;

            int firstVisible = Math.Max(0, _scrollOffset / _itemHeight);
            int lastVisible = Math.Min(Items.Count - 1,
                (_scrollOffset + _itemPanel.Height) / _itemHeight + 1);

            for (int i = firstVisible; i <= lastVisible; i++)
            {
                if (i >= Items.Count) break;

                int y = i * _itemHeight - _scrollOffset;
                var itemRect = new Rectangle(0, y, _itemPanel.Width, _itemHeight);

                DrawItem(g, i, itemRect);
            }
        }

        private void DrawItem(Graphics g, int index, Rectangle bounds)
        {
            bool isSelected = index == _selectedIndex;
            bool isHovered = index == _hoverIndex && !isSelected;

            // Background
            if (isSelected)
            {
                using var b = new SolidBrush(_accent);
                g.FillRectangle(b, bounds);
            }
            else if (isHovered)
            {
                using var b = new SolidBrush(Color.FromArgb(30, _accent));
                g.FillRectangle(b, bounds);
            }

            // Checkbox
            var checkRect = GetCheckBoxRect(index);
            bool checkHovered = index == _hoverCheckIndex;
            DrawCheckBox(g, checkRect, Items[index].Checked, checkHovered, isSelected);

            // Text
            string text = Items[index].Item?.ToString() ?? string.Empty;
            Color textColor = isSelected ? Color.White : _text;

            int textLeft = CheckBoxMargin + CheckBoxSize + CheckBoxMargin;
            var textRect = new Rectangle(
                bounds.X + textLeft,
                bounds.Y,
                bounds.Width - textLeft - 12,
                bounds.Height
            );

            TextRenderer.DrawText(
                g,
                text,
                Font,
                textRect,
                textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        private void DrawCheckBox(Graphics g, Rectangle r, bool isChecked, bool isHovered, bool itemSelected)
        {
            // Background
            Color backColor = isHovered ? _glyphBackHover : _glyphBack;
            if (itemSelected)
                backColor = Color.FromArgb(200, backColor); // Slightly transparent on selected row

            using (var b = new SolidBrush(backColor))
                g.FillRectangle(b, r);

            // Border
            using (var p = new Pen(_border))
                g.DrawRectangle(p, r);

            // Checkmark
            if (isChecked)
            {
                Color checkColor = isHovered ? _accentHover : _accent;

                using var pen = new Pen(checkColor, 2.2f)
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

                g.DrawLine(pen, x1, y1, x2, y2);
                g.DrawLine(pen, x2, y2, x3, y3);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                case Keys.Space:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (Items.Count == 0)
                return;

            if (e.KeyCode == Keys.Space && _selectedIndex >= 0 && _selectedIndex < Items.Count)
            {
                SetItemChecked(_selectedIndex, !Items[_selectedIndex].Checked);
                e.Handled = true;
                return;
            }

            int newIndex = _selectedIndex;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    newIndex = Math.Max(0, _selectedIndex - 1);
                    break;
                case Keys.Down:
                    newIndex = Math.Min(Items.Count - 1, _selectedIndex + 1);
                    break;
                case Keys.Home:
                    newIndex = 0;
                    break;
                case Keys.End:
                    newIndex = Items.Count - 1;
                    break;
                case Keys.PageUp:
                    int pageSize = Math.Max(1, _itemPanel.Height / _itemHeight);
                    newIndex = Math.Max(0, _selectedIndex - pageSize);
                    break;
                case Keys.PageDown:
                    pageSize = Math.Max(1, _itemPanel.Height / _itemHeight);
                    newIndex = Math.Min(Items.Count - 1, _selectedIndex + pageSize);
                    break;
            }

            if (newIndex != _selectedIndex)
            {
                SelectedIndex = newIndex;
                e.Handled = true;
            }
        }

        // =========================================================
        // Custom Collections
        // =========================================================

        public class CheckedListBoxItem
        {
            public object? Item { get; set; }
            public bool Checked { get; set; }

            // Parameterless constructor for designer serialization
            public CheckedListBoxItem()
            {
                Item = null;
                Checked = false;
            }

            public CheckedListBoxItem(object? item, bool isChecked = false)
            {
                Item = item;
                Checked = isChecked;
            }

            public override string ToString() => Item?.ToString() ?? string.Empty;
        }

        public class CheckedListBoxItemCollection : System.Collections.IList
        {
            private readonly ModernCheckedListBox _owner;
            private readonly System.Collections.ArrayList _items = new();

            internal CheckedListBoxItemCollection(ModernCheckedListBox owner)
            {
                _owner = owner;
            }

            public int Count => _items.Count;
            public bool IsReadOnly => false;
            public bool IsFixedSize => false;
            public object SyncRoot => _items.SyncRoot;
            public bool IsSynchronized => false;

            public CheckedListBoxItem this[int index]
            {
                get => (CheckedListBoxItem)_items[index]!;
                set
                {
                    _items[index] = value;
                    _owner.UpdateScrollBar();
                    _owner.Invalidate();
                }
            }

            object? System.Collections.IList.this[int index]
            {
                get => this[index];
                set => this[index] = (CheckedListBoxItem)value!;
            }

            public int Add(object? item, bool isChecked = false)
            {
                var wrappedItem = new CheckedListBoxItem(item, isChecked);
                int result = _items.Add(wrappedItem);
                _owner.UpdateScrollBar();
                _owner.Invalidate();
                return result;
            }

            int System.Collections.IList.Add(object? value)
            {
                if (value is CheckedListBoxItem item)
                    return Add(item.Item, item.Checked);
                return Add(value, false);
            }

            public void Clear()
            {
                _items.Clear();
                _owner._selectedIndex = -1;
                _owner._hoverIndex = -1;
                _owner._scrollOffset = 0;
                _owner.UpdateScrollBar();
                _owner.Invalidate();
            }

            public bool Contains(object? item)
            {
                foreach (CheckedListBoxItem citem in _items)
                {
                    if (citem.Item?.Equals(item) == true)
                        return true;
                }
                return false;
            }

            public int IndexOf(object? item)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (((CheckedListBoxItem)_items[i]!).Item?.Equals(item) == true)
                        return i;
                }
                return -1;
            }

            public void Insert(int index, object? item, bool isChecked = false)
            {
                var wrappedItem = new CheckedListBoxItem(item, isChecked);
                _items.Insert(index, wrappedItem);
                if (_owner._selectedIndex >= index)
                    _owner._selectedIndex++;
                _owner.UpdateScrollBar();
                _owner.Invalidate();
            }

            void System.Collections.IList.Insert(int index, object? value)
            {
                if (value is CheckedListBoxItem item)
                    Insert(index, item.Item, item.Checked);
                else
                    Insert(index, value, false);
            }

            public void Remove(object? item)
            {
                int index = IndexOf(item);
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
                _owner.UpdateScrollBar();
                _owner.Invalidate();
            }

            public void CopyTo(Array array, int index) => _items.CopyTo(array, index);
            public System.Collections.IEnumerator GetEnumerator() => _items.GetEnumerator();
        }

        public class CheckedIndexCollection : System.Collections.IList
        {
            private readonly ModernCheckedListBox _owner;

            internal CheckedIndexCollection(ModernCheckedListBox owner)
            {
                _owner = owner;
            }

            public int Count
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < _owner.Items.Count; i++)
                    {
                        if (_owner.Items[i].Checked)
                            count++;
                    }
                    return count;
                }
            }

            public int this[int index]
            {
                get
                {
                    int checkedCount = 0;
                    for (int i = 0; i < _owner.Items.Count; i++)
                    {
                        if (_owner.Items[i].Checked)
                        {
                            if (checkedCount == index)
                                return i;
                            checkedCount++;
                        }
                    }
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }

            public bool Contains(int index)
            {
                if (index < 0 || index >= _owner.Items.Count)
                    return false;
                return _owner.Items[index].Checked;
            }

            public int IndexOf(int index) => Contains(index) ? index : -1;

            public bool IsReadOnly => true;
            public bool IsFixedSize => false;
            object? System.Collections.IList.this[int index]
            {
                get => this[index];
                set => throw new NotSupportedException();
            }
            public object SyncRoot => this;
            public bool IsSynchronized => false;
            int System.Collections.IList.Add(object? value) => throw new NotSupportedException();
            void System.Collections.IList.Clear() => throw new NotSupportedException();
            bool System.Collections.IList.Contains(object? value) => value is int i && Contains(i);
            int System.Collections.IList.IndexOf(object? value) => value is int i ? IndexOf(i) : -1;
            void System.Collections.IList.Insert(int index, object? value) => throw new NotSupportedException();
            void System.Collections.IList.Remove(object? value) => throw new NotSupportedException();
            void System.Collections.IList.RemoveAt(int index) => throw new NotSupportedException();
            public void CopyTo(Array array, int index)
            {
                for (int i = 0; i < Count; i++)
                    array.SetValue(this[i], index + i);
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                for (int i = 0; i < _owner.Items.Count; i++)
                {
                    if (_owner.Items[i].Checked)
                        yield return i;
                }
            }
        }

        public class CheckedItemCollection : System.Collections.IList
        {
            private readonly ModernCheckedListBox _owner;

            internal CheckedItemCollection(ModernCheckedListBox owner)
            {
                _owner = owner;
            }

            public int Count
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < _owner.Items.Count; i++)
                    {
                        if (_owner.Items[i].Checked)
                            count++;
                    }
                    return count;
                }
            }

            public object? this[int index]
            {
                get
                {
                    int checkedCount = 0;
                    for (int i = 0; i < _owner.Items.Count; i++)
                    {
                        if (_owner.Items[i].Checked)
                        {
                            if (checkedCount == index)
                                return _owner.Items[i].Item;
                            checkedCount++;
                        }
                    }
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }

            public bool Contains(object? item)
            {
                for (int i = 0; i < _owner.Items.Count; i++)
                {
                    if (_owner.Items[i].Checked && _owner.Items[i].Item?.Equals(item) == true)
                        return true;
                }
                return false;
            }

            public int IndexOf(object? item)
            {
                int checkedIndex = 0;
                for (int i = 0; i < _owner.Items.Count; i++)
                {
                    if (_owner.Items[i].Checked)
                    {
                        if (_owner.Items[i].Item?.Equals(item) == true)
                            return checkedIndex;
                        checkedIndex++;
                    }
                }
                return -1;
            }

            public bool IsReadOnly => true;
            public bool IsFixedSize => false;
            object? System.Collections.IList.this[int index]
            {
                get => this[index];
                set => throw new NotSupportedException();
            }
            public object SyncRoot => this;
            public bool IsSynchronized => false;
            int System.Collections.IList.Add(object? value) => throw new NotSupportedException();
            void System.Collections.IList.Clear() => throw new NotSupportedException();
            void System.Collections.IList.Insert(int index, object? value) => throw new NotSupportedException();
            void System.Collections.IList.Remove(object? value) => throw new NotSupportedException();
            void System.Collections.IList.RemoveAt(int index) => throw new NotSupportedException();
            public void CopyTo(Array array, int index)
            {
                for (int i = 0; i < Count; i++)
                    array.SetValue(this[i], index + i);
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                for (int i = 0; i < _owner.Items.Count; i++)
                {
                    if (_owner.Items[i].Checked)
                        yield return _owner.Items[i].Item;
                }
            }
        }
    }
}