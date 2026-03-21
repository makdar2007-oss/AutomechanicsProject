using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Classes
{
    public class ShipmentItem
    {
        public Guid Id { get; set; }            
        public Guid ShipmentId { get; set; }      
        public Guid ProductId { get; set; }        
        public int Quantity { get; set; }        
        public decimal Price { get; set; }         
        public string ProductName { get; set; }  
        public string Article { get; set; }      
        public virtual Product Product { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
    public class Shipment
    {
        public Guid Id { get; set; }            
        public DateTime Date { get; set; }         
        public Guid UserId { get; set; }            
        public Guid CreatedByUserId { get; set; }   
        public decimal TotalAmount { get; set; }    

        public virtual Users User { get; set; }             
        public virtual Users CreatedByUser { get; set; }     
        public virtual List<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();
    } 
}
