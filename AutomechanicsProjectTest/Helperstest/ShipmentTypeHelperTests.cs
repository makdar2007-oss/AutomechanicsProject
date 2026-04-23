using Xunit;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Enum;

namespace AutomechanicsProjectTest.Classes
{
    public class ShipmentTypeHelperTests
    {
        [Fact]
        public void GetLocalizedName_Shipment_ReturnsValue()
        {
            var result = ShipmentTypeHelper.GetLocalizedName(ShipmentTypeEnum.Shipment);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void GetLocalizedName_WriteOff_ReturnsValue()
        {
            var result = ShipmentTypeHelper.GetLocalizedName(ShipmentTypeEnum.WriteOff);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void GetLocalizedName_Defect_ReturnsValue()
        {
            var result = ShipmentTypeHelper.GetLocalizedName(ShipmentTypeEnum.Defect);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void GetLocalizedName_Unknown_ReturnsEnumToString()
        {
            var result = ShipmentTypeHelper.GetLocalizedName((ShipmentTypeEnum)999);

            Assert.Equal("999", result);
        }

        [Fact]
        public void GetLocalizedTypeList_ReturnsThreeItems()
        {
            var list = ShipmentTypeHelper.GetLocalizedTypeList();

            Assert.NotNull(list);
            Assert.Equal(3, list.Length);
        }

        [Fact]
        public void GetLocalizedTypeList_AllItemsNotEmpty()
        {
            var list = ShipmentTypeHelper.GetLocalizedTypeList();

            foreach (var item in list)
            {
                Assert.False(string.IsNullOrWhiteSpace(item));
            }
        }

        [Fact]
        public void GetLocalizedTypeList_ContainsAllTypes()
        {
            var list = ShipmentTypeHelper.GetLocalizedTypeList();

            Assert.Equal(3, list.Length);
        }
    }
}