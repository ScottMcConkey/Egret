using Egret.Models;
using Egret.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Egret.Tests.Models
{
    [Trait("class", "InventoryItemTests")]
    [Trait("group", "ModelTests")]
    public class InventoryItemTests
    {
        [Theory]
        [ClassData(typeof(StockQuantityData))]
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
        [ClassData(typeof(TotalCostData))]
        [Trait("name", "total_cost_evaluates")]
        // Test nulls?
        public void total_cost_evaluates(decimal? fobcost, decimal? shippingCost, decimal? importCosts, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                FobCost = fobcost,
                ShippingCost = shippingCost,
                ImportCosts = importCosts
            };

            Assert.Equal(expectedValue, item.TotalCost);
        }

        [Theory]
        [ClassData(typeof(CostPerUnitData))]
        [Trait("name", "cost_per_unit_evaluates")]
        public void cost_per_unit_evaluates(decimal? fobcost, decimal? qtyPurchased, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                QtyPurchased = qtyPurchased,
                FobCost = fobcost
            };

            Assert.Equal(expectedValue, item.CostPerUnit);
        }

        // This is redundant in order to test the calculated property TotalCost
        // and is not ideal, but I have not found a better solution at this time
        [Theory]
        [ClassData(typeof(TotalCostPerUnitData))]
        [Trait("name", "total_cost_per_unit_evaluates")]
        public void total_cost_per_unit_evaluates(decimal? fobcost, decimal? shippingCost, decimal? importCosts,
            decimal? qtyPurchased, decimal? expectedValue)
        {
            var item = new InventoryItem()
            {
                FobCost = fobcost,
                ShippingCost = shippingCost,
                ImportCosts = importCosts,
                QtyPurchased = qtyPurchased
            };

            Assert.Equal(expectedValue, item.TotalCostPerUnit);
        }

        #endregion



        private class CostPerUnitData : TheoryData<decimal?, decimal?, decimal?>
        {
            public CostPerUnitData()
            {
                this.Add(200, 20, 10);
                this.Add(null, null, null);
                this.Add(null, 20, null);
                this.Add(200, null, null);
                this.Add(200, 0, null);
                this.Add(0, 20, null);
                this.Add(Convert.ToDecimal(200.2), 1, Convert.ToDecimal(200.2));
            }
        }

        private class StockQuantityData : TheoryData<decimal?, int, int, int?, string>
        {
            public StockQuantityData()
            {
                this.Add(null, 0, 0, null, "Unknown");
                this.Add(null, 1, 100, null, "Unknown");
                this.Add(null, 1, -100, null, "Unknown");
                this.Add(100, 0, 0, 100, "In Stock");
                this.Add(100, 1, 100, 0, "Out of Stock");
                this.Add(100, 2, 50, 0, "Out of Stock");
                this.Add(100, 1, 40, 60, "In Stock");
                this.Add(100, 1, -40, 140, "In Stock");
                this.Add(100, 1, 150, -50, "Error");
                this.Add(-100, 0, 0, -100, "Error");
                this.Add(-100, 1, 40, -100, "Error");
                this.Add(-100, 1, -50, -100, "Error");
            }
        }

        private class TotalCostData : TheoryData<decimal?,decimal?,decimal?,decimal?>
        {
            public TotalCostData()
            {
                this.Add(null, null, null, null);
                this.Add(25, null, null, 25);
                this.Add(Convert.ToDecimal(25.5), null, null, Convert.ToDecimal(25.5));
                this.Add(0, 49, null, 49);
                this.Add(null, 35, null, 35);
                this.Add(0, null, null, 0);
                this.Add(400, 50, Convert.ToDecimal(12.5), Convert.ToDecimal(462.5));
            }
        }

        private class TotalCostPerUnitData : TheoryData<decimal?, decimal?, decimal?, decimal?, decimal?>
        {
            public TotalCostPerUnitData()
            {
                this.Add(null, null, null, null, null);
                this.Add(null, null, null, 50, null);
                this.Add(50, null, null, null, null);
                this.Add(50, 20, 5, 5, 15);
                this.Add(50, 20, 5, null, null);
                this.Add(Convert.ToDecimal(50.25), Convert.ToDecimal(20.7),
                    Convert.ToDecimal(5.28), 80, Convert.ToDecimal(0.9529));
            }
        }

    }
}
