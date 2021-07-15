using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPerson>
    {
        public void Configure(EntityTypeBuilder<ContactPerson> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Active).IsRequired().HasDefaultValue(true);
            
            builder.HasOne(a => a.PersonalDetail).
                WithOne(b => b.ContactPerson).HasForeignKey<ContactPerson>(c => c.PersonId).
                IsRequired().HasConstraintName("FK_ContactPerson_Person");
        }
    }
}
