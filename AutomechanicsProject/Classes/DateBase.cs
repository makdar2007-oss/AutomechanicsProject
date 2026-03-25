using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Контекст базы данных для работы с автомеханикой
    /// </summary>
    public class DateBase : DbContext
    {
        /// <summary>
        /// Коллекция пользователей системы
        /// </summary>
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// Коллекция категорий товаров
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Коллекция товаров
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Коллекция отгрузок
        /// </summary>
        public DbSet<Shipment> Shipments { get; set; }

        /// <summary>
        /// Коллекция позиций отгрузок
        /// </summary>
        public DbSet<ShipmentItem> ShipmentItems { get; set; }

        /// <summary>
        /// Коллекция ролей пользователей
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Коллекция адресов
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Настройка подключения к базе данных
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=project_auto;Username=postgres;Password=1234");
        }

        /// <summary>
        /// Настройка модели базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            // Настройка таблицы ролей
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(e => e.Position).IsUnique();
            });

            // Настройка таблицы пользователей
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(100);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Login).IsUnique();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .IsRequired();

                entity.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Ignore(u => u.RoleName);
                entity.Ignore(u => u.FullName);
            });

            // Настройка таблицы категорий
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Настройка таблицы товаров
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Article)
                    .HasColumnName("article")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Article).IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .IsRequired();

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(50)
                    .HasDefaultValue("шт");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasDefaultValue(0);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка таблицы адресов
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            // Настройка таблицы отгрузок
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("shipment");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(e => e.CreatedByUserId)
                    .HasColumnName("created_by_user_id")
                    .IsRequired();

                entity.Property(e => e.Date)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
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

            // Настройка таблицы позиций отгрузок
            modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.ToTable("shipment_positions");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.ShipmentId)
                    .HasColumnName("shipment_id")
                    .IsRequired();

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Article)
                    .HasColumnName("article")
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