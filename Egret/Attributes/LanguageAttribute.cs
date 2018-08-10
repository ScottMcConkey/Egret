using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LanguageAttribute : Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
