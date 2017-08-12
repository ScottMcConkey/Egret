using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class ProjectItem
    {
        public int Projectid { get; set; }
        public int Itemid { get; set; }

        public virtual Projects Project { get; set; }
    }
}
