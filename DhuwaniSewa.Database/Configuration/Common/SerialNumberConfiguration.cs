using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class SerialNumberConfiguration : IEntityTypeConfiguration<SerialNumbers>
    {
        public void Configure(EntityTypeBuilder<SerialNumbers> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
