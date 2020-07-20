using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.TagHelpers
{
    /// <summary>
    /// Sets the delete-name attribute, which is used to generate
    /// a custom confirmation message before an object is deleted.
    /// </summary>
    [HtmlTargetElement(Attributes = "delete-for")]
    public class DeleteTagHelper : TagHelper
    {
        [HtmlAttributeName("delete-for")]
        public string DeleteFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("delete-name", DeleteFor);
        }
    }
}
