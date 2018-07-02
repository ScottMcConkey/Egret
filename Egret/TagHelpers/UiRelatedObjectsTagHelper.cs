using System;
using System.Collections;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Egret.TagHelpers
{
    [HtmlTargetElement("ui-related-objects", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class UiRelatedObjectsTagHelper : TagHelper
    {
        [HtmlAttributeName("list-object")]
        public ICollection ObjectList { get; set; }

        [HtmlAttributeName("box-title")]
        public string Title { get; set; }

        [HtmlAttributeName("route-id")]
        public string RouteId { get; set; }

        [HtmlAttributeName("route-controller")]
        public string RouteController { get; set; }

        [HtmlAttributeName("route-action")]
        public string RouteAction { get; set; }

        [HtmlAttributeName("help-text")]
        public string HelpText { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = "";

            if (ObjectList.Count == 0)
            {
                html = "<div class='related-object-blank'>There are currently no { Title }.</div>";
            }
            else
            {
                foreach (var item in ObjectList)
                {
                    html += $@"<a class='related-object-link' target='_blank' href='/{RouteController}/{RouteAction}/{RouteId}'>
                                    <div class='related-object-div'>
                                        <span class='glyphicon glyphicon-chevron-right'></span>
                                    </div>
                                </a>";
                }
            }
            
            output.TagName = null;

            output.Content.SetHtmlContent(
                $@"<div class='related-objects'>
                        <div class='related-objects-header'>{Title}
                            <a asp-route-inventoryid='{RouteId}' asp-controller='{RouteController}' asp-action='Create'>
                                <span title='{HelpText}' style='float: right; top: -1px;' class='glyphicon glyphicon-plus' />
                            </a>
                        </div>
                        {html}");

            //        <a class="related-object-link" target="_blank" asp-controller="Inventory" asp-action="Edit" asp-route-id="@Model.ConsumptionEvent.InventoryItemCode">
            //            <div class="related-object-div">
            //                <span class="glyphicon glyphicon-chevron-right"></span>
            //                Test
            //            </div>
            //        </a>
        }
    }

}
