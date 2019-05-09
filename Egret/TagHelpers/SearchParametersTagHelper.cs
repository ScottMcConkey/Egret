using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.TagHelpers
{
    [HtmlTargetElement("a", Attributes = RouteAllAttributeName)]
    public class SearchParametersTagHelper : AnchorTagHelper
    {
        private const string RouteAllAttributeName = "asp-routeall";

        public SearchParametersTagHelper(IHtmlGenerator generator) : base(generator)
        {

        }


        /// <summary>
        /// The name of the Object who properties will be converted to query strings.
        /// </summary>
        [HtmlAttributeName(RouteAllAttributeName)]
        public string RouteAll { get; set; }

        private IDictionary<string, string> _routeValues;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);

            RouteValueDictionary routeValues = null;
            if (_routeValues != null && _routeValues.Count > 0)
            {
                // foreach property in object: 
                    routeValues = new RouteValueDictionary(_routeValues);
            }

            var routeLink = Route != null;
            var pageLink = Page != null || PageHandler != null;

            TagBuilder tagBuilder;
            if (pageLink)
            {
                tagBuilder = Generator.GeneratePageLink(
                    ViewContext,
                    linkText: string.Empty,
                    pageName: Page,
                    pageHandler: PageHandler,
                    protocol: Protocol,
                    hostname: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: null);
            }
            else if (routeLink)
            {
                tagBuilder = Generator.GenerateRouteLink(
                    ViewContext,
                    linkText: string.Empty,
                    routeName: Route,
                    protocol: Protocol,
                    hostName: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: null);
            }
            else
            {
                tagBuilder = Generator.GenerateActionLink(
                   ViewContext,
                   linkText: string.Empty,
                   actionName: Action,
                   controllerName: Controller,
                   protocol: Protocol,
                   hostname: Host,
                   fragment: Fragment,
                   routeValues: routeValues,
                   htmlAttributes: null);
            }

            output.MergeAttributes(tagBuilder);
        }

    }
}
