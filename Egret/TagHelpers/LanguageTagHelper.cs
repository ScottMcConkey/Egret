using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Egret.Attributes;

namespace Egret.TagHelpers
{
    [HtmlTargetElement("label", Attributes = "include-language")]
    public class LanguageTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression Source { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.TagMode = TagMode.StartTagAndEndTag;

            //(Source.Metadata.ContainerType.FullName.GetType().GetCustomAttribute(typeof(LanguageAttribute)).ToString())
            //LanguageAttribute test = (LanguageAttribute)Source.Metadata.ContainerType.FullName.GetType().GetCustomAttribute(typeof(LanguageAttribute));
            //string value = test?.Value.ToString();

            //var contents = $@"{value}";
            string PropertyName = Source.Name.Substring(Source.Name.LastIndexOf('.') + 1);

            //Assembly asm = typeof(Egret.DataAccess.EgretContext).Assembly;

            //Type type = asm.GetType(PropertyName);

            PropertyInfo info = Source.Metadata.ContainerType.GetProperty(PropertyName);

            output.Content.SetContent(((LanguageAttribute)info.GetCustomAttribute(typeof(LanguageAttribute))).Value);// Source.Metadata.ContainerType.GetProperty(Source.Name).ToString());

            //(Source.Metadata.ContainerType.GetCustomAttribute(type).ToString()
        }
    }
}
