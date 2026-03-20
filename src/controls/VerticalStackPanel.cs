using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LetheChat.Controls
{
    /// <summary>
    /// Vertical stack layout container:
    /// - Stacks children top->bottom with Gap spacing.
    /// - Respects Padding.
    /// - Optional animation of child height changes.
    /// - Safe to call RequestLayout() during InitializeComponent (no handle yet).
    /// - Use AddControl / InsertControl or add via Controls.Add (it hooks automatically).
    /// </summary>
    [DesignerCategory("Code")]
    [DefaultProperty(nameof(Gap))]
    public class VerticalStackPanel : Panel
    {
        private int _gap = 8;
        private bool _suspend;
        private bool _deferLayoutRequested;
        private bool _autoScrollEnabled;
        private bool _animate;
        private int _animateDuration = 140;
        private bool _autoListenForSizeChanges = true;
        private bool _pendingHandleLayout;

        private readonly Dictionary<Control, AnimationState> _animations = [];
        private readonly Timer _animTimer;

        private sealed class AnimationState
        {
            public int StartH;
            public int TargetH;
            public int CurrentH;
            public long StartTicks;
        }

        public VerticalStackPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            DoubleBuffered = true;
            Padding = new Padding(0, 6, 0, 6);
            base.AutoScroll = false; // controlled by ScrollableContent

            _animTimer = new Timer { Interval = 15 };
            _animTimer.Tick += AnimTick;

            ControlAdded += OnControlAdded;
            ControlRemoved += OnControlRemoved;
        }

        [Category("Layout")]
        [DefaultValue(8)]
        public int Gap
        {
            get => _gap;
            set
            {
                if (value < 0 || value == _gap) return;
                _gap = value;
                RequestLayout();
            }
        }

        [Category("Layout")]
        [DefaultValue(false)]
        public bool ScrollableContent
        {
            get => _autoScrollEnabled;
            set
            {
                if (_autoScrollEnabled == value) return;
                _autoScrollEnabled = value;
                base.AutoScroll = value;
                RequestLayout();
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool Animate
        {
            get => _animate;
            set
            {
                if (_animate == value) return;
                _animate = value;
                if (!_animate)
                {
                    _animations.Clear();
                    if (_animTimer.Enabled) _animTimer.Stop();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(140)]
        public int AnimateDuration
        {
            get => _animateDuration;
            set => _animateDuration = Math.Max(16, value);
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool AutoListenForSizeChanges
        {
            get => _autoListenForSizeChanges;
            set
            {
                if (_autoListenForSizeChanges == value) return;
                _autoListenForSizeChanges = value;
                RewireChildrenSizeHandlers();
            }
        }

        /// <summary>
        /// Adds a control at the bottom.
        /// </summary>
        public void AddControl(Control child)
        {
            if (child == null) return;
            Controls.Add(child);
        }

        /// <summary>
        /// Inserts a control at a visual index (0 = top).
        /// </summary>
        public void InsertControl(int visualIndex, Control child)
        {
            if (child == null) return;
            int insertion = Controls.Count - visualIndex;
            insertion = Math.Max(0, Math.Min(insertion, Controls.Count));
            Controls.Add(child);
            Controls.SetChildIndex(child, insertion);
            RequestLayout();
        }

        /// <summary>
        /// Request a (debounced) layout pass. Safe before handle exists.
        /// </summary>
        public void RequestLayout()
        {
            if (_suspend || IsDisposed) return;

            if (_deferLayoutRequested)
                return;

            _deferLayoutRequested = true;

            // If handle already created, schedule with BeginInvoke.
            if (IsHandleCreated)
            {
                try
                {
                    BeginInvoke(new Action(ExecuteDeferredLayout));
                }
                catch (InvalidOperationException)
                {
                    // Handle race: fallback to direct layout once handle exists.
                    HookHandleCreatedFallback();
                }
            }
            else
            {
                // Defer until handle creation.
                HookHandleCreatedFallback();
            }
        }

        /// <summary>
        /// For rare cases where you want immediate synchronous layout (only after handle exists).
        /// </summary>
        public void ForceImmediateLayout()
        {
            if (IsHandleCreated && !IsDisposed)
            {
                _deferLayoutRequested = false;
                PerformLayout();
                Invalidate();
            }
        }

        private void HookHandleCreatedFallback()
        {
            if (_pendingHandleLayout) return;
            _pendingHandleLayout = true;
            HandleCreated -= OnHandleCreatedDeferred;
            HandleCreated += OnHandleCreatedDeferred;
        }

        private void OnHandleCreatedDeferred(object? sender, EventArgs e)
        {
            HandleCreated -= OnHandleCreatedDeferred;
            _pendingHandleLayout = false;
            if (IsDisposed) return;
            ExecuteDeferredLayout();
        }

        private void ExecuteDeferredLayout()
        {
            if (IsDisposed) return;
            _deferLayoutRequested = false;
            PerformLayout();
            Invalidate();
        }

        /// <summary>
        /// Wrap multiple adds/removes.
        /// </summary>
        public IDisposable SuspendStackLayout()
        {
            _suspend = true;
            return new CallbackDisposable(() =>
            {
                _suspend = false;
                RequestLayout();
            });
        }

        /// <summary>
        /// Animate a height change (call BEFORE you set child.Height to the new value).
        /// </summary>
        public void AnimateHeightChange(Control child, int oldHeight, int newHeight)
        {
            if (child == null || oldHeight == newHeight || !_animate || IsDisposed)
            {
                RequestLayout();
                return;
            }

            // Initialize animation state
            var st = new AnimationState
            {
                StartH = oldHeight,
                TargetH = newHeight,
                CurrentH = oldHeight,
                StartTicks = Stopwatch.GetTimestamp()
            };
            _animations[child] = st;

            if (IsHandleCreated && !_animTimer.Enabled)
                _animTimer.Start();

            RequestLayout();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (_suspend || IsDisposed) return;

            int y = Padding.Top;
            int width = ClientSize.Width - Padding.Left - Padding.Right;

            // Layout in visual order: reverse of Controls collection
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                var c = Controls[i];
                if (!c.Visible) continue;

                int h = GetEffectiveHeight(c);
                c.SetBounds(Padding.Left, y, ResolveChildWidth(c, width), h);
                y += h + _gap;
            }

            if (_autoScrollEnabled)
                AutoScrollMinSize = new Size(0, y - _gap + Padding.Bottom);
        }

        private int GetEffectiveHeight(Control c)
        {
            if (_animate && _animations.TryGetValue(c, out var st))
                return st.CurrentH;
            return c.Height;
        }

        private int ResolveChildWidth(Control c, int defaultWidth)
        {
            // All children stretch horizontally by default. Anchor adjustments are still respected
            return defaultWidth;
        }

        private void AnimTick(object? sender, EventArgs e)
        {
            if (_animations.Count == 0)
            {
                _animTimer.Stop();
                return;
            }

            double freq = Stopwatch.Frequency;
            long now = Stopwatch.GetTimestamp();
            bool anyRunning = false;

            foreach (var kv in new List<KeyValuePair<Control, AnimationState>>(_animations))
            {
                var ctrl = kv.Key;
                var st = kv.Value;
                double elapsedMs = (now - st.StartTicks) * 1000.0 / freq;
                double raw = Math.Min(1.0, elapsedMs / _animateDuration);
                double eased = raw < 0.5 ? 2 * raw * raw : -1 + (4 - 2 * raw) * raw;

                st.CurrentH = st.StartH + (int)((st.TargetH - st.StartH) * eased);

                if (raw >= 1.0)
                {
                    st.CurrentH = st.TargetH;
                    _animations.Remove(ctrl);
                }
                else
                {
                    anyRunning = true;
                }
            }

            // Re-layout based on animated heights
            PerformLayout();
            Invalidate();

            if (!anyRunning)
                _animTimer.Stop();
        }

        private void OnControlAdded(object? sender, ControlEventArgs e)
        {
            if (_autoListenForSizeChanges && e.Control is not null)
            {
                e.Control.SizeChanged -= Child_SizeChanged;
                e.Control.SizeChanged += Child_SizeChanged;
            }
            RequestLayout();
        }

        private void OnControlRemoved(object? sender, ControlEventArgs e)
        {
            if (e.Control is null) return;
            e.Control.SizeChanged -= Child_SizeChanged;
            if (_animations.ContainsKey(e.Control))
                _animations.Remove(e.Control);
            RequestLayout();
        }

        private void Child_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is not Control c) return;

            if (_animate && !_animations.ContainsKey(c))
            {
                // If a control changes height abruptly (e.g. its own animation),
                // we treat it as an instantaneous change (no old height known).
                RequestLayout();
            }
            else
            {
                RequestLayout();
            }
        }

        private void RewireChildrenSizeHandlers()
        {
            foreach (Control c in Controls)
            {
                c.SizeChanged -= Child_SizeChanged;
                if (_autoListenForSizeChanges)
                    c.SizeChanged += Child_SizeChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animTimer.Stop();
                _animTimer.Tick -= AnimTick;
                _animTimer.Dispose();
                foreach (Control c in Controls)
                    c.SizeChanged -= Child_SizeChanged;
            }
            base.Dispose(disposing);
        }

        private sealed class CallbackDisposable(Action cb) : IDisposable
        {
            private Action? _cb = cb;

            public void Dispose()
            {
                _cb?.Invoke();
                _cb = null;
            }
        }
    }
}