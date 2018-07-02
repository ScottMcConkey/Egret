using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Egret.TagHelpers
{
    [HtmlTargetElement("ui-object-info", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class UiObjectInfoTagHelper : TagHelper
    {
        [HtmlAttributeName("addedby")]
        public string AddedBy { get; set; }

        [HtmlAttributeName("addedon")]
        public DateTime? AddedOn { get; set; }

        [HtmlAttributeName("updatedby")]
        public string UpdatedBy { get; set; }

        [HtmlAttributeName("updatedon")]
        public DateTime? UpdatedOn { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.Content.SetHtmlContent($@"<div class='edit-info'>
                                                <table>
                                                    <tr>
                                                        <td>Added By:</td>
                                                        <td>{AddedBy}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Added On:</td>
                                                        <td>{(AddedOn != null ? AddedOn.Value.ToShortDateString() : "")}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Updated By:</td>
                                                        <td>{UpdatedBy}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Updated On:</td>
                                                        <td>{(UpdatedOn != null ?  UpdatedOn.Value.ToShortDateString() : "")}</td>
                                                    </tr>
                                                </table>
                                            </div>");
        }
    }
}
