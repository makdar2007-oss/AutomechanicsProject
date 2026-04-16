using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;

namespace AutomechanicsProject.Mappers
{
    /// <summary>
    /// Предоставляет статические методы для преобразования объектов категорий между различными слоями приложения.
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        /// Преобразует сущность категории из базы данных в DTO для передачи данных.
        /// </summary>
        public static CategoryDto ToDto(Category entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ProductsCount = entity.Products?.Count ?? 0
            };
        }

        /// <summary>
        /// Преобразует DTO категории в элемент для выпадающего списка.
        /// </summary>
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