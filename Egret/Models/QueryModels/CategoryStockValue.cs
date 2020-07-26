using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using Egret.Attributes;
using Egret.Widgets;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Egret.Models.QueryModels
{ 
    public class CategoryStockValue
    {
        public string Name { get; set; }
        public string StockValue { get; set; }
    }
}
