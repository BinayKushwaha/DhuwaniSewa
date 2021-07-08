using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class ServiceProviderVehicleDetailConfiguration : IEntityTypeConfiguration<ServiceProviderVehicleDetail>
    {
        public void Configure(EntityTypeBuilder<ServiceProviderVehicleDetail> builder)
        {
            builder.HasKey(a => new { a.ServiceProviderId, a.VehicleDetailId });

            builder.HasOne(a => a.VehicleDetail).
                WithOne(b => b.ServiceProviderVehicleDetail).
                HasForeignKey<ServiceProviderVehicleDetail>(c => c.VehicleDetailId).
                HasConstraintName("FK_ServiceProviderVehicleDetail_VehicleDetail").
                IsRequired();

            builder.HasOne<ServiceProvider>(a => a.ServiceProvider).
                WithMany(b => b.ServiceProviderVehicleDetail).
                HasForeignKey(c => c.ServiceProviderId).
                HasConstraintName("FK_SericeProvider_ServiceProviderVehicleDetail").
                IsRequired();
        }
    }
}
