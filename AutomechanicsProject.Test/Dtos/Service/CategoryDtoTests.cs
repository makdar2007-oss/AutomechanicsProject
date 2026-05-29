using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class CategoryDtoTests
    {
        [Fact]
        public void CanCreateCategoryDto()
        {
            var dto = new CategoryDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetIdAndName()
        {
            var id = Guid.NewGuid();
            var dto = new CategoryDto
            {
                Id = id,
                Name = "Автозапчасти"
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("Автозапчасти", dto.Name);
        }

        [Fact]
        public void ProductsCount_DefaultValue_IsZero()
        {
            var dto = new CategoryDto();
            Assert.Equal(0, dto.ProductsCount);
        }

        [Fact]
        public void CanSetProductsCount()
        {
            var dto = new CategoryDto { ProductsCount = 25 };
            Assert.Equal(25, dto.ProductsCount);
        }
    }
}