using System;
using NUnit.Framework;
using Egret.Models;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryItemTests
    {

        [Test]
        public void ChangeItemName()
        {
            // Arrange
            var i = new InventoryItem { Description = "Test Item", Comments = "This is a test" };

            // Act
            i.Description = "Modified Test";

            // Assert
            Assert.AreEqual("Modified Test", i.Description);
        }

        // Test active dropdowns for create and edit
        // Test default currency type
    }
}
