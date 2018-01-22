using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;

namespace Egret.ViewModels
{
    public class InventoryListViewModel
    {
        public InventoryListViewModel()
        {

        }

        public IQueryable<List<InventoryItem>> results;
    }
}
