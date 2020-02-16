using System;
using NUnit.Framework;
using Egret.Models;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryItemTests
    {
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
        public void StockQuantity (decimal? qtyPurchased, int consumptionCount, int consumptionValue, 
            int? expectedStockQty, string expectedStockLevel)
        {
            //+ Arrange
            var item = new InventoryItem();
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>();

            if (qtyPurchased != null)
            {
                item.QtyPurchased = qtyPurchased;
            }

            for (var i = 0; i < consumptionCount; i++)
            {
                var consumptionEvent = new ConsumptionEvent() { QuantityConsumed = consumptionValue };
                item.ConsumptionEventsNavigation.Add(consumptionEvent);
            }

            //+ Assert
            Assert.AreEqual(expectedStockQty, item.StockQuantity);
            Assert.AreEqual(expectedStockLevel, item.StockLevel);

        }


        [TestCase("9/15/2017 10:50 am", "9/18/2017 3:30 pm", 3)]
        [TestCase("9/15/2017 10:50 am", "9/15/2017 3:30 pm", 0)]
        [TestCase("9/15/2017 10:50 am", null, null)]
        [TestCase(null, "9/15/2017 3:30 pm", null)]
        [TestCase(null, null, null)]
        public void DaysToConfirm(string added, string confirmed, int? expectedDays)
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
        
    }
}
