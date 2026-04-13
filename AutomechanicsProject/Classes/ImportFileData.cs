using System.Collections.Generic;

namespace AutomechanicsProject.Classes
{
    public class ImportFileData
    {
        public List<ImportProductItem> Products { get; set; }
        public string Currency { get; set; }
        public string OrderNumber { get; set; }
    }
}