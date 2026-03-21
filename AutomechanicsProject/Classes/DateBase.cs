using AutomechanicsProject.Classes.AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutomechanicsProject.Classes
{
    public class DateBase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentItem> ShipmentItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=AutomechanicDB;Username=postgres;Password=1234");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("Идентификатор")
                    .HasDefaultValueSql("uuid_generate_v4()");
                
                entity.Property(e => e.Position)
                    .HasColumnName("Должность")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("Идентификатор")
                    .HasDefaultValueSql("uuid_generate_v4()");
                
                entity.Property(e => e.Surname)
                    .HasColumnName("Фамилия")
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Name)
                    .HasColumnName("Имя")
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Lastname)
                    .HasColumnName("Отчество")
                    .HasMaxLength(100);
                
                entity.Property(e => e.Login)
                    .HasColumnName("Логин")
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.HasIndex(e => e.Login)
                    .IsUnique();
                
                entity.Property(e => e.Password)
                    .HasColumnName("Пароль")
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.RoleId)
                    .HasColumnName("РольПользователя");

                entity.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Ignore(u => u.RoleName);
                entity.Ignore(u => u.FullName);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("Идентификатор")
                    .HasDefaultValueSql("uuid_generate_v4()");
                
                entity.Property(e => e.Name)
                    .HasColumnName("Название")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("Идентификатор")
                    .HasDefaultValueSql("uuid_generate_v4()");
                
                entity.Property(e => e.Article)
                    .HasColumnName("Артикул")
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Name)
                    .HasColumnName("Название")
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID");
                
                entity.Property(e => e.Unit)
                    .HasColumnName("ЕдиницаИзмерения")
                    .HasMaxLength(50)
                    .HasDefaultValue("шт");
                
                entity.Property(e => e.Price)
                    .HasColumnName("Цена")
                    .HasColumnType("decimal(10,2)");
                
                entity.Property(e => e.Balance)
                    .HasColumnName("Остаток")
                    .HasDefaultValue(0);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipping");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("Идентификатор")
                    .HasDefaultValueSql("uuid_generate_v4()");
                
                entity.Property(e => e.UserId)
                    .HasColumnName("ПолныйИдент");
                
                entity.Property(e => e.CreatedByUserId)
                    .HasColumnName("СоздалИдент");
                
                entity.Property(e => e.Date)
                    .HasColumnName("ДатаСоздания")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.Property(e => e.TotalAmount)
                    .HasColumnName("ОбщаяСумма")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValue(0);

                entity.HasOne(s => s.User)
                    .WithMany()
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(s => s.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.ToTable("ShippingPositions");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("Идентификатор")
                    .HasDefaultValueSql("uuid_generate_v4()");
                
                entity.Property(e => e.ShipmentId)
                    .HasColumnName("ОтгрузИдент");
                
                entity.Property(e => e.ProductId)
                    .HasColumnName("ТоварИдент");
                
                entity.Property(e => e.Quantity)
                    .HasColumnName("КоличествоТовара");
                
                entity.Property(e => e.Price)
                    .HasColumnName("Цена")
                    .HasColumnType("decimal(10,2)");
                
                entity.Property(e => e.ProductName)
                    .HasColumnName("НазваниеТовара")
                    .HasMaxLength(255);
                
                entity.Property(e => e.Article)
                    .HasColumnName("Артикул")
                    .HasMaxLength(100);

                entity.HasOne(si => si.Shipment)
                    .WithMany(s => s.Items)
                    .HasForeignKey(si => si.ShipmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(si => si.Product)
                    .WithMany()
                    .HasForeignKey(si => si.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}