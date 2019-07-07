using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class CategoryReport
    {
        public string CategoryName { get; set; }
        public decimal CurrentStockValue { get; set; }

        public decimal AvailableLotCount { get; set; }

        public string[] Errors { get; set; }

        public int GetErrorCount
        {
            get
            {
                return Errors.Count();
            }
        }
    }
}
