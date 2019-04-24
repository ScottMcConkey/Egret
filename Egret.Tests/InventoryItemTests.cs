using System;
using NUnit.Framework;
using Egret.Models;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryItemTests
    {
        [Test]
        public void QtyPurchasedNull_ConsumptionEventsNone_Sets_StockQuantityNull_StockLevelNull()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = null;

            //+ Assert
            Assert.AreEqual(item.StockQuantity, null);
            Assert.AreEqual(item.StockLevel, "Unknown");
        }

        [Test]
        public void QtyPurchasedNull_ConsumptionEventsPositive_Sets_StockQuantityNull_StockLevelUnknown()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = null;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = 100 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, null);
            Assert.AreEqual(item.StockLevel, "Unknown");
        }

        [Test]
        public void QtyPurchasedNull_ConsumptionEventsNegative_Sets_StockQuantityNull_StockLevelUnknown()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = null;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = -100 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, null);
            Assert.AreEqual(item.StockLevel, "Unknown");
        }



        [Test]
        public void QtyPurchasedPositive_ConsumptionEventsNone_Sets_StockQuantityPositive_StockLevelInStock()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = 100;

            //+ Assert
            Assert.AreEqual(item.StockQuantity, 100);
            Assert.AreEqual(item.StockLevel, "In Stock");
        }

        [Test]
        public void QtyPurchasedPositive_ConsumptionEventsFill_Sets_StockQuantity0_StockLevelOutOfStock()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = 100;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = 100 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, 0);
            Assert.AreEqual(item.StockLevel, "Out of Stock");
        }

        [Test]
        public void QtyPurchasedPositive_ConsumptionEventsPositive_Sets_StockQuantityPositive_StockLevelInStock()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = 100;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = 40 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, 60);
            Assert.AreEqual(item.StockLevel, "In Stock");
        }

        [Test]
        public void QtyPurchasedPositive_ConsumptionEventsNegative_Sets_StockQuantityPositive_StockLevelInStock()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = 100;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = 40 }, new ConsumptionEvent() { QuantityConsumed = -40 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, 100);
            Assert.AreEqual(item.StockLevel, "In Stock");
        }




        [Test]
        public void QtyPurchasedNegative_ConsumptionEventsNone_Sets_StockQuantityNegative_StockLevelError()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = -100;

            //+ Assert
            Assert.AreEqual(item.StockQuantity, -100);
            Assert.AreEqual(item.StockLevel, "Error");
        }

        [Test]
        public void QtyPurchasedNegative_ConsumptionEventsPositive_Sets_StockQuantityNegative_StockLevelError()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = -100;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = 200 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, -100);
            Assert.AreEqual(item.StockLevel, "Error");
        }

        [Test]
        public void QtyPurchasedNegative_ConsumptionEventsNegative_Sets_StockQuantityNegative_StockLevelError()
        {
            //+ Arrange
            var item = new InventoryItem();
            item.QtyPurchased = -100;
            item.ConsumptionEventsNavigation = new List<ConsumptionEvent>() { new ConsumptionEvent() { QuantityConsumed = -200 } };

            //+ Assert
            Assert.AreEqual(item.StockQuantity, -100);
            Assert.AreEqual(item.StockLevel, "Error");
        }
    }
}
