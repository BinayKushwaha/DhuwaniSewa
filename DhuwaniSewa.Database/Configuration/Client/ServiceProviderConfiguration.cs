using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.Active).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.DhuwaniSewaId).IsRequired().HasMaxLength(250);
            builder.Property(a => a.DetailsCorrectAgreed).IsRequired();

            builder.HasOne(a => a.AppUser).
                WithMany(b => b.ServiceProvider).
                HasForeignKey(c => c.UserId).
                HasConstraintName("FK_ServiceProvider_AppUser").IsRequired();
        }
    }
}
