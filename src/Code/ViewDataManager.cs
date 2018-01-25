using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Egret.Code
{
    /// <summary>
    /// Sets ViewData for View titles.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ViewDataManagedAttribute : Attribute
    {
        public string TabHeader { get; }
        public string PageHeader { get; }

        public ViewDataManagedAttribute(string tab, string page)
        {
            TabHeader = tab;
            PageHeader = page;
        }
        //private 

        //public Dictionary<string, string> Names(IActionResult e, string tabname, string pageheader)
        //{
        //    Dictionary<string, string> assignmentMatrix = new Dictionary<string, string>();
        //
        //    e.ViewData["Title"] = this.ControllerContext.RouteData.Values["action"].ToString();
        //
        //    return assignmentMatrix;
        //}
    }
}
