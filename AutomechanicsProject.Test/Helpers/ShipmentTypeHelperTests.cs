using Xunit;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Enum;

namespace AutomechanicsProject.Tests.Helpers
{
    public class ShipmentTypeHelperTests
    {
        [Fact]
        public void GetLocalizedName_ForShipment_ReturnsRussianName()
        {
            var name = ShipmentTypeHelper.GetLocalizedName(ShipmentTypeEnum.Shipment);
            Assert.Equal("Отгрузка", name);
        }

        [Fact]
        public void GetLocalizedName_ForWriteOff_ReturnsRussianName()
        {
            var name = ShipmentTypeHelper.GetLocalizedName(ShipmentTypeEnum.WriteOff);
            Assert.Equal("Списание", name);
        }

        [Fact]
        public void GetLocalizedName_ForDefect_ReturnsRussianName()
        {
            var name = ShipmentTypeHelper.GetLocalizedName(ShipmentTypeEnum.Defect);
            Assert.Equal("Брак", name);
        }

        [Fact]
        public void GetLocalizedTypeList_ReturnsArrayWithThreeItems()
        {
            var list = ShipmentTypeHelper.GetLocalizedTypeList();
            Assert.Equal(3, list.Length);
            Assert.Contains("Отгрузка", list);
            Assert.Contains("Списание", list);
            Assert.Contains("Брак", list);
        }
    }
}