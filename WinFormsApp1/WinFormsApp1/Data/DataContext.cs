using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<ItemCart> itemcarts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ItemOrder> itemorders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Ensure the table name matches

                entity.Property(u => u.Id)
                        .HasColumnName("id");  // Ensures EF Core maps "Id" to the "id" column in PostgreSQL

                entity.Property(e => e.Name)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("name");
                
                entity.Property(u => u.Phone)
                         .HasMaxLength(100)
                         .IsUnicode(false)
                        .HasColumnName("phone");

                entity.Property(u => u.Address)
                .HasMaxLength(100)
                    .IsUnicode(false)
                        .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.Login)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products"); // Ensure the table name matches

                entity.Property(u => u.Id)
                        .HasColumnName("id");  // Ensures EF Core maps "Id" to the "id" column in PostgreSQL

                entity.Property(e => e.Name)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("name");

                entity.Property(u => u.Price)
                        .HasColumnName("price");

                entity.Property(u => u.Quantity)
                        .HasColumnName("quantity");
                entity.Property(u => u.Description)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("description");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders"); // Ensure the table name matches

                entity.Property(u => u.Id)
                        .HasColumnName("id");  // Ensures EF Core maps "Id" to the "id" column in PostgreSQL

                entity.Property(e => e.Status)
                        .HasConversion<string>()
                        .HasColumnName("status");

                entity.Property(u => u.Data_created)
                        .HasColumnName("data_created");

                entity.Property(u => u.Id_user)
                        .HasColumnName("id_user");
            });

            modelBuilder.Entity<ItemOrder>(entity =>
            {
                entity.ToTable("itemorders"); // Ensure the table name matches

                entity.Property(u => u.Id)
                        .HasColumnName("id");  // Ensures EF Core maps "Id" to the "id" column in PostgreSQL

                entity.Property(e => e.Id_product)
                        .HasColumnName("id_product");

                entity.Property(u => u.Id_order)
                        .HasColumnName("id_order");

                entity.Property(u => u.Quantity)
                        .HasColumnName("quantity");
            });
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts"); // Ensure the table name matches

                entity.Property(u => u.Id)
                        .HasColumnName("id");  // Ensures EF Core maps "Id" to the "id" column in PostgreSQL


                entity.Property(u => u.Data_created)
                        .HasColumnName("data_created");

                entity.Property(u => u.Id_user)
                        .HasColumnName("id_user");
            });

            modelBuilder.Entity<ItemCart>(entity =>
            {
                entity.ToTable("itemcarts"); // Ensure the table name matches

                entity.Property(u => u.Id)
                        .HasColumnName("id");  // Ensures EF Core maps "Id" to the "id" column in PostgreSQL

                entity.Property(e => e.Id_product)
                        .HasColumnName("id_product");

                entity.Property(u => u.Id_cart)
                        .HasColumnName("id_cart");

                entity.Property(u => u.Quantity)
                        .HasColumnName("quantity");
            });
        }

    }
}
