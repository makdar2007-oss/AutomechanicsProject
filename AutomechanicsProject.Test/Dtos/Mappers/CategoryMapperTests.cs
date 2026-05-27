using Xunit;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Tests.Mappers
{
    public class CategoryMapperTests
    {
        [Fact]
        public void ToDto_ConvertsEntityToDto()
        {
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Автозапчасти",
                Products = new List<Product> { new Product(), new Product(), new Product() }
            };

            var dto = CategoryMapper.ToDto(category);

            Assert.Equal(categoryId, dto.Id);
            Assert.Equal("Автозапчасти", dto.Name);
            Assert.Equal(3, dto.ProductsCount);
        }

        [Fact]
        public void ToDto_WhenProductsIsNull_ReturnsZeroCount()
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Тест",
                Products = null
            };

            var dto = CategoryMapper.ToDto(category);

            Assert.Equal(0, dto.ProductsCount);
        }

        [Fact]
        public void ToDto_WhenProductsIsEmpty_ReturnsZeroCount()
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Тест",
                Products = new List<Product>()
            };

            var dto = CategoryMapper.ToDto(category);

            Assert.Equal(0, dto.ProductsCount);
        }

        [Fact]
        public void ToComboItem_ConvertsDtoToComboItem()
        {
            var categoryId = Guid.NewGuid();
            var dto = new CategoryDto
            {
                Id = categoryId,
                Name = "Шины"
            };

            var comboItem = CategoryMapper.ToComboItem(dto);

            Assert.Equal(categoryId, comboItem.Id);
            Assert.Equal("Шины", comboItem.Text);
        }
    }
}