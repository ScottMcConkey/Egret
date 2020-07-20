using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Egret.Attributes;
using Egret.Models;

namespace Egret.TagHelpers
{
    [HtmlTargetElement(Attributes = "authorize-for")]
    public class AuthorizeTagHelper : TagHelper
    {
        private readonly IActionContextAccessor _actionAccessor;

        public AuthorizeTagHelper(IActionContextAccessor actionAccessor)
        {
            _actionAccessor = actionAccessor;
        }

        /// <summary>
        /// Use on elements to control which Roles are allowed to see that element.
        /// </summary>
        [HtmlAttributeName("authorize-for")]
        public string AccessGroups { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<string> AuthorizedAccessGroups = AccessGroups.Split(',').ToList();

            var user = _actionAccessor.ActionContext.HttpContext.User;

            foreach (var ag in AuthorizedAccessGroups)
            {
                if (!user.IsInRole(ag))
                {
                    output.Content.SetHtmlContent("");
                }
            }

        }
    }
}
