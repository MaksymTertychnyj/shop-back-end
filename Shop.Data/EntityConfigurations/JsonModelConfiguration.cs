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
    public class JsonModelConfiguration : IEntityTypeConfiguration<JsonModel>
    {
        public void Configure(EntityTypeBuilder<JsonModel> builder)
        {
            builder.ToTable("Json_model");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique(true);
            builder.HasOne(m => m.Category)
                .WithOne(c => c.JsonModel)
                .HasForeignKey<JsonModel>(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(m => m.Id).UseIdentityColumn();
        }
    }
}
