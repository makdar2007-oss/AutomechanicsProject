using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomechanicsProject.Classes
{
    public class Product
    {
        public virtual Category Category { get; set; }
        public Guid Id { get; set; }
        public string Article { get; set; }          
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Unit { get; set; } = "шт";      
        public decimal Price { get; set; }             
        public int Balance { get; set; }              
    }
}
