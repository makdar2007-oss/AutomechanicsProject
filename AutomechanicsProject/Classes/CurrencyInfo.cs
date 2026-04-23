using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Информация о валюте
    /// </summary>
    public class CurrencyInfo
    {
        /// <summary>
        /// Код валюты 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Обменный курс валюты
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Отображаемое название валюты
        /// </summary>
        public string DisplayText { get; set; }
    }
}
