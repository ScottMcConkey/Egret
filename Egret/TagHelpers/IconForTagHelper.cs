using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Utilities;

namespace Egret.TagHelpers
{
    /// <summary>
    /// Sets the Icon css class using the CssIconClassAttribute on the Model.
    /// </summary>
    [HtmlTargetElement(Attributes = "icon-for")]
    public class IconForTagHelper : TagHelper
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
