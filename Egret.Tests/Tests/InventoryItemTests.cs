using Egret.Controllers;
using Egret.DataAccess;
using Egret.Models;
using Egret.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryItemTests
    {
        private readonly string _unknown = ItemStockLevel.Unknown.Value;
        private readonly string _inStock = ItemStockLevel.InStock.Value;
        private readonly string _outOfStock = ItemStockLevel.OutOfStock.Value;
        private readonly string _error = ItemStockLevel.Error.Value;



        [TestCase(null, 0, 0, null, "Unknown")]
        [TestCase(null, 1, 100, null, "Unknown")]
        [TestCase(null, 1, -100, null, "Unknown")]
        [TestCase(100, 0, 0, 100, "In Stock")]
        [TestCase(100, 1, 100, 0, "Out of Stock")]
        [TestCase(100, 2, 50, 0, "Out of Stock")]
        [TestCase(100, 1, 40, 60, "In Stock")]
        [TestCase(100, 1, -40, 140, "In Stock")]
        [TestCase(100, 1, 150, -50, "Error")]
        [TestCase(-100, 0, 0, -100, "Error")]
        [TestCase(-100, 1, 40, -100, "Error")]
        [TestCase(-100, 1, -50, -100, "Error")]
        public void StockQuantity_Evaluates(decimal? qtyPurchased, int consumptionCount, int consumptionValue, 
            int? expectedStockQty, string expectedStockLevel)
        {
            //+ Arrange
            var item = new InventoryItem();
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>();

            //if (qtyPurchased != null)
            //{
                item.QtyPurchased = qtyPurchased;
            //}

            for (var i = 0; i < consumptionCount; i++)
            {
                var consumptionEvent = new ConsumptionEvent() { QuantityConsumed = consumptionValue };
                item.ConsumptionEventsNavigation.Add(consumptionEvent);
            }

            //+ Assert
            Assert.AreEqual(expectedStockQty, item.StockQuantity);
            Assert.AreEqual(expectedStockLevel, item.StockLevel);

        }


        #region Logistics

        [TestCase("9/15/2017 10:50 am", "9/18/2017 3:30 pm", 3)]
        [TestCase("9/15/2017 10:50 am", "9/15/2017 3:30 pm", 0)]
        [TestCase("9/15/2017 10:50 am", null, null)]
        [TestCase(null, "9/15/2017 3:30 pm", null)]
        [TestCase(null, null, null)]
        public void DaysToConfirm_Evaluates(string added, string confirmed, int? expectedDays)
        {
            //+ Arrange
            var item = new InventoryItem();

            if (!string.IsNullOrWhiteSpace(added))
            {
                item.DateAdded = Convert.ToDateTime(added);
            }

            if (!string.IsNullOrWhiteSpace(confirmed))
            {
                item.DateConfirmed = Convert.ToDateTime(confirmed);
            }

            //+ Assert
            Assert.AreEqual(expectedDays, item.DaysToConfirm);
        }

        [TestCase(null, null, null)]
        [TestCase("9/15/2017", null, null)]
        [TestCase(null, "9/30/2017", null)]
        [TestCase("9/15/2017", "9/30/2017", 15)]
        [TestCase("9/30/2017", "9/15/2017", null)]
        public void DaysToShip_Evaluates(string dateShipped, string dateArrived, int? expectedValue)
        {
            var item = new InventoryItem();

            if (!string.IsNullOrWhiteSpace(dateShipped))
            {
                item.DateShipped = Convert.ToDateTime(dateShipped);
            }

            if (!string.IsNullOrWhiteSpace(dateArrived))
            {
                item.DateArrived = Convert.ToDateTime(dateArrived);
            }

            Assert.AreEqual(expectedValue, item.DaysToShip);
        }
        
        [TestCase(null, null, null)]
        [TestCase(null, "9/15/2017", null)]
        [TestCase("9/15/2017", null, null)]
        [TestCase("9/30/2017", "9/15/2017", null)]
        [TestCase("9/10/2017", "9/15/2017", 5)]
        [TestCase("9/10/2020", "10/1/2020", 21)]
        public void DaysToComplete_Evaluates(string dateAdded, string dateArrived, int? expectedValue)
        {
            var item = new InventoryItem();

            if (!string.IsNullOrWhiteSpace(dateAdded))
            {
                item.DateAdded = Convert.ToDateTime(dateAdded);
            }

            if (!string.IsNullOrWhiteSpace(dateArrived))
            {
                item.DateArrived = Convert.ToDateTime(dateArrived);
            }

            Assert.AreEqual(expectedValue, item.DaysToComplete);
        }

        #endregion

        #region Accounting

        [TestCase(null, null, null, null)]
        [TestCase(25, null, null, 25)]
        [TestCase(25.5, null, null, 25.5)]
        [TestCase(0, 49, null, 49)]
        [TestCase(null, 35, null, 35)]
        [TestCase(0, null, null, 0)]
        [TestCase(400, 50, 12.5, 462.5)]
        // Test nulls?
        public void TotalCost_Evaluates(decimal? fobCost, decimal? shippingCost, decimal? importCosts, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                FOBCost = fobCost,
                ShippingCost = shippingCost,
                ImportCosts = importCosts
            };

            Assert.AreEqual(expectedValue, item.TotalCost);
        }

        [TestCase(200, 20, 10)]
        [TestCase(null, null, null)]
        [TestCase(null, 20, null)]
        [TestCase(200, null, null)]
        [TestCase(200, 0, null)]
        [TestCase(0, 20, null)]
        [TestCase(200.2, 1, 200.2)]
        public void CostPerUnit_Evaluates(decimal? fobCost, decimal? qtyPurchased, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                QtyPurchased = qtyPurchased,
                FOBCost = fobCost
            };

            Assert.AreEqual(expectedValue, item.CostPerUnit);
        }

        // This is redundant in order to test the calculated property TotalCost
        // and is not ideal, but I have not found a better solution at this time
        [TestCase(null, null, null, null, null)] // Total Cost Null
        [TestCase(null, null, null, 50, null)] // Total Cost Null
        [TestCase(50, null, null, null, null)] // Total Cost Null
        [TestCase(50, 20, 5, 5, 15)] // Total Cost Positive (60)
        [TestCase(50, 20, 5, null, null)] // Total Cost Positive (60)
        [TestCase(50.25, 20.7, 5.28, 80, 0.9529)] // Calculate Rounding!
        public void TotalCostPerUnit_Evaluates(decimal? fobCost, decimal? shippingCost, decimal? importCosts,
            decimal? qtyPurchased, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                FOBCost = fobCost,
                ShippingCost = shippingCost,
                ImportCosts = importCosts,
                QtyPurchased = qtyPurchased
            };

            Assert.AreEqual(expectedValue, item.TotalCostPerUnit);
        }

        #endregion

    }
}
