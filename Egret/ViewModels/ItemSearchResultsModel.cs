using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;

namespace Egret.ViewModels
{
    public class ItemSearchResultsModel : PaginatedResults
    {
        public List<InventoryItem> Items { get; set; }        
    }
}
