using System;
using System.Text;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Infrastructure
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tagLi = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                    tagLi.AddCssClass("active");
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                tagLi.InnerHtml = tag.ToString();
                result.Append(tagLi);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}