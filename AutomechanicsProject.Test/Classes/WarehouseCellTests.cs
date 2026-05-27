using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class WarehouseCellTests
    {
        [Fact]
        public void CanCreateWarehouseCell()
        {
            var cell = new WarehouseCell();
            Assert.NotNull(cell);
        }

        [Fact]
        public void CanSetRowAndColumn()
        {
            var cell = new WarehouseCell();
            cell.Row = 5;
            cell.Column = 3;

            Assert.Equal(5, cell.Row);
            Assert.Equal(3, cell.Column);
        }

        [Fact]
        public void ProductId_CanBeNull()
        {
            var cell = new WarehouseCell();
            cell.ProductId = null;

            Assert.Null(cell.ProductId);
        }

        [Fact]
        public void ProductId_CanBeSet()
        {
            var cell = new WarehouseCell();
            var productId = Guid.NewGuid();
            cell.ProductId = productId;

            Assert.Equal(productId, cell.ProductId);
        }
    }
}