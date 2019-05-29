using Egret.Attributes;
using System;

namespace Egret.Utilities
{
    public class CustomAttributeManager
    {
        public static string GetCssIconClass(Type item)
        {
            CssIconClassAttribute attr = (CssIconClassAttribute)Attribute.GetCustomAttribute(item, typeof(CssIconClassAttribute));
            var className = attr.ClassName;

            return className;
        }
    }
}
