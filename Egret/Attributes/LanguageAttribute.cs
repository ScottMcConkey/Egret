using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Attributes
{
    /// <summary>
    /// Allows for the specifying of an alternative language for the Model property name.
    /// This value may be accessed in razor files using the 'include-languages' attribute, 
    /// which implements the LanguageTagHelper for labels.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LanguageAttribute : Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
