using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Interfaces
{
    public interface IHasStockCount
    {
        decimal? StockQuantity { get; }

        string StockLevel { get; }
    }
}
