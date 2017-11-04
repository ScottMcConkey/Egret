using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class Customers
    {
        public Customers()
        {
            Projects = new HashSet<Projects>();
        }

        public int Customerid { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}
