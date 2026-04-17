using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Информация о валюте
    /// </summary>
    public class CurrencyInfo
    {
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public string DisplayText { get; set; }
    }
}
