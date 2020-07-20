using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public interface IAuditable
    {
        DateTime? DateAdded { get; set; }

        string AddedBy { get; set; }

        DateTime? DateUpdated { get; set; }

        string UpdatedBy { get; set; }
    }
}
