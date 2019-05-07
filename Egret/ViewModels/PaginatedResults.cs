using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.ViewModels
{
    public abstract class PaginatedResults
    {
        public int Count { get; set; }

        public int CurrentPage { get; set; }

        public int ResultsPerPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)Count / ResultsPerPage);

    }
}
