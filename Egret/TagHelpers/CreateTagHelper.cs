using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Egret.Attributes;

namespace Egret.TagHelpers
{
    [HtmlTargetElement("create-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CreateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //output.Content.
            output.TagName = "a";
            output.Content.SetContent("Test");
            //output.Content.SetContent();
        }
    }
}
