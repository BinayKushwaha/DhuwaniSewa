using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public sealed class FiscalYearConfiguration : IEntityTypeConfiguration<FiscalYear>
    {
        
        public void Configure(EntityTypeBuilder<FiscalYear> builder)
        {
            builder.HasKey(a => a.FiscalYearId);
            builder.Property(a => a.FiscalYearId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
            builder.Property(a => a.StartDate).IsRequired();
            builder.Property(a => a.EndDate).IsRequired();
        }
    }
}
