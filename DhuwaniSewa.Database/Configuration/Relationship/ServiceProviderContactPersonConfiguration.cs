using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class ServiceProviderContactPersonConfiguration : IEntityTypeConfiguration<ServiceProviderContactPerson>
    {
        public void Configure(EntityTypeBuilder<ServiceProviderContactPerson> builder)
        {
            builder.HasKey(a => new { a.ContactPersonId, a.ServiceProviderId });

            builder.HasOne(a => a.ServiceProvider).
                WithMany(b => b.ServiceProviderContactPerson).
                HasForeignKey(c => c.ServiceProviderId).
                IsRequired().
                HasConstraintName("FK_SerciceProvider_ServiceProviderContactPerson");

            builder.HasOne(a => a.ContactPerson).
                WithOne(b => b.ServiceProviderContactPerson).
                HasForeignKey<ServiceProviderContactPerson>(c => c.ContactPersonId).
                IsRequired().
                HasConstraintName("FK_ServiceProviderContactPerson_ContactPerson");
        }
    }
}
