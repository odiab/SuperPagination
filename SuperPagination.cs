using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SuperPagination
{
    [HtmlTargetElement("pagination",
    Attributes = "total-pages, current-page, link-url, show-previous, show-next")]
    public class PaginationTagHelper : TagHelper
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool ShowPrevious { get; set; }
        public bool ShowNext { get; set; }

        [HtmlAttributeName("link-url")]
        public string Url { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.Attributes.Add("aria-label", "Page navigation");
            output.PreContent.SetHtmlContent(@"<ul class=""pagination"">");

            if (ShowPrevious)
            {
                output.Content.AppendHtml(
                    $@"<li class=""page-item""><a class=""page-link"" href=""{Url}/{CurrentPage - 1}"" aria-label=""Previous""><span aria-hidden=""true"">&laquo;</span></a></li>");
            }

            for (var i = 1; i <= TotalPages; i++)
            {
                output.Content.AppendHtml(CurrentPage == i
                    ? $@"<li class=""page-item active"" aria-current=""page""><span class=""page-link"">{i}<span class=""sr-only"">(current)</span></span></li>"
                    : $@"<li class=""page-item""><a href=""{Url}/{i}"" class=""page-link"">{i}</a></li>");
            }

            if (ShowNext)
            {
                output.Content.AppendHtml(
                $@"<li class=""page-item""><a class=""page-link"" href=""{Url}/{CurrentPage + 1}"" aria-label=""Previous""><span aria-hidden=""true"">&raquo;</span></a></li>");
            }

            output.PostContent.SetHtmlContent("</ul>");
            output.Attributes.Clear();
        }
    }
}