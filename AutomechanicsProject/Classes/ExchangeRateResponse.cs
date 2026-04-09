using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomechanicsProject.Classes
{
    internal class ExchangeRateResponse
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public string Date { get; set; }
    }
}
