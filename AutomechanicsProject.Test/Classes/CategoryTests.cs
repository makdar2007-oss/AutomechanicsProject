using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class CategoryTests
    {
        [Fact]
        public void CanCreateCategory()
        {
            var category = new Category();
            Assert.NotNull(category);
        }

        [Fact]
        public void CanSetCategoryName()
        {
            var category = new Category();
            category.Name = "Автозапчасти";
            Assert.Equal("Автозапчасти", category.Name);
        }

        [Fact]
        public void IsDeleted_DefaultValueIsFalse()
        {
            var category = new Category();
            Assert.False(category.IsDeleted);
        }
    }
}