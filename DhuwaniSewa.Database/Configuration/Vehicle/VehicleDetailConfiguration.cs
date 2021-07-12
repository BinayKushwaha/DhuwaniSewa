using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class VehicleDetailConfiguration : IEntityTypeConfiguration<VehicleDetail>
    {
        public void Configure(EntityTypeBuilder<VehicleDetail> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.RegistrationNumber).IsRequired().HasMaxLength(100);
            builder.Property(a => a.MaxWeight).IsRequired();
            builder.Property(a => a.WeightUnit).IsRequired();
            builder.Property(a => a.WheelType).IsRequired();
            builder.Property(a => a.Model).HasMaxLength(250);

            builder.HasOne<Choice>(a => a.Brand).
                WithMany(b => b.VehicleDetailBrand).
                HasForeignKey(c => c.BrandId).
                HasConstraintName("Fk_VehicleDetail_Brand_Choice").IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Choice>(a => a.Type).
                WithMany(b => b.VehicleDetailType).
                HasForeignKey(c => c.TypeId).
                HasConstraintName("FK_VehicleDetail_Type_Choice").IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
