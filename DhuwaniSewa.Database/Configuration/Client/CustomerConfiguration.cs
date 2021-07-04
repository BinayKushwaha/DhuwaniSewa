using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Database.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(a => a.AppUserId).IsRequired();
            builder.HasOne(a => a.AppUsers).WithOne(b => b.Customer).
                HasForeignKey<Customer>(c => c.AppUserId).
                HasConstraintName("FK_Customer_To_AppUser");
        }
    }
}
