﻿using System;
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
        public string Role { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var user = _actionAccessor.ActionContext.HttpContext.User;

            if (!user.IsInRole(Role))
            {
                output.SuppressOutput();
            }
        }
    }
}
