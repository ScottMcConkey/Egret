﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;

namespace Egret.ViewModels
{
    public class InventoryItemViewModel
    {
        public List<FabricTest> FabricTests { get; set; }
        public List<ConsumptionEvent> ConsumptionEvents { get; set; }
        public InventoryItem Item { get; set; }
    }
}
