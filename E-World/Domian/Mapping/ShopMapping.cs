using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Mapping
{
    public class ShopMapping : IEntityTypeConfiguration<Shop>
    {

        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shop");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasColumnType("int");
            builder.Property(x => x.ShopName).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(x => x.ShopType).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(x => x.Photo).HasColumnType("nvarchar(120)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");
        }
    }
}
