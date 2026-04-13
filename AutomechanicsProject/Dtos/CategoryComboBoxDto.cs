using System;

namespace AutomechanicsProject.Classes.Dtos  
{
    public class CategoryComboBoxDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; }
    }
}