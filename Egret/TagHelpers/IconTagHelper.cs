using Egret.Utilities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Egret.TagHelpers
{
    /// <summary>
    /// Sets the Icon css class using the CssIconClassAttribute on the Model.
    /// </summary>
    [HtmlTargetElement(Attributes = "icon-for")]
    public class IconTagHelper : TagHelper
    {
        [HtmlAttributeName("icon-for")]
        public string IconFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var test = Type.GetType("Egret.Models." + IconFor);
            if (test != null)
            {
                var className = CustomAttributeManager.GetCssIconClass(test);
                output.Attributes.Add("class", className);
            }
            else
            {

            }
        }
    }
}
