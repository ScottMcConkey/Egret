﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Egret.ViewModels
{
    public class InventorySearchViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [UIHint("date")]
        public DateTime? DateCreatedStart { get; set; }

        [UIHint("date")]
        public DateTime? DateCreatedEnd { get; set; }
    }
}
