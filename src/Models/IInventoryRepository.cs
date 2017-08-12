using System.Collections.Generic;
using Egret_Dev.EF;

namespace Egret_Dev.Models
{
    public interface IInventoryRepository
    {
        IEnumerable<EF.InventoryItem> Items { get; }
    }
}
