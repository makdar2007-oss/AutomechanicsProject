using AutomechanicsProject.Classes;
using System.Collections.Generic;
using Xunit;

public class ImportFileDataTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var data = new ImportFileData
        {
            Currency = "USD",
            Products = new List<ImportProductItem>()
        };

        Assert.Equal("USD", data.Currency);
        Assert.NotNull(data.Products);
    }

    [Fact]
    public void Products_Default_NotNull()
    {
        var data = new ImportFileData();

        Assert.Null(data.Products);
    }
}