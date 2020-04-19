using Egret.Attributes;
using System;

namespace Egret.Utilities
{
    public class CustomAttributeManager
    {
        /// <summary>
        /// Get the CSS class name assigned to a C# class that utilizes the CssIconClass custom attribute
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetCssIconClass(Type item)
        {
            CssIconClassAttribute attr = (CssIconClassAttribute)Attribute.GetCustomAttribute(item, typeof(CssIconClassAttribute));
            var className = attr.ClassName;

            return className;
        }
    }
}
