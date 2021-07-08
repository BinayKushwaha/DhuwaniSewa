using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class DocumentDetailConfiguration : IEntityTypeConfiguration<DocumentDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentDetail> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(a => a.Type).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Number).IsRequired().HasMaxLength(100);
            builder.Property(a => a.IssuedDistrict).HasMaxLength(100);
        }
    }
}
