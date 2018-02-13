using System;
using Xunit;
using Egret.Models;

namespace Egret.Tests
{
    public class InventoryTests
    {

        [Fact]
        public void ChangeItemName()
        {
            // Arrange
            var i = new InventoryItem { Description = "Test Item", Comment = "This is a test" };

            // Act
            i.Description = "Modified Test";

            // Assert
            Assert.Equal("Modified Test", i.Description);
        }
    }
}
