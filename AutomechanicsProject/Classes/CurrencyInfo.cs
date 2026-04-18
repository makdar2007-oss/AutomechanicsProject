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
        /// <summary>
        /// Получает или задаёт код валюты 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Получает или задаёт обменный курс валюты
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Получает или задаёт отображаемое название валюты
        /// </summary>
        public string DisplayText { get; set; }
    }
}
