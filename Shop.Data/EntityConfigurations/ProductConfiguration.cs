using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Id).IsUnique(true);
            builder.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.JsonParameters).IsRequired(true);
            builder.Property(p => p.CategoryId).IsRequired(true);
            builder.Property(p => p.Id).UseIdentityColumn();
        }
    }
}
