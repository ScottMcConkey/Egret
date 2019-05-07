using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Utilities
{
    /// <summary>
    /// Factory object for Select Lists generated for simple drop downs.
    /// </summary>
    public static class DropDownFactory
    {
        public static SelectList ResultsPerPage()
        {
            List<SelectListItem> pageOptions = new List<SelectListItem>();
            pageOptions.Add(new SelectListItem() { Text = "10", Value = "10" });
            pageOptions.Add(new SelectListItem() { Text = "25", Value = "25" });
            pageOptions.Add(new SelectListItem() { Text = "50", Value = "50" });
            pageOptions.Add(new SelectListItem() { Text = "100", Value = "100" });

            return new SelectList(pageOptions, "Value", "Text");
        }
    }
}
