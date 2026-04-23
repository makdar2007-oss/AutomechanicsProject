using System; 
using AutomechanicsProject.Enum;
using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с типами отгрузки
    /// </summary>
    public static class ShipmentTypeHelper
    {
        /// <summary>
        /// Получить локализованное название типа отгрузки
        /// </summary>
        public static string GetLocalizedName(ShipmentTypeEnum type)
        {
            switch (type)
            {
                case ShipmentTypeEnum.Shipment:
                    return Resources.ShipmentType_Shipment;
                case ShipmentTypeEnum.WriteOff:
                    return Resources.ShipmentType_WriteOff;
                case ShipmentTypeEnum.Defect:
                    return Resources.ShipmentType_Defect;
                default:
                    return type.ToString();
            }
        }

        /// <summary>
        /// Получить список всех типов для ComboBox
        /// </summary>
        public static string[] GetLocalizedTypeList()
        {
            return new[]
            {
                Resources.ShipmentType_Shipment,
                Resources.ShipmentType_WriteOff,
                Resources.ShipmentType_Defect
            };
        }
    }
}