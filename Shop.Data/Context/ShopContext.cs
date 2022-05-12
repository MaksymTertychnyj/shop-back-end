using Microsoft.EntityFrameworkCore;
using Shop.Data.Entities;
using Shop.Data.EntityConfigurations;
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

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
