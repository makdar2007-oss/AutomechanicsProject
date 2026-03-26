using Xunit;
using AutomechanicsProject.Classes;
using System;

public class ShipmentItemTests
{
    [Fact]
    public void ShipmentItem_ShouldStoreQuantity()
    {
        var item = new ShipmentItem { Quantity = 5 };
        Assert.Equal(5, item.Quantity);
    }

    [Fact]
    public void ShipmentItem_ShouldStorePrice()
    {
        var item = new ShipmentItem { Price = 100 };
        Assert.Equal(100, item.Price);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreProductName()
    {
        var item = new ShipmentItem { ProductName = "Фильтр" };
        Assert.Equal("Фильтр", item.ProductName);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreArticle()
    {
        var item = new ShipmentItem { Article = "A123" };
        Assert.Equal("A123", item.Article);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreProductId()
    {
        var id = Guid.NewGuid();
        var item = new ShipmentItem { ProductId = id };

        Assert.Equal(id, item.ProductId);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreShipmentId()
    {
        var id = Guid.NewGuid();
        var item = new ShipmentItem { ShipmentId = id };

        Assert.Equal(id, item.ShipmentId);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreProduct()
    {
        var product = new Product();
        var item = new ShipmentItem { Product = product };

        Assert.Equal(product, item.Product);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreShipment()
    {
        var shipment = new Shipment();
        var item = new ShipmentItem { Shipment = shipment };

        Assert.Equal(shipment, item.Shipment);
    }

    [Fact]
    public void ShipmentItem_QuantityCanBeZero()
    {
        var item = new ShipmentItem { Quantity = 0 };

        Assert.Equal(0, item.Quantity);
    }

    [Fact]
    public void ShipmentItem_QuantityCanBeNegative()
    {
        var item = new ShipmentItem { Quantity = -1 };

        Assert.Equal(-1, item.Quantity);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreDecimalPrice()
    {
        var item = new ShipmentItem { Price = 99.99m };

        Assert.Equal(99.99m, item.Price);
    }

    [Fact]
    public void ShipmentItem_ShouldBeCreated_NotNull()
    {
        var item = new ShipmentItem();

        Assert.NotNull(item);
    }

    [Fact]
    public void ShipmentItem_ProductCanBeNull()
    {
        var item = new ShipmentItem { Product = null };

        Assert.Null(item.Product);
    }

    [Fact]
    public void ShipmentItem_ShipmentCanBeNull()
    {
        var item = new ShipmentItem { Shipment = null };

        Assert.Null(item.Shipment);
    }

    [Fact]
    public void ShipmentItem_ShouldStoreId()
    {
        var item = new ShipmentItem { Id = Guid.NewGuid() };

        Assert.NotEqual(Guid.Empty, item.Id);
    }
}