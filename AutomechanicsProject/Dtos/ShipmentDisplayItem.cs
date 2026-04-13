using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomechanicsProject.Dtos
{
    internal class ShipmentDisplayItem
    {
        public string Артикул { get; set; }
        public string Название { get; set; }
        public int Количество { get; set; }
        public decimal Цена { get; set; }
        public decimal Прибыль { get; set; }
        public decimal Сумма { get; set; }
        public string Кому { get; set; }
    }
}