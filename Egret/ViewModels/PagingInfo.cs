using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 1;
        public int CurrentPage { get; set; } = 1;

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
