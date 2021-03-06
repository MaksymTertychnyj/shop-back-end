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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.HasIndex(c =>  c.Id).IsUnique(true);
            builder.HasIndex(c => c.Name).IsUnique(true);
            builder.HasOne(category => category.Department)
                .WithMany(department => department.Categories)
                .HasForeignKey(category => category.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(c => c.Name).IsRequired(true);
            builder.Property(c => c.DepartmentId).IsRequired(true);
            builder.Property(c => c.Id).UseIdentityColumn();
        }
    }
}
