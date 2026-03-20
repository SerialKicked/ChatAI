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
    public class ModernListBox : UserControl, IThemeAware
    {
        public bool ThemeProcessInnerComponent => true;

        private readonly DoubleBufferedPanel _itemPanel;
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

        public ModernListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.Selectable, true);

            Items = new ListBoxItemCollection(this);

            // Create item display panel
            _itemPanel = new DoubleBufferedPanel
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
                    _itemPanel.Invalidate();
                    OnSelectedIndexChanged(EventArgs.Empty);
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public ListBoxItemCollection Items { get; }

        [Category("Behavior")]
        public event EventHandler? SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

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

                // Clamp scroll offset
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

            _scrollTrackRect = new Rectangle(
                2,
                2,
                _scrollPanel.Width - 4,
                _scrollPanel.Height - 4
            );

            int totalContentHeight = Items.Count * _itemHeight;
            int viewHeight = _itemPanel.Height;

            // Thumb height proportional to visible content
            float ratio = (float)viewHeight / totalContentHeight;
            int thumbHeight = Math.Max(30, (int)(_scrollTrackRect.Height * ratio));

            // Thumb position based on scroll offset
            float scrollRatio = _scrollBarMaxValue > 0
                ? (float)_scrollOffset / _scrollBarMaxValue
                : 0f;
            int availableTravel = _scrollTrackRect.Height - thumbHeight;
            int thumbY = _scrollTrackRect.Y + (int)(availableTravel * scrollRatio);

            _scrollThumbRect = new Rectangle(
                _scrollTrackRect.X,
                thumbY,
                _scrollTrackRect.Width,
                thumbHeight
            );
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
            if (index != _hoverIndex)
            {
                _hoverIndex = index;
                _itemPanel.Invalidate();
            }
        }

        private void ItemPanel_MouseLeave(object? sender, EventArgs e)
        {
            if (_hoverIndex != -1)
            {
                _hoverIndex = -1;
                _itemPanel.Invalidate();
            }
        }

        private void ItemPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int index = GetItemIndexAtPoint(e.Location);
            if (index >= 0 && index < Items.Count)
            {
                _hoverIndex = -1;
                SelectedIndex = index;
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

            // Draw thumb
            Color thumbColor = _scrollThumbDragging
                ? _accentHover
                : (_scrollThumbHovered ? _scrollThumbHover : _scrollThumb);

            using var thumbBrush = new SolidBrush(thumbColor);

            // Rounded rectangle for thumb
            var thumbPath = GetRoundedRect(_scrollThumbRect, 3);
            g.FillPath(thumbBrush, thumbPath);
        }

        private static GraphicsPath GetRoundedRect(Rectangle rect, int radius)
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
                // Calculate new scroll position based on drag
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
                // Start dragging thumb
                _scrollThumbDragging = true;
                _scrollDragStartY = e.Y;
                _scrollDragStartValue = _scrollOffset;
                _scrollPanel.Capture = true;
                _scrollPanel.Invalidate();
            }
            else if (_scrollTrackRect.Contains(e.Location))
            {
                // Click on track - jump to position
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

                // Re-check hover state
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
            bool isHovered = index == _hoverIndex && !isSelected; //             bool isHovered = index == _hoverIndex;

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

            // Text
            string text = Items[index]?.ToString() ?? string.Empty;
            Color textColor = isSelected ? Color.White : _text;

            var textRect = new Rectangle(
                bounds.X + 12,
                bounds.Y,
                bounds.Width - 24,
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Border is handled by BackColor = _border and Padding
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return keyData switch
            {
                Keys.Up or Keys.Down or Keys.PageUp or Keys.PageDown or Keys.Home or Keys.End => true,
                _ => base.IsInputKey(keyData),
            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (Items.Count == 0)
                return;

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
        // Custom Collection
        // =========================================================

        private sealed class DoubleBufferedPanel : Panel
        {
            public DoubleBufferedPanel()
            {
                SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.UserPaint,
                    true);
            }
        }

        public class ListBoxItemCollection
        {
            private readonly ModernListBox _owner;
            private readonly System.Collections.ArrayList _items = [];

            internal ListBoxItemCollection(ModernListBox owner)
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
                    _owner.UpdateScrollBar();
                    _owner.Invalidate();
                }
            }

            public int Add(object? item)
            {
                int result = _items.Add(item);
                _owner.UpdateScrollBar();
                _owner.Invalidate();
                return result;
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

            public bool Contains(object? item) => _items.Contains(item);
            public int IndexOf(object? item) => _items.IndexOf(item);

            public void Insert(int index, object? item)
            {
                _items.Insert(index, item);
                if (_owner._selectedIndex >= index)
                    _owner._selectedIndex++;
                _owner.UpdateScrollBar();
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
                _owner.UpdateScrollBar();
                _owner.Invalidate();
            }

            public void CopyTo(Array array, int index) => _items.CopyTo(array, index);
            public System.Collections.IEnumerator GetEnumerator() => _items.GetEnumerator();

            public void AddRange(object[] items)
            {
                _items.AddRange(items);
                _owner.UpdateScrollBar();
                _owner.Invalidate();
            }
        }
    }
}