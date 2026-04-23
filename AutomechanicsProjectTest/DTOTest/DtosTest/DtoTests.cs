using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using System;
using Xunit;

namespace AutomechanicsProjectTest.Classes
{
    public class DtoTests
    {

        [Fact]
        public void CategoryDto_SetProperties_Works()
        {
            var id = Guid.NewGuid();

            var dto = new CategoryDto
            {
                Id = id,
                Name = "Фильтры",
                ProductsCount = 5
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("Фильтры", dto.Name);
            Assert.Equal(5, dto.ProductsCount);
        }

        [Fact]
        public void ProductDto_SetProperties_Works()
        {
            var dto = new ProductDto
            {
                Id = Guid.NewGuid(),
                Article = "A1",
                Name = "Масло",
                Balance = 10,
                Price = 100
            };

            Assert.Equal("A1", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal(10, dto.Balance);
            Assert.Equal(100, dto.Price);
        }

        [Fact]
        public void ProductDto_DefaultValues_AreValid()
        {
            var dto = new ProductDto();

            Assert.Equal(0, dto.Balance);
            Assert.Equal(0, dto.Price);
        }

        [Fact]
        public void ProductBatchDto_SetProperties_Works()
        {
            var dto = new ProductBatchDto
            {
                BatchNumber = "B1",
                Balance = 10,
                Price = 200
            };

            Assert.Equal("B1", dto.BatchNumber);
            Assert.Equal(10, dto.Balance);
            Assert.Equal(200, dto.Price);
        }

        [Fact]
        public void ComboItemDto_SetProperties_Works()
        {
            var id = Guid.NewGuid();

            var dto = new ComboItemDto
            {
                Id = id,
                Text = "Категория",
                Tooltip = "Подсказка"
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("Категория", dto.Text);
            Assert.Equal("Подсказка", dto.Tooltip);
        }

        [Fact]
        public void ComboItemDto_EmptyText_Allowed()
        {
            var dto = new ComboItemDto
            {
                Text = ""
            };

            Assert.Equal("", dto.Text);
        }

        [Fact]
        public void ExpiryItemDto_NullDate_Works()
        {
            var dto = new ExpiryItemDto
            {
                ExpiryDate = null
            };

            Assert.Null(dto.ExpiryDate);
        }

        [Fact]
        public void ExpiryItemDto_WithDate_Works()
        {
            var date = DateTime.Today;

            var dto = new ExpiryItemDto
            {
                ExpiryDate = date
            };

            Assert.Equal(date, dto.ExpiryDate);
        }

        [Fact]
        public void ProductListItemDto_Flags_Work()
        {
            var dto = new ProductListItemDto
            {
                RequiresDiscount = true,
                IsExpired = false
            };

            Assert.True(dto.RequiresDiscount);
            Assert.False(dto.IsExpired);
        }

        [Fact]
        public void ProductListItemDto_DefaultFlags_False()
        {
            var dto = new ProductListItemDto();

            Assert.False(dto.RequiresDiscount);
            Assert.False(dto.IsExpired);
        }


        [Fact]
        public void RecipientDto_SetProperties_Works()
        {
            var dto = new RecipientDto
            {
                CompanyName = "ООО Тест"
            };

            Assert.Equal("ООО Тест", dto.CompanyName);
        }

        [Fact]
        public void RecipientDto_EmptyName_Allowed()
        {
            var dto = new RecipientDto
            {
                CompanyName = ""
            };

            Assert.Equal("", dto.CompanyName);
        }

        [Fact]
        public void UnitDto_SetProperties_Works()
        {
            var dto = new UnitDto
            {
                Name = "Штука",
                ShortName = "шт"
            };

            Assert.Equal("Штука", dto.Name);
            Assert.Equal("шт", dto.ShortName);
        }

        [Fact]
        public void UnitDto_DefaultValues_NotNull()
        {
            var dto = new UnitDto();

            Assert.Null(dto.Name);
            Assert.Null(dto.ShortName);
        }
    }
}