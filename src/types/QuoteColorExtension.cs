using Markdig;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.Text;
using System.Text.RegularExpressions;

namespace WaifuAI
{
    public class QuoteColorRenderer : HtmlObjectRenderer<QuoteColorInline>
    {
        protected override void Write(HtmlRenderer renderer, QuoteColorInline obj)
        {
            renderer.Write("<span style=\"color: aquamarine;\">\"")
                    .Write(obj.Content)
                    .Write("\"</span>");
        }
    }

    public class QuoteColorExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            if (!pipeline.InlineParsers.Contains<QuoteColorInlineParser>())
            {
                pipeline.InlineParsers.Insert(0, new QuoteColorInlineParser());
            }
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            if (renderer is HtmlRenderer htmlRenderer)
            {
                if (!htmlRenderer.ObjectRenderers.Contains<QuoteColorRenderer>())
                {
                    htmlRenderer.ObjectRenderers.Insert(0, new QuoteColorRenderer());
                }
            }
        }
    }

    public class QuoteColorInline : LeafInline
    {
        public string Content { get; set; } = string.Empty;
    }

    public class QuoteColorInlineParser : InlineParser
    {
        public QuoteColorInlineParser()
        {
            OpeningCharacters = ['\"', '“'];
        }

        public override bool Match(InlineProcessor processor, ref StringSlice slice)
        {
            // Check if the current character is a double-quote
            if (slice.CurrentChar != '\"' && slice.CurrentChar != '“')
                return false;

            // Save the initial position
            int startPosition = slice.Start;

            // Advance past the opening quote
            slice.NextChar();

            var contentBuilder = new StringBuilder();

            // Iterate through the slice to find the closing quote
            while (slice.CurrentChar != '\0')
            {
                if (slice.CurrentChar == '\"' || slice.CurrentChar == '”')
                {
                    // Found the closing quote
                    // Capture the content and create the inline element
                    var quoteInline = new QuoteColorInline
                    {
                        Content = contentBuilder.ToString(),
                        Span = new SourceSpan(startPosition, slice.Start), // Include the quotes in the span
                        Line = processor.GetSourcePosition(startPosition, out _, out int column),
                        Column = column
                    };

                    // Advance past the closing quote
                    slice.NextChar();
                    // Append the inline element to the processor
                    processor.Inline = quoteInline;

                    return true;
                }
                else
                {
                    // Append the current character to the content
                    contentBuilder.Append(slice.CurrentChar);
                    slice.NextChar();
                }
            }

            // If we reach the end without finding a closing quote, reset the slice position
            slice.Start = startPosition;
            return false;
        }
    }
}
