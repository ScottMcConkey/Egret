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
    
    [HtmlTargetElement("label", Attributes = "include-languages")]
    public class LanguageTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression Source { get; set; }

        [HtmlAttributeName("egret-class")]
        public string ClassName { get; set; }

        [HtmlAttributeName("egret-property")]
        public string PropertyName { get; set; }

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

            PropertyInfo info;

            if (Source != null)
            {
                string PropertyName = Source.Name.Substring(Source.Name.LastIndexOf('.') + 1) ?? Source.Name;
                info = Source.Metadata.ContainerType.GetProperty(PropertyName);
            }
            else
            {
                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                Type type = Type.GetType($"{assemblyName}.Models.{ClassName}, {assemblyName}");
                info = type.GetProperty(PropertyName);
            }

            foreach (string language in LanguageList)
            {
                if (info.GetCustomAttributes(typeof(LanguageAttribute)).Any())
                {
                    foreach (LanguageAttribute attribute in info.GetCustomAttributes(typeof(LanguageAttribute)))
                    {
                        if (language == (attribute.Name))
                        {
                            TagBuilder label = new TagBuilder("label");
                            label.AddCssClass("language-label");
                            label.AddCssClass("control-label");
                            
                            label.InnerHtml.Append(attribute.Value);
                            output.PostElement.AppendHtml("<br>");
                            output.PostElement.AppendHtml(label);
                        }
                    }
                }
            }

        }
    }
}
