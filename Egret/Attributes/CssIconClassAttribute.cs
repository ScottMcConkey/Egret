using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Attributes
{
    /// <summary>
    /// Defines the CSS class used to assign an icon for the object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CssIconClassAttribute : Attribute
    {
        public string ClassName { get; set; }

        public CssIconClassAttribute(string className)
        {
            ClassName = className;
        }
    }
}
