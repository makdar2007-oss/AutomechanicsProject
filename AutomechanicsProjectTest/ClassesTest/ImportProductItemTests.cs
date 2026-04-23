using Xunit;
using AutomechanicsProject.Classes;

public class ImportProductItemTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var item = new ImportProductItem
        {
            Article = "A1",
            ProductName = "Масло",
            Quantity = 5,
            Price = 100
        };

        Assert.Equal("A1", item.Article);
        Assert.Equal("Масло", item.ProductName);
        Assert.Equal(5, item.Quantity);
        Assert.Equal(100, item.Price);
    }

    [Fact]
    public void NegativeQuantity_Works()
    {
        var item = new ImportProductItem { Quantity = -1 };

        Assert.Equal(-1, item.Quantity);
    }
}