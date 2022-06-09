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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Login);
            builder.HasIndex(c => c.Login).IsUnique(true);
            builder.Property(c => c.FirstName).IsRequired(true);
            builder.Property(c => c.LastName).IsRequired(true);
            builder.Property(c => c.Email).IsRequired(true);
            builder.Property(c => c.Password).IsRequired(true);
        }
    }
}
