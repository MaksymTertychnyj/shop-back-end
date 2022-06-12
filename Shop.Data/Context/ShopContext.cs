using Microsoft.EntityFrameworkCore;
using Shop.Data.Entities;
using Shop.Data.Entities.Orders;
using Shop.Data.EntityConfigurations;
using Shop.Data.EntityConfigurations.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Context
{
    public class ShopContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<JsonModel>? JsonModels { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderAddress>? OrderAddresses { get; set; }
        public DbSet<OrderProduct>? OrderProducts { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new JsonModelConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderAddressConfiguration());
            builder.ApplyConfiguration(new OrderProductConfiguration());
        }
    }
}
