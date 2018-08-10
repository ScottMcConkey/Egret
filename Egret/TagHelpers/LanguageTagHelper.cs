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
    
    [HtmlTargetElement("label", Attributes = "include-languages")]
    public class LanguageTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression Source { get; set; }

        /// <summary>
        /// Use on Labels to indicate which languages should also appear beneath the main label.
        /// These languages must be specified on the Model property using the LanguageAttribute.
        /// Separate multiple values with commas and no spaces.
        /// </summary>
        [HtmlAttributeName("include-languages")]
        public string Languages { get; set; }

        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<string> LanguageList = Languages.Split(',').ToList();

            string PropertyName = Source.Name.Substring(Source.Name.LastIndexOf('.') + 1) ?? Source.Name;
            PropertyInfo info = Source.Metadata.ContainerType.GetProperty(PropertyName);

            foreach (string language in LanguageList)
            {
                if (info.GetCustomAttributes(typeof(LanguageAttribute)).Any())
                {
                    foreach (LanguageAttribute attribute in info.GetCustomAttributes(typeof(LanguageAttribute)))
                    {
                        if (language == (attribute.Name))
                        {
                            output.PostElement.AppendHtml("<br>");
                            output.PostElement.AppendHtml($"<label class='control-label' style='font-size: calc(100% - 2px); color: #999'>" +
                                $"{(attribute).Value}</label>"
                            );
                        }
                    }
                }
            }

        }
    }
}
