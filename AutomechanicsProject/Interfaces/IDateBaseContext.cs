using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Описывает доступ к контексту базы данных приложения
    /// </summary>
    public interface IDateBaseContext
    {
        /// <summary>
        /// Возвращает пользователей
        /// </summary>
        DbSet<Users> Users { get; set; }

        /// <summary>
        /// Возвращает категории товаров
        /// </summary>
        DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Возвращает товары
        /// </summary>
        DbSet<Product> Products { get; set; }

        /// <summary>
        /// Возвращает отгрузки
        /// </summary>
        DbSet<Shipment> Shipments { get; set; }

        /// <summary>
        /// Возвращает позиции отгрузок
        /// </summary>
        DbSet<ShipmentItem> ShipmentItems { get; set; }

        /// <summary>
        /// Возвращает роли пользователей
        /// </summary>
        DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Возвращает адреса и контрагентов
        /// </summary>
        DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Возвращает единицы измерения
        /// </summary>
        DbSet<Unit> Units { get; set; }

        /// <summary>
        /// Возвращает поставщиков
        /// </summary>
        DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Возвращает поставки
        /// </summary>
        DbSet<Supply> Supplies { get; set; }

        /// <summary>
        /// Возвращает позиции поставок
        /// </summary>
        DbSet<SupplyPosition> SupplyPositions { get; set; }

        /// <summary>
        /// Возвращает ячейки склада
        /// </summary>
        DbSet<WarehouseCell> WarehouseCells { get; set; }

        /// <summary>
        /// Возвращает доступ к операциям базы данных
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Сохраняет изменения в базе данных
        /// </summary>
        int SaveChanges();
    }
}