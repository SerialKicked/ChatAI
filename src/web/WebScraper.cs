using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AIToolkit.Files;
using AIToolkit;

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

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLinuxLine($"**{Title}**");
            if (Tags.Count > 0)
            {
                var tags = new StringBuilder();
                // make comma separated list of tags
                foreach (var tag in Tags)
                {
                    tags.Append(tag.Name).Append(", ");
                }
                // remove last comma
                tags.Remove(tags.Length - 2, 2);
                str.AppendLinuxLine("- Tags: " + tags.ToString());
            }
            if (!string.IsNullOrEmpty(Article))
                str.AppendLinuxLine("- Summary: " + Article.Replace("\n\n", " ").Replace("\n", " ").Trim());
            str.AppendLinuxLine($"- [Link]({Link})");
            return str.ToString();
        }
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
                    result.AppendLinuxLine("(Tags: " + tags.ToString()+")");
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
            if (found == null)
                return string.Empty;
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
        MetaPage,
        SearchPage
    }

    public class WLink
    {
        public string ID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public PageType Category { get; set; } = PageType.ListingPage;
        public bool InnerScan { get; set; } = false;
        public string URL { get; set; } = string.Empty;
    }

    public class WebsiteDefinition : BaseFile
    {
        public bool AllowComplexNavigation = false;
        public string WebsiteName { get; set; } = "";
        public string TaskQuery { get; set; } = "";
        public string WebsiteInfo { get; set; } = "";
        public string CommandID { get; set; } = "";
        public string Address { get; set; } = "";
        public List<WLink> MainLinks { get; set; } = [];
        public Dictionary<string, List<WLink>> SubLinks { get; set; } = [];
        public string ListingCellSelector { get; set; } = "";
        public string ListingDateFormat { get; set; } = "d MMM, yy";
        public WQuery ListingTitleSelector { get; set; } = new();
        public WQuery ListingPictureSelector { get; set; } = new();
        public WQuery ListingLinkSelector { get; set;  } = new();
        public WQuery ListingDateSelector { get; set; } = new(); 
        public WQuery SubListingSelector { get; set;  } = new();
        public WQuery PageCounterSelector { get; } = new();
        public WQuery PageContentSelector { get; } = new();

        [JsonIgnore] public WListing CurrentListing = new();


        public string RenderFrontPage(string Goal)
        {
            var str = new StringBuilder();
            str.AppendLinuxLine($"# LLM Web Browser");
            str.AppendLinuxLine($"## {WebsiteName}");
            str.AppendLinuxLine($"{WebsiteInfo}").AppendLinuxLine();
            str.AppendLinuxLine("## Available Links");
            var x = 1;
            foreach (var item in MainLinks)
            {
                str.AppendLinuxLine($"{x}. {item.Title}");
                x++;
            }
            if (!string.IsNullOrEmpty(Goal))
            {
                str.AppendLinuxLine();
                str.AppendLinuxLine("# Goal:");
                str.AppendLinuxLine(Goal);
            }
            str.AppendLinuxLine();
            str.AppendLinuxLine("# Rules:");
            str.AppendLinuxLine("- If one of the links above corresponds to the request, answer with the corresponding number only, nothing else.");
            str.AppendLinuxLine("- If no task in the list above corresponds to what the user requested, state the reason why.");
            str.AppendLinuxLine("- Pick one single option.");
            str.AppendLinuxLine("- Do not add any commentary or names.");
            return str.ToString();
        }

        public async Task<string> RenderPage(string LinkID, string Goal, WebScraper scraper)
        {
            var link = MainLinks.FirstOrDefault(l => l.ID == LinkID);
            if (link == null)
            {
                // search for link in sublinks
                foreach (var sublist in SubLinks)
                {
                    link = sublist.Value.FirstOrDefault(l => l.ID == LinkID);
                    if (link != null)
                        break;
                }
            }
            if (link == null)
                return string.Empty;
            var str = new StringBuilder();
            str.AppendLinuxLine($"# LLM Web Browser");
            str.AppendLinuxLine($"## {link.Title}");
            str.AppendLinuxLine($"{link.Body}").AppendLinuxLine();

            if (link.Category == PageType.ListingPage)
            {
                CurrentListing = await scraper.ParseWebListing(link.URL, this, link.InnerScan);
                str.AppendLinuxLine("## Available Links");
                var x = 1;
                foreach (var entry in CurrentListing.Entries)
                {
                    str.AppendLinuxLine($"{x}. {entry.Title}");
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
                        str.AppendLinuxLine("(Tags: " + tags.ToString() + ")");
                    }
                    if (!string.IsNullOrEmpty(entry.Article))
                        str.AppendLinuxLine("Summary: " + entry.Article.Replace("\n\n", " ").Replace("\n", " ").Trim());
                }
                str.AppendLinuxLine();
            }
            else if(link.Category == PageType.ArticlePage)
            {
                var content = await scraper.GetPageContent(link.URL, this);
                str.AppendLinuxLine(content);
            }
            else if (link.Category == PageType.MetaPage)
            {
                str.AppendLinuxLine("## Available Links");
                var x = 1;
                foreach (var item in SubLinks[LinkID])
                {
                    str.AppendLinuxLine($"{x}. {item.Title}");
                    x++;
                }
            }

            if (!string.IsNullOrEmpty(Goal))
            {
                str.AppendLinuxLine();
                str.AppendLinuxLine("# Goal:");
                str.AppendLinuxLine(Goal);
            }
            str.AppendLinuxLine();
            str.AppendLinuxLine("# Rules:");
            switch (link.Category)
            {
                case PageType.FrontPage:
                case PageType.MetaPage:
                    str.AppendLinuxLine("- Retrieve information from this website to complete your goal.");
                    str.AppendLinuxLine("- Type the number corresponding to the link you want to visit.");
                    if (AllowComplexNavigation)
                        str.AppendLinuxLine("- Enter 0 to return to the main page and pick another link.");
                    str.AppendLinuxLine("- Only write the number and nothing else.");
                    break;
                case PageType.ListingPage:
                    str.AppendLinuxLine("- If one of the links above corresponds to the request, answer with the corresponding number only, nothing else.");
                    if (AllowComplexNavigation)
                        str.AppendLinuxLine("- Enter 0 to return to the main page and pick another link.");
                    else
                        str.AppendLinuxLine("- If no task in the list above correspond to what the user requested, pick something close.");
                    str.AppendLinuxLine("- Pick one single option. Do not add any commentary or names.");
                    break;
                case PageType.ArticlePage:
                    str.AppendLinuxLine("- You have selected this page.");
                    str.AppendLinuxLine("- If you want to send it to {{user}}, type 1. Any other number will send you back to front page.");
                    str.AppendLinuxLine("- Only write the number and nothing else.");
                    break;
                case PageType.SearchPage:
                    str.AppendLinuxLine("- Type the search terms you're looking for to complete your request. Only type those search terms and nothing else.");
                    str.AppendLinuxLine("- Use few but descriptive words about the topic.");
                    break;
                default:
                    break;
            }
            return str.ToString();
        }
    }


    public class WebScraper
    {
        public string Address { get; set; } = "";
        
        private readonly IConfiguration config = Configuration.Default.WithDefaultLoader();
        private readonly IBrowsingContext context;

        private Dictionary<string, IDocument> Cache = [];


        public WebScraper() 
        {
            context = BrowsingContext.New(config);
        }

        public void ClearCache()
        {
            Cache.Clear();
        }

        public async Task<IHtmlCollection<IElement>?> FindCells(string address, string cellselector)
        {
            IDocument? document;
            if (Cache.TryGetValue(address, out var cache))
            {
                document = cache;
            }
            else
            {
                document = await context.OpenAsync(address);
                if (document != null)
                    Cache[address] = document;
            }
            if (document == null)
                return default;
            var cells = document.QuerySelectorAll(cellselector);
            return cells;
        }

        private static DateTime StringToDate(string textdate, string format)
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
            IDocument? document;
            if (Cache.TryGetValue(page, out var cache))
            {
                document = cache;
            }
            else
            {
                document = await context.OpenAsync(page);
                if (document != null)
                    Cache[page] = document;
            }
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
                entry.Date = !string.IsNullOrEmpty(dateinfo) ? StringToDate(dateinfo, web.ListingDateFormat) : DateTime.Now;
                if (innerscan)
                {
                    var content = await GetPageContent(entry.Link, web);
                    entry.Article = content;
                }
                entries.Add(entry);
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
            IDocument? document;
            if (Cache.TryGetValue(page, out var cache))
            {
                document = cache;
            }
            else
            {
                document = await context.OpenAsync(page);
                if (document != null)
                    Cache[page] = document;
            }
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
