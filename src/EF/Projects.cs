using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class Projects
    {
        public Projects()
        {
            ConsumptionEvents = new HashSet<ConsumptionEvents>();
            ProjectItems = new HashSet<ProjectItem>();
            Purchases = new HashSet<Purchases>();
        }

        public int Projectid { get; set; }
        public int? Customerid { get; set; }
        public int? Productid { get; set; }
        public int? Productqty { get; set; }
        public DateTime? Startdate { get; set; }

        public virtual ICollection<ConsumptionEvents> ConsumptionEvents { get; set; }
        public virtual ICollection<ProjectItem> ProjectItems { get; set; }
        public virtual ICollection<Purchases> Purchases { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
