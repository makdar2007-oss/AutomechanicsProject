// ShipmentItemTests.cs
using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class ShipmentItemTests
    {
        [Fact]
        public void ScrapMetal_DefaultValue_IsFalse()
        {
            var item = new ShipmentItem();

            Assert.False(item.ScrapMetal);
        }

        [Fact]
        public void IsMetal_DefaultValue_IsFalse()
        {
            var item = new ShipmentItem();

            Assert.False(item.IsMetal);
        }

        [Fact]
        public void CanSetScrapMetalAndIsMetal()
        {
            var item = new ShipmentItem();

            item.ScrapMetal = true;
            item.IsMetal = true;

            Assert.True(item.ScrapMetal);
            Assert.True(item.IsMetal);
        }
    }
}