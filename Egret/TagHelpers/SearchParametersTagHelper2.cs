using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Egret.Interfaces;
using Egret.ViewModels;

namespace Egret.TagHelpers
{
    /// <summary>
    /// Transform object properties into asp-route-* Tag Helpers
    /// </summary>
    [HtmlTargetElement("input", Attributes = QuerifyObject)]
    public class SearchParametersTagHelper2 : TagHelper
    {
        private const string QuerifyObject = "querify-object";
        private const string RouteValuesPrefix = "asp-route-";

        [HtmlAttributeName(QuerifyObject)]
        public int SearchParameters { get; set; } = 0;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //foreach (PropertyInfo info in SearchParameters.GetType().GetProperties())
            //{
                //output.Attributes.Add(RouteValuesPrefix + info.Name, SearchParameters.GetType().GetProperty(info.ToString()).GetValue(SearchParameters, null));
                output.Attributes.Add("asp-route-size", SearchParameters/*SearchParameters.GetType().GetProperties().Count()*/);
            //}
        }
    }
}
