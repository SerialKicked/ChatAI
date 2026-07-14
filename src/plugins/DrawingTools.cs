using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LetheAISharp.Agent.Tools;
using LetheChat.Controls;
using LetheChat.Forms;
using OpenAI;

namespace LetheChat.Plugins
{
    /// <summary>
    /// Function-calling toolset that lets the persona draw on a dedicated <see cref="DrawingForm"/>.
    /// The canvas is a fixed-size bitmap whose dimensions come from
    /// <see cref="Files.LetheChatSettings.DrawingCanvasWidth"/> / <c>DrawingCanvasHeight</c>;
    /// the model cannot choose the size, it is told the dimensions when it opens a new drawing.
    /// The drawing form is created lazily the first time the model calls a drawing tool.
    /// </summary>
    internal class DrawingTools : IToolList
    {
        public string Id => "Drawing";

        public string Description => "A set of tools to draw shapes, lines, text and fills on a visual canvas.";

        public string SystemPromptInstruction =>
            "You have access to a drawing canvas. Coordinates are in pixels, with the origin (0,0) at the TOP-LEFT corner; " +
            "x increases to the right and y increases downward. Colors can be given as common names (e.g. 'red', 'skyblue') " +
            "or hex strings (e.g. '#FF8800'). You must call NewDrawing before drawing; it will tell you the exact canvas size. " +
            "Build up an image by calling the drawing tools one after another. Use GetCanvasInfo if you need to recall the size.";

        private readonly List<Tool> toolList = [];

        public IReadOnlyList<Tool> GetToolList() => toolList;

        public bool RequiresConfirmation(string functionName) => false;

        // ── Lazy access to the single drawing form ───────────────────────────

        private DrawingForm? _form;

        /// <summary>
        /// Returns the drawing form, creating and showing it on the UI thread if needed.
        /// </summary>
        private DrawingForm GetForm()
        {
            var main = Program.BigForm ?? throw new InvalidOperationException("Main window is not available.");

            DrawingForm Create()
            {
                if (_form == null || _form.IsDisposed)
                {
                    _form = new DrawingForm();
                    ThemeManager.ApplyToForm(_form);
                    _form.FormClosed += (_, _) => _form = null;
                    _form.Show(main);
                }
                else if (!_form.Visible)
                {
                    _form.Show(main);
                }
                _form.BringToFront();
                return _form;
            }

            if (main.InvokeRequired)
                return (DrawingForm)main.Invoke(Create);
            return Create();
        }

        // ── Tool registration ────────────────────────────────────────────────

        public void LoadTools(bool clearExisting = false)
        {
            toolList.Clear();
            if (clearExisting)
                Tool.ClearRegisteredTools();

            toolList.Add(Tool.GetOrCreateTool(this, nameof(NewDrawing), "Drawing: Opens the drawing canvas (or clears the existing one) to a blank background. The canvas size is fixed by the app settings; this returns the exact width and height you must draw within."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(GetCanvasInfo), "Drawing: Inspects the current canvas. Returns its width and height, plus the bounding box (leftmost/topmost/rightmost/bottommost pixels) of everything drawn so far. Call this between steps to check where your shapes actually landed and whether separate parts are drifting apart or off-center, since you cannot see the canvas directly."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(DrawLine), "Drawing: Draws a straight line between two points."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(DrawRectangle), "Drawing: Draws a rectangle. Set filled=true for a solid rectangle, otherwise only the outline is drawn."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(DrawCircle), "Drawing: Draws a circle from a center point and radius. Set filled=true for a solid disc."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(DrawEllipse), "Drawing: Draws an ellipse/oval inside a bounding box (top-left corner plus width and height). Set filled=true for a solid ellipse."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(DrawPolygon), "Drawing: Draws a polygon (or open polyline) through a list of points. Great for triangles and arbitrary shapes."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(DrawText), "Drawing: Draws a text string at the given position."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(FloodFill), "Drawing: Bucket-fills the contiguous area of the same color starting at a seed pixel, replacing it with a new color."));
            toolList.Add(Tool.GetOrCreateTool(this, nameof(Erase), "Drawing: Erases a rectangular region back to the background color. Pass eraseAll=true to clear the whole canvas."));
        }

        public void UnloadTools()
        {
            foreach (var tool in toolList)
                Tool.TryUnregisterTool(tool);
            toolList.Clear();
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private static Color _background = Color.White;

        /// <summary>
        /// Parses a color from a common name or hex string. Returns false with a message on failure.
        /// </summary>
        private static bool TryParseColor(string value, out Color color, out string error)
        {
            error = string.Empty;
            color = Color.Black;
            if (string.IsNullOrWhiteSpace(value))
            {
                error = "No color provided.";
                return false;
            }
            var v = value.Trim();
            try
            {
                // ColorTranslator handles both '#RRGGBB' and known names like 'Red'.
                color = ColorTranslator.FromHtml(v);
                return true;
            }
            catch
            {
                // Fall back to Color.FromName for names ColorTranslator missed.
                var named = Color.FromName(v);
                if (named.IsKnownColor || named.A != 0)
                {
                    color = named;
                    return true;
                }
                error = $"'{value}' is not a valid color name or hex code (e.g. 'red' or '#FF8800').";
                return false;
            }
        }

        private static string EnsureCanvas(DrawingForm form)
        {
            return form.HasCanvas ? string.Empty : "No canvas exists yet. Call NewDrawing first.";
        }

        // ── Tools ────────────────────────────────────────────────────────────

        public async Task<string> NewDrawing(
            [FunctionParameter("Background color of the new canvas, as a name or hex string. Defaults to white.")] string backgroundColor = "white")
        {
            await Task.Delay(1).ConfigureAwait(false);
            if (!TryParseColor(backgroundColor, out var bg, out var err))
                return err;
            _background = bg;
            int w = Program.Settings.DrawingCanvasWidth;
            int h = Program.Settings.DrawingCanvasHeight;
            var form = GetForm();
            form.NewCanvas(w, h, bg);
            return $"New drawing created. The canvas is {w} pixels wide and {h} pixels tall. Origin (0,0) is the top-left corner.";
        }

        public async Task<string> GetCanvasInfo()
        {
            await Task.Delay(1).ConfigureAwait(false);
            if (_form == null || _form.IsDisposed || !_form.HasCanvas)
                return "No canvas exists yet. Call NewDrawing to create one.";
            int w = _form.CanvasWidth, h = _form.CanvasHeight;
            var bounds = _form.GetDrawnBounds();
            if (bounds.IsEmpty)
                return $"Canvas is {w} x {h} pixels and currently blank. Origin (0,0) is the top-left corner; x increases right, y increases down.";
            int centerX = bounds.X + bounds.Width / 2;
            int centerY = bounds.Y + bounds.Height / 2;
            return $"Canvas is {w} x {h} pixels. Origin (0,0) is the top-left corner; x increases right, y increases down. "
                + $"Everything drawn so far fits in a bounding box from top-left ({bounds.Left},{bounds.Top}) to bottom-right ({bounds.Right - 1},{bounds.Bottom - 1}), "
                + $"which is {bounds.Width} wide by {bounds.Height} tall, centered at ({centerX},{centerY}). "
                + $"The canvas center is ({w / 2},{h / 2}); compare these to judge whether your drawing is well-centered or whether parts have drifted apart.";
        }

        public async Task<string> DrawLine(
            [FunctionParameter("X coordinate of the line's start point, in pixels.")] int x1,
            [FunctionParameter("Y coordinate of the line's start point, in pixels.")] int y1,
            [FunctionParameter("X coordinate of the line's end point, in pixels.")] int x2,
            [FunctionParameter("Y coordinate of the line's end point, in pixels.")] int y2,
            [FunctionParameter("Line color, as a name or hex string. Defaults to black.")] string color = "black",
            [FunctionParameter("Line thickness in pixels. Defaults to 2.")] int thickness = 2)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (!TryParseColor(color, out var c, out var err)) return err;
            form.DrawLine(x1, y1, x2, y2, c, thickness);
            return $"Drew a {color} line from ({x1},{y1}) to ({x2},{y2}).";
        }

        public async Task<string> DrawRectangle(
            [FunctionParameter("X coordinate of the rectangle's top-left corner, in pixels.")] int x,
            [FunctionParameter("Y coordinate of the rectangle's top-left corner, in pixels.")] int y,
            [FunctionParameter("Rectangle width in pixels.")] int width,
            [FunctionParameter("Rectangle height in pixels.")] int height,
            [FunctionParameter("Rectangle color, as a name or hex string. Defaults to black.")] string color = "black",
            [FunctionParameter("If true, the rectangle is filled solid; otherwise only its outline is drawn. Defaults to false.")] bool filled = false,
            [FunctionParameter("Outline thickness in pixels (ignored when filled). Defaults to 2.")] int thickness = 2)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (!TryParseColor(color, out var c, out var err)) return err;
            form.DrawRectangle(x, y, width, height, c, filled, thickness);
            return $"Drew a {(filled ? "filled " : string.Empty)}{color} rectangle at ({x},{y}), size {width}x{height}.";
        }

        public async Task<string> DrawCircle(
            [FunctionParameter("X coordinate of the circle's center, in pixels.")] int centerX,
            [FunctionParameter("Y coordinate of the circle's center, in pixels.")] int centerY,
            [FunctionParameter("Circle radius in pixels.")] int radius,
            [FunctionParameter("Circle color, as a name or hex string. Defaults to black.")] string color = "black",
            [FunctionParameter("If true, the circle is filled solid; otherwise only its outline is drawn. Defaults to false.")] bool filled = false,
            [FunctionParameter("Outline thickness in pixels (ignored when filled). Defaults to 2.")] int thickness = 2)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (!TryParseColor(color, out var c, out var err)) return err;
            if (radius <= 0) return "Radius must be a positive number.";
            form.DrawEllipse(centerX - radius, centerY - radius, radius * 2, radius * 2, c, filled, thickness);
            return $"Drew a {(filled ? "filled " : string.Empty)}{color} circle centered at ({centerX},{centerY}) with radius {radius}.";
        }

        public async Task<string> DrawEllipse(
            [FunctionParameter("X coordinate of the bounding box's top-left corner, in pixels.")] int x,
            [FunctionParameter("Y coordinate of the bounding box's top-left corner, in pixels.")] int y,
            [FunctionParameter("Width of the bounding box in pixels.")] int width,
            [FunctionParameter("Height of the bounding box in pixels.")] int height,
            [FunctionParameter("Ellipse color, as a name or hex string. Defaults to black.")] string color = "black",
            [FunctionParameter("If true, the ellipse is filled solid; otherwise only its outline is drawn. Defaults to false.")] bool filled = false,
            [FunctionParameter("Outline thickness in pixels (ignored when filled). Defaults to 2.")] int thickness = 2)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (!TryParseColor(color, out var c, out var err)) return err;
            form.DrawEllipse(x, y, width, height, c, filled, thickness);
            return $"Drew a {(filled ? "filled " : string.Empty)}{color} ellipse in box ({x},{y}) size {width}x{height}.";
        }

        public async Task<string> DrawPolygon(
            [FunctionParameter("The polygon vertices as 'x1,y1;x2,y2;x3,y3' pixel pairs separated by semicolons. Minimum two points.")] string points,
            [FunctionParameter("Line/fill color, as a name or hex string. Defaults to black.")] string color = "black",
            [FunctionParameter("If true, the polygon is filled solid (requires at least three points); otherwise only its outline is drawn. Defaults to false.")] bool filled = false,
            [FunctionParameter("Outline thickness in pixels (ignored when filled). Defaults to 2.")] int thickness = 2)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (!TryParseColor(color, out var c, out var err)) return err;

            var parsed = new List<Point>();
            foreach (var pair in points.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                var xy = pair.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (xy.Length != 2
                    || !int.TryParse(xy[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var px)
                    || !int.TryParse(xy[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var py))
                    return $"Could not parse point '{pair}'. Use the format 'x1,y1;x2,y2;...'.";
                parsed.Add(new Point(px, py));
            }
            if (parsed.Count < 2)
                return "A polygon needs at least two points.";
            form.DrawPolygon(parsed.ToArray(), c, filled, thickness);
            return $"Drew a {(filled ? "filled " : string.Empty)}{color} polygon with {parsed.Count} points.";
        }

        public async Task<string> DrawText(
            [FunctionParameter("X coordinate where the text starts (top-left of the text), in pixels.")] int x,
            [FunctionParameter("Y coordinate where the text starts (top-left of the text), in pixels.")] int y,
            [FunctionParameter("The text to draw.")] string text,
            [FunctionParameter("Text color, as a name or hex string. Defaults to black.")] string color = "black",
            [FunctionParameter("Font size in pixels. Defaults to 16.")] int fontSize = 16)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (string.IsNullOrEmpty(text)) return "No text provided.";
            if (!TryParseColor(color, out var c, out var err)) return err;
            form.DrawText(x, y, text, c, fontSize);
            return $"Drew the text \"{text}\" at ({x},{y}).";
        }

        public async Task<string> FloodFill(
            [FunctionParameter("X coordinate of the seed pixel to start filling from, in pixels.")] int x,
            [FunctionParameter("Y coordinate of the seed pixel to start filling from, in pixels.")] int y,
            [FunctionParameter("Fill color, as a name or hex string.")] string color)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (!TryParseColor(color, out var c, out var err)) return err;
            if (x < 0 || y < 0 || x >= form.CanvasWidth || y >= form.CanvasHeight)
                return $"Seed point ({x},{y}) is outside the {form.CanvasWidth}x{form.CanvasHeight} canvas.";
            var ok = form.FloodFill(x, y, c);
            return ok ? $"Flood-filled the area at ({x},{y}) with {color}." : "Flood fill failed.";
        }

        public async Task<string> Erase(
            [FunctionParameter("X coordinate of the region's top-left corner, in pixels. Ignored if eraseAll is true.")] int x = 0,
            [FunctionParameter("Y coordinate of the region's top-left corner, in pixels. Ignored if eraseAll is true.")] int y = 0,
            [FunctionParameter("Width of the region to erase in pixels. Ignored if eraseAll is true.")] int width = 0,
            [FunctionParameter("Height of the region to erase in pixels. Ignored if eraseAll is true.")] int height = 0,
            [FunctionParameter("If true, erases the entire canvas back to the background color. Defaults to false.")] bool eraseAll = false)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var form = GetForm();
            var missing = EnsureCanvas(form);
            if (missing != string.Empty) return missing;
            if (eraseAll)
            {
                form.ClearCanvas(_background);
                return "Erased the entire canvas.";
            }
            if (width <= 0 || height <= 0)
                return "Provide a positive width and height to erase, or set eraseAll=true.";
            form.EraseRegion(x, y, width, height, _background);
            return $"Erased the region at ({x},{y}), size {width}x{height}.";
        }
    }
}
