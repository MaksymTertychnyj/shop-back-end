using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Data.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EntityConfigurations.Orders
{
    public class OrderAddressConfiguration : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.ToTable("OrderAddress");
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Id).IsUnique(true);
            builder.Property(o => o.Id).UseIdentityColumn();
            builder.Property(o => o.Country).IsRequired();
            builder.Property(o => o.Region).IsRequired();
            builder.Property(o => o.City).IsRequired();
            builder.Property(o => o.Place).IsRequired();
            builder.HasOne(a => a.Order)
                .WithOne(o => o.OrderAddress)
                .HasForeignKey<OrderAddress>(a => a.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
