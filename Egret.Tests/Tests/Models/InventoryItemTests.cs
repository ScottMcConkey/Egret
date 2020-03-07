using Egret.Models;
using Egret.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Egret.Tests.Models
{
    [Trait("class", "InventoryItemTests")]
    [Trait("group", "ModelTests")]
    public class InventoryItemTests
    {
        private readonly string _unknown = ItemStockLevel.Unknown.Value;
        private readonly string _inStock = ItemStockLevel.InStock.Value;
        private readonly string _outOfStock = ItemStockLevel.OutOfStock.Value;
        private readonly string _error = ItemStockLevel.Error.Value;


        [Theory]
        [InlineData(null, 0, 0, null, "Unknown")]
        [InlineData(null, 1, 100, null, "Unknown")]
        [InlineData(null, 1, -100, null, "Unknown")]
        [InlineData(100, 0, 0, 100, "In Stock")]
        [InlineData(100, 1, 100, 0, "Out of Stock")]
        [InlineData(100, 2, 50, 0, "Out of Stock")]
        [InlineData(100, 1, 40, 60, "In Stock")]
        [InlineData(100, 1, -40, 140, "In Stock")]
        [InlineData(100, 1, 150, -50, "Error")]
        [InlineData(-100, 0, 0, -100, "Error")]
        [InlineData(-100, 1, 40, -100, "Error")]
        [InlineData(-100, 1, -50, -100, "Error")]
        [Trait("name", "stock_quantity_evaluates")]
        public void stock_quantity_evaluates(decimal? qtyPurchased, int consumptionCount, int consumptionValue, 
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
            Assert.Equal(expectedStockQty, item.StockQuantity);
            Assert.Equal(expectedStockLevel, item.StockLevel);
        }


        #region Logistics

        [Theory]
        [InlineData("9/15/2017 10:50 am", "9/18/2017 3:30 pm", 3)]
        [InlineData("9/15/2017 10:50 am", "9/15/2017 3:30 pm", 0)]
        [InlineData("9/15/2017 10:50 am", null, null)]
        [InlineData(null, "9/15/2017 3:30 pm", null)]
        [InlineData(null, null, null)]
        [Trait("name", "days_to_confirm_evaluates")]
        public void days_to_confirm_evaluates(string added, string confirmed, int? expectedDays)
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
            Assert.Equal(expectedDays, item.DaysToConfirm);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData("9/15/2017", null, null)]
        [InlineData(null, "9/30/2017", null)]
        [InlineData("9/15/2017", "9/30/2017", 15)]
        [InlineData("9/30/2017", "9/15/2017", null)]
        [Trait("name", "days_to_ship_evaluates")]
        public void days_to_ship_evaluates(string dateShipped, string dateArrived, int? expectedValue)
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

            Assert.Equal(expectedValue, item.DaysToShip);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, "9/15/2017", null)]
        [InlineData("9/15/2017", null, null)]
        [InlineData("9/30/2017", "9/15/2017", null)]
        [InlineData("9/10/2017", "9/15/2017", 5)]
        [InlineData("9/10/2020", "10/1/2020", 21)]
        [Trait("name", "days_to_complete_evaluates")]
        public void days_to_complete_evaluates(string dateAdded, string dateArrived, int? expectedValue)
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

            Assert.Equal(expectedValue, item.DaysToComplete);
        }

        #endregion

        #region Accounting

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData(25, null, null, 25)]
        [InlineData(25.5, null, null, 25.5)]
        [InlineData(0, 49, null, 49)]
        [InlineData(null, 35, null, 35)]
        [InlineData(0, null, null, 0)]
        [InlineData(400, 50, 12.5, 462.5)]
        [Trait("name", "total_cost_evaluates")]
        // Test nulls?
        public void total_cost_evaluates(decimal? fobCost, decimal? shippingCost, decimal? importCosts, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                FOBCost = fobCost,
                ShippingCost = shippingCost,
                ImportCosts = importCosts
            };

            Assert.Equal(expectedValue, item.TotalCost);
        }

        [Theory]
        [InlineData(200, 20, 10)]
        [InlineData(null, null, null)]
        [InlineData(null, 20, null)]
        [InlineData(200, null, null)]
        [InlineData(200, 0, null)]
        [InlineData(0, 20, null)]
        [InlineData(200.2, 1, 200.2)]
        [Trait("name", "cost_per_unit_evaluates")]
        public void cost_per_unit_evaluates(decimal? fobCost, decimal? qtyPurchased, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                QtyPurchased = qtyPurchased,
                FOBCost = fobCost
            };

            Assert.Equal(expectedValue, item.CostPerUnit);
        }

        // This is redundant in order to test the calculated property TotalCost
        // and is not ideal, but I have not found a better solution at this time
        [Theory]
        [InlineData(null, null, null, null, null)] // Total Cost Null
        [InlineData(null, null, null, 50, null)] // Total Cost Null
        [InlineData(50, null, null, null, null)] // Total Cost Null
        [InlineData(50, 20, 5, 5, 15)] // Total Cost Positive (60)
        [InlineData(50, 20, 5, null, null)] // Total Cost Positive (60)
        [InlineData(50.25, 20.7, 5.28, 80, 0.9529)] // Calculate Rounding!
        [Trait("name", "total_cost_per_unit_evaluates")]
        public void total_cost_per_unit_evaluates(decimal? fobCost, decimal? shippingCost, decimal? importCosts,
            decimal? qtyPurchased, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                FOBCost = fobCost,
                ShippingCost = shippingCost,
                ImportCosts = importCosts,
                QtyPurchased = qtyPurchased
            };

            Assert.Equal(expectedValue, item.TotalCostPerUnit);
        }

        #endregion

    }
}
