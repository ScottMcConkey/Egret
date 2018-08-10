using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Attributes
{
    public class LanguageAttribute : Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
