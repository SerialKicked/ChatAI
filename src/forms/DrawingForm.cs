using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LetheChat.Forms
{
    /// <summary>
    /// A simple bitmap-backed drawing surface driven by the "Drawing" LLM toolset.
    /// All public draw methods are thread-safe: they marshal onto the UI thread if needed,
    /// so they can be called directly from tool methods running off the UI thread.
    /// The canvas size is dictated by <see cref="Files.LetheChatSettings"/>, not by the model.
    /// </summary>
    public partial class DrawingForm : Form
    {
        private Bitmap? _canvas;
        private Graphics? _gfx;

        private Color _bgColor = Color.White;

        public int CanvasWidth => _canvas?.Width ?? 0;
        public int CanvasHeight => _canvas?.Height ?? 0;
        public bool HasCanvas => _canvas != null;

        public DrawingForm()
        {
            InitializeComponent();
        }

        // ── Thread marshaling helper ─────────────────────────────────────────

        private T RunOnUi<T>(Func<T> func)
        {
            if (InvokeRequired)
                return (T)Invoke(func);
            return func();
        }

        private void RunOnUi(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        // ── Canvas lifecycle ─────────────────────────────────────────────────

        /// <summary>
        /// Creates a fresh canvas of the given size filled with the background color,
        /// discarding any previous drawing. Called by the NewDrawing tool.
        /// </summary>
        public void NewCanvas(int width, int height, Color background)
        {
            RunOnUi(() =>
            {
                _gfx?.Dispose();
                _canvas?.Dispose();

                _canvas = new Bitmap(Math.Max(1, width), Math.Max(1, height), PixelFormat.Format32bppArgb);
                _gfx = Graphics.FromImage(_canvas);
                _gfx.SmoothingMode = SmoothingMode.AntiAlias;
                _gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                _gfx.Clear(background);
                _bgColor = background;

                picCanvas.Image = _canvas;
                picCanvas.Size = _canvas.Size;
                UpdateInfo();
                Refresh();
            });
        }

        // ── Primitives (each returns after refreshing the display) ───────────

        public void DrawLine(int x1, int y1, int x2, int y2, Color color, int thickness)
        {
            RunOnUi(() =>
            {
                if (_gfx == null) return;
                using var pen = new Pen(color, Math.Max(1, thickness));
                _gfx.DrawLine(pen, x1, y1, x2, y2);
                Present();
            });
        }

        public void DrawRectangle(int x, int y, int width, int height, Color color, bool filled, int thickness)
        {
            RunOnUi(() =>
            {
                if (_gfx == null) return;
                // Normalize negative width/height so it draws regardless of corner order.
                var rect = new Rectangle(Math.Min(x, x + width), Math.Min(y, y + height), Math.Abs(width), Math.Abs(height));
                if (filled)
                {
                    using var brush = new SolidBrush(color);
                    _gfx.FillRectangle(brush, rect);
                }
                else
                {
                    using var pen = new Pen(color, Math.Max(1, thickness));
                    _gfx.DrawRectangle(pen, rect);
                }
                Present();
            });
        }

        public void DrawEllipse(int x, int y, int width, int height, Color color, bool filled, int thickness)
        {
            RunOnUi(() =>
            {
                if (_gfx == null) return;
                var rect = new Rectangle(Math.Min(x, x + width), Math.Min(y, y + height), Math.Abs(width), Math.Abs(height));
                if (filled)
                {
                    using var brush = new SolidBrush(color);
                    _gfx.FillEllipse(brush, rect);
                }
                else
                {
                    using var pen = new Pen(color, Math.Max(1, thickness));
                    _gfx.DrawEllipse(pen, rect);
                }
                Present();
            });
        }

        public void DrawPolygon(Point[] points, Color color, bool filled, int thickness)
        {
            RunOnUi(() =>
            {
                if (_gfx == null || points.Length < 2) return;
                if (filled && points.Length >= 3)
                {
                    using var brush = new SolidBrush(color);
                    _gfx.FillPolygon(brush, points);
                }
                else
                {
                    using var pen = new Pen(color, Math.Max(1, thickness));
                    _gfx.DrawPolygon(pen, points);
                }
                Present();
            });
        }

        public void DrawText(int x, int y, string text, Color color, float fontSize)
        {
            RunOnUi(() =>
            {
                if (_gfx == null) return;
                using var font = new Font("Segoe UI", Math.Max(1f, fontSize), FontStyle.Regular, GraphicsUnit.Pixel);
                using var brush = new SolidBrush(color);
                _gfx.DrawString(text, font, brush, x, y);
                Present();
            });
        }

        public void EraseRegion(int x, int y, int width, int height, Color background)
        {
            RunOnUi(() =>
            {
                if (_gfx == null) return;
                var rect = new Rectangle(Math.Min(x, x + width), Math.Min(y, y + height), Math.Abs(width), Math.Abs(height));
                using var brush = new SolidBrush(background);
                _gfx.FillRectangle(brush, rect);
                Present();
            });
        }

        public void ClearCanvas(Color background)
        {
            RunOnUi(() =>
            {
                if (_gfx == null) return;
                _gfx.Clear(background);
                Present();
            });
        }

        /// <summary>
        /// Scanline flood fill from a seed pixel, replacing the contiguous region
        /// of the seed color with the fill color.
        /// </summary>
        public bool FloodFill(int x, int y, Color fill)
        {
            return RunOnUi(() =>
            {
                if (_canvas == null) return false;
                if (x < 0 || y < 0 || x >= _canvas.Width || y >= _canvas.Height) return false;

                var target = _canvas.GetPixel(x, y);
                var fillArgb = fill.ToArgb();
                if (target.ToArgb() == fillArgb) return true; // already that color

                int w = _canvas.Width, h = _canvas.Height;
                var bmpData = _canvas.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                try
                {
                    int stride = bmpData.Stride;
                    var buffer = new int[w * h];
                    // Read via a row-by-row copy to respect stride.
                    unsafe
                    {
                        byte* scan0 = (byte*)bmpData.Scan0;
                        for (int row = 0; row < h; row++)
                        {
                            System.Runtime.InteropServices.Marshal.Copy((IntPtr)(scan0 + row * stride), buffer, row * w, w);
                        }
                    }

                    int targetArgb = target.ToArgb();
                    var stack = new Stack<Point>();
                    stack.Push(new Point(x, y));
                    while (stack.Count > 0)
                    {
                        var p = stack.Pop();
                        int px = p.X;
                        // move to the left edge of the span
                        while (px >= 0 && buffer[p.Y * w + px] == targetArgb) px--;
                        px++;
                        bool spanUp = false, spanDown = false;
                        while (px < w && buffer[p.Y * w + px] == targetArgb)
                        {
                            buffer[p.Y * w + px] = fillArgb;
                            if (p.Y > 0)
                            {
                                bool up = buffer[(p.Y - 1) * w + px] == targetArgb;
                                if (up && !spanUp) { stack.Push(new Point(px, p.Y - 1)); spanUp = true; }
                                else if (!up) spanUp = false;
                            }
                            if (p.Y < h - 1)
                            {
                                bool down = buffer[(p.Y + 1) * w + px] == targetArgb;
                                if (down && !spanDown) { stack.Push(new Point(px, p.Y + 1)); spanDown = true; }
                                else if (!down) spanDown = false;
                            }
                            px++;
                        }
                    }

                    unsafe
                    {
                        byte* scan0 = (byte*)bmpData.Scan0;
                        for (int row = 0; row < h; row++)
                        {
                            System.Runtime.InteropServices.Marshal.Copy(buffer, row * w, (IntPtr)(scan0 + row * stride), w);
                        }
                    }
                }
                finally
                {
                    _canvas.UnlockBits(bmpData);
                }
                Present();
                return true;
            });
        }

        // ── Rendering / info ─────────────────────────────────────────────────

        /// <summary>
        /// Scans the bitmap and returns the tight bounding box of every pixel that differs
        /// from the background color, i.e. the extent of everything drawn so far.
        /// Returns <see cref="Rectangle.Empty"/> when the canvas is blank.
        /// </summary>
        public Rectangle GetDrawnBounds()
        {
            return RunOnUi(() =>
            {
                if (_canvas == null) return Rectangle.Empty;
                int w = _canvas.Width, h = _canvas.Height;
                int bgArgb = _bgColor.ToArgb();

                var bmpData = _canvas.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                try
                {
                    int stride = bmpData.Stride;
                    var row = new int[w];
                    int minX = w, minY = h, maxX = -1, maxY = -1;
                    unsafe
                    {
                        byte* scan0 = (byte*)bmpData.Scan0;
                        for (int y = 0; y < h; y++)
                        {
                            System.Runtime.InteropServices.Marshal.Copy((IntPtr)(scan0 + y * stride), row, 0, w);
                            for (int x = 0; x < w; x++)
                            {
                                if (row[x] != bgArgb)
                                {
                                    if (x < minX) minX = x;
                                    if (x > maxX) maxX = x;
                                    if (y < minY) minY = y;
                                    if (y > maxY) maxY = y;
                                }
                            }
                        }
                    }
                    if (maxX < 0) return Rectangle.Empty;
                    return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
                }
                finally
                {
                    _canvas.UnlockBits(bmpData);
                }
            });
        }

        private void Present()
        {
            picCanvas.Invalidate();
        }

        private void UpdateInfo()
        {
            lblInfo.Text = _canvas == null ? "No canvas." : $"Canvas: {_canvas.Width} x {_canvas.Height}";
        }

        // ── Save button ──────────────────────────────────────────────────────

        private void btSave_Click(object? sender, EventArgs e)
        {
            if (_canvas == null)
            {
                MessageBox.Show(this, "There is nothing to save yet.", "Save as PNG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using var dlg = new SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png",
                DefaultExt = "png",
                FileName = $"drawing_{DateTime.Now:yyyyMMdd_HHmmss}.png"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _canvas.Save(dlg.FileName, ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Failed to save: {ex.Message}", "Save as PNG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
