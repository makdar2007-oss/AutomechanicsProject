using Xunit;
using AutomechanicsProject.Classes;
using System;

public class ShipmentTests
{
    [Fact]
    public void Shipment_ShouldHaveDate()
    {
        var date = DateTime.Now;
        var shipment = new Shipment { Date = date };

        Assert.Equal(date, shipment.Date);
    }

    [Fact]
    public void Shipment_ShouldStoreUserId()
    {
        var id = Guid.NewGuid();
        var shipment = new Shipment { UserId = id };

        Assert.Equal(id, shipment.UserId);
    }

    [Fact]
    public void Shipment_ShouldStoreCreatedByUserId()
    {
        var id = Guid.NewGuid();
        var shipment = new Shipment { CreatedByUserId = id };

        Assert.Equal(id, shipment.CreatedByUserId);
    }

    [Fact]
    public void Shipment_ShouldStoreTotalAmount()
    {
        var shipment = new Shipment { TotalAmount = 500 };
        Assert.Equal(500, shipment.TotalAmount);
    }

    [Fact]
    public void Shipment_Items_ShouldBeEmptyByDefault()
    {
        var shipment = new Shipment();

        Assert.NotNull(shipment.Items);
        Assert.Empty(shipment.Items);
    }

    [Fact]
    public void Shipment_ShouldAllowAddingItems()
    {
        var shipment = new Shipment();

        shipment.Items.Add(new ShipmentItem());

        Assert.Single(shipment.Items);
    }

    [Fact]
    public void Shipment_ShouldStoreUser()
    {
        var address = new Address();
        var shipment = new Shipment { User = address };

        Assert.Equal(address, shipment.User);
    }

    [Fact]
    public void Shipment_ShouldStoreCreatedByUser()
    {
        var user = new Users();
        var shipment = new Shipment { CreatedByUser = user };

        Assert.Equal(user, shipment.CreatedByUser);
    }

    [Fact]
    public void Shipment_ShouldHandleMultipleItems()
    {
        var shipment = new Shipment();

        shipment.Items.Add(new ShipmentItem());
        shipment.Items.Add(new ShipmentItem());

        Assert.Equal(2, shipment.Items.Count);
    }

    [Fact]
    public void Shipment_ShouldStoreId()
    {
        var shipment = new Shipment { Id = Guid.NewGuid() };

        Assert.NotEqual(Guid.Empty, shipment.Id);
    }

    [Fact]
    public void Shipment_TotalAmountCanBeLarge()
    {
        var shipment = new Shipment { TotalAmount = 999999 };

        Assert.Equal(999999, shipment.TotalAmount);
    }

    [Fact]
    public void Shipment_ShouldBeCreated_NotNull()
    {
        var shipment = new Shipment();

        Assert.NotNull(shipment);
    }

    [Fact]
    public void Shipment_ItemsReference_ShouldPersist()
    {
        var shipment = new Shipment();
        var item = new ShipmentItem();

        shipment.Items.Add(item);

        Assert.Contains(item, shipment.Items);
    }

    [Fact]
    public void Shipment_CanAssignNewItemsList()
    {
        var shipment = new Shipment();

        shipment.Items = new System.Collections.Generic.List<ShipmentItem>();

        Assert.NotNull(shipment.Items);
    }
}