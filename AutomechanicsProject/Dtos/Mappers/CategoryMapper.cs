using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;

namespace AutomechanicsProject.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(Category entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ProductsCount = entity.Products?.Count ?? 0
            };
        }

        public static ComboItemDto ToComboItem(CategoryDto dto)
        {
            return new ComboItemDto
            {
                Id = dto.Id,
                Text = dto.Name
            };
        }
    }
}