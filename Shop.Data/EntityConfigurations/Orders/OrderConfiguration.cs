using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Data.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Data.Entities;

namespace Shop.Data.EntityConfigurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Id).IsUnique(true);
            builder.Property(o => o.Id).UseIdentityColumn();
            builder.Property(o => o.DateRegister).IsRequired();
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerLogin)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.OrderAddress)
                .WithOne(a => a.Order)
                .HasForeignKey<Order>(o => o.OrderAddressId);
        }
    }
}
