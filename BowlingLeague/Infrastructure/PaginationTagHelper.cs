using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        public PaginationTagHelper(IUrlHelperFactory uhf) //Uses the IUrlHelperFactory whenever a new instance of the class is created
        {
            urlInfo = uhf;
        }

        //This stores the info the tag gets from the Index page
        public PageNumInfo pageInfo { get; set; }

        //Creating a new dictonary to hold the pages that will be made into the page number a tags
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext viewContext { get; set; }

        //Bootstrap
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        //Override the original Process tag helper function
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder finishedTag = new TagBuilder("div"); //build a div tag to hold the page numbers

            for (int i = 1; i <= pageInfo.NumPages; i++)
            {
                IUrlHelper urlHelp = urlInfo.GetUrlHelper(viewContext);

                TagBuilder individualTag = new TagBuilder("a"); //build an new a tag each time for each page number

                KeyValuePairs["pageNum"] = i ;

                //Set up info for each a tag
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);

                //Highlighting the page numbers
                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == pageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                individualTag.InnerHtml.Append(i.ToString());

                //Put the a tag to the div
                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
