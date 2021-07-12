using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class ServiceSeekerConfiguration : IEntityTypeConfiguration<ServiceSeeker>
    {
        public void Configure(EntityTypeBuilder<ServiceSeeker> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.DetailsCorrectAgreed).IsRequired();
            builder.Property(a => a.Active).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.DhuwaniSewaId).IsRequired().HasMaxLength(250);

            builder.HasOne(a => a.AppUser).
                WithMany(b => b.ServiceSeeker).
                HasForeignKey(c => c.UserId).
                HasConstraintName("FK_ServiceSeeker_AppUser").IsRequired();
        }
    }
}
