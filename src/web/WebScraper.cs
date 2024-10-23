using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Fluid.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaifuAI.Files;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WaifuAI.Web
{
    public class WEntry
    {
        public string Title = string.Empty;
        public string Picture = string.Empty;
        public string Link = string.Empty;
        public DateTime Date = default;
        public List<(string Name, string Link)> Tags = [];
        public string Article = string.Empty;
    }

    public class WListing
    {
        public string Title = string.Empty;
        public string Link = string.Empty;
        public List<WEntry> Entries = [];
        public int CurrentPage = 1;
        public int PageCount = 1;

        public string ExportToMarkdown()
        {
            var result = new StringBuilder();
            result.AppendLinuxLine($"{Title}").AppendLinuxLine();
            var x = 0;
            foreach (var entry in Entries)
            {
                result.AppendLinuxLine($"{x}. {entry.Title}");
                x++;
                if (entry.Tags.Count > 0)
                {
                    var tags = new StringBuilder();
                    // make comma separated list of tags
                    foreach (var tag in entry.Tags)
                    {
                        tags.Append(tag.Name).Append(", ");
                    }
                    // remove last comma
                    tags.Remove(tags.Length - 2, 2);
                    result.AppendLinuxLine("(Tags: " + tags.ToString());
                }
                if (!string.IsNullOrEmpty(entry.Article))
                    result.AppendLinuxLine("Summary: " + entry.Article.Replace("\n\n", " ").Replace("\n", " ").Trim());
                result.AppendLinuxLine();
            }
            return result.ToString();
        }
    }

    public class WQuery
    {
        public string[] Selectors { get; set; } = [];
        public string Attribute { get; set; } = string.Empty;
        public string Listing { get; set; } = string.Empty;

        public string RunQuery(IParentNode element)
        {
            var found = element;
            foreach (var selector in Selectors)
            {
                found = found.QuerySelector(selector);
                if (found == null)
                    return string.Empty;
            }
            if (string.IsNullOrEmpty(Attribute) && found is IElement el)
                return el.TextContent;
            return (found as IElement)?.GetAttribute(Attribute) ?? string.Empty;
        }

        public IHtmlCollection<IElement>? RunListQuery(IElement element)
        {
            IElement? found = element;
            foreach (var selector in Selectors)
            {
                found = found.QuerySelector(selector);
                if (found == null)
                    return default;
            }
            return found.QuerySelectorAll(Listing);
        }
    }

    public enum PageType
    {
        FrontPage,
        ListingPage,
        ArticlePage,
        SearchPage
    }

    public class WLink
    {
        public string ID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public PageType Category { get; set; } = PageType.FrontPage;
        public string URL { get; set; } = string.Empty;
    }

    public class WebsiteDefinition : BaseFile
    {
        public string WebsiteName { get; set; } = "";
        public string WebsiteInfo { get; set; } = "";
        public string CommandID { get; set; } = "";
        public string Address { get; set; } = "";
        public List<WLink> MainLinks { get; set; } = [];
        public string ListingCellSelector { get; set; } = "";
        public string ListingDateFormat { get; set; } = "d MMM, yy";
        public WQuery ListingTitleSelector { get; set; } = new();
        public WQuery ListingPictureSelector { get; set; } = new();
        public WQuery ListingLinkSelector { get; set;  } = new();
        public WQuery ListingDateSelector { get; set; } = new(); 
        public WQuery SubListingSelector { get; set;  } = new();
        public WQuery PageCounterSelector { get; } = new();
        public WQuery PageContentSelector { get; } = new();

        public string RenderFrontPage(string Goal)
        {
            var str = new StringBuilder();
            str.AppendLinuxLine($"# {WebsiteName}");
            str.AppendLinuxLine($"{WebsiteInfo}").AppendLinuxLine();
            str.AppendLinuxLine("## Available Links");
            var x = 0;
            foreach (var item in MainLinks)
            {
                str.AppendLinuxLine($"{x}. {item.Title}");
                x++;
            }
            str.AppendLinuxLine();
            str.AppendLinuxLine("## Instructions");
            str.AppendLinuxLine("To retrieve information from this website, type the number corresponding to the link you want to visit. Only write the number, nothing else.");
            if (!string.IsNullOrEmpty(Goal))
                str.AppendLinuxLine(Goal);
            return str.ToString();
        }

        public string RenderPage(string LinkID, string Goal)
        {
            var link = MainLinks.FirstOrDefault(l => l.ID == LinkID);
            if (link == null)
                return string.Empty;
            var str = new StringBuilder();
            str.AppendLinuxLine($"# {link.Title}");
            str.AppendLinuxLine($"{link.Body}").AppendLinuxLine();




            str.AppendLinuxLine("## Instructions");
            switch (link.Category)
            {
                case PageType.FrontPage:
                    str.AppendLinuxLine("To retrieve information from this website, type the number corresponding to the link you want to visit. Only write the number, nothing else.");
                    break;
                case PageType.ListingPage:
                    str.AppendLinuxLine("To open one of the links above, type the number corresponding to the link you want to visit, only write the number, nothing else. Any other input will send you back to the front page.");
                    break;
                case PageType.ArticlePage:
                    str.AppendLinuxLine("You have selected this page. If you want to send it to {{user}}, type SEND. Nothing else. Any other input will send you back to the front page.");
                    break;
                case PageType.SearchPage:
                    str.AppendLinuxLine("Type the search terms you're looking for to complete your request. Only type those search terms and nothing else.");
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(Goal))
                str.AppendLinuxLine(Goal);
            return str.ToString();
        }
    }


    public class WebScraper
    {
        public string Address { get; set; } = "";
        
        private readonly IConfiguration config = Configuration.Default.WithDefaultLoader();
        private readonly IBrowsingContext context;

        public WebScraper() 
        {
            context = BrowsingContext.New(config);
        }

        public async Task<IHtmlCollection<IElement>> FindCells(string address, string cellselector)
        {
            var document = await context.OpenAsync(address);
            var cells = document.QuerySelectorAll(cellselector);
            return cells;
        }

        private DateTime StringToDate(string textdate, string format)
        {
            // Parse the string into a DateTime object
            if (DateTime.TryParseExact(textdate, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            else
            {
                return default;
            }
        }

        public async Task<WListing> ParseWebListing(string page, WebsiteDefinition web, bool innerscan)
        {
            var document = await context.OpenAsync(page);
            if (document == null)
                return new WListing();
            var res = new WListing()
            {
                Link = page,
                Title = document.Title ?? string.Empty
            };
            var cells = document.QuerySelectorAll(web.ListingCellSelector);
            var entries = new List<WEntry>(cells.Length);
            foreach (var cell in cells)
            {
                var entry = new WEntry()
                {
                    Picture = web.ListingPictureSelector.RunQuery(cell),
                    Title = web.ListingTitleSelector.RunQuery(cell),
                    Link = web.ListingLinkSelector.RunQuery(cell)
                };
                // remove all occurences of "\n" in entry.Title
                entry.Title = entry.Title.Replace("\n", string.Empty).Trim();
                if (web.SubListingSelector.Selectors?.Length > 0)
                {
                    var taglist = web.SubListingSelector.RunListQuery(cell);
                    if (taglist?.Length > 0)
                    {
                        foreach (var element in taglist)
                        {
                            var name = element.TextContent;
                            var link = element.GetAttribute("href");
                            name = name.Replace("\n", string.Empty).Trim();
                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(link))
                                entry.Tags.Add((name, link));
                        }
                    }
                }
                // retrieve posting date
                var dateinfo = web.ListingDateSelector.RunQuery(cell);
                entry.Date = StringToDate(dateinfo, web.ListingDateFormat);
                entries.Add(entry);
                if (innerscan)
                {
                    var content = await GetPageContent(entry.Link, web);
                    entry.Article = content;
                }
            }
            res.Entries = entries;
            var pages = web.PageCounterSelector.RunQuery(document);
            // retrieve current and last page numbers
            if (!string.IsNullOrEmpty(pages))
            {
                var parts = pages.Split(" of ");
                if (parts.Length == 2)
                {
                    var firstpart = parts[0].Replace("Page ", string.Empty).Replace(",", string.Empty);
                    var secondpart = parts[1].Replace(",", string.Empty);
                    if (int.TryParse(firstpart, out int current))
                        res.CurrentPage = current;
                    if (int.TryParse(secondpart, out int last))
                        res.PageCount = last;
                }
            }
            return res;
        }

        public async Task<string> GetPageContent(string page, WebsiteDefinition web)
        {
            var document = await context.OpenAsync(page);
            if (document == null)
                return string.Empty;
            var content = web.PageContentSelector.RunQuery(document);
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Trim('\n').Trim();
            }
            return content;
        }

    }
}
