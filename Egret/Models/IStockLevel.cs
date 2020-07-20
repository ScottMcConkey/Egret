using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public interface IStockLevel
    {
        decimal? StockQuantity { get; }

        string StockLevel { get; }
    }
}
