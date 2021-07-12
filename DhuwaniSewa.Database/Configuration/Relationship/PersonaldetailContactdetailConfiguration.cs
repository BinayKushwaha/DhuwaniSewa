using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class PersonaldetailContactdetailConfiguration : IEntityTypeConfiguration<PersonalDetailContactDetail>
    {
        public void Configure(EntityTypeBuilder<PersonalDetailContactDetail> builder)
        {
            builder.HasKey(a => new { a.PersonalDetailId, a.ContactDetailId });

            builder.HasOne(a => a.PersonalDetail).
                WithMany(b => b.PersonalDetailContactDetails).
                HasForeignKey(c => c.PersonalDetailId).
                HasConstraintName("FK_PersonalDetail_PersonalDetailContactDetail").
                IsRequired();

            builder.HasOne(a => a.ContactDetail).
                WithMany(b => b.PersonalDetailContactDetails).
                HasForeignKey(c => c.ContactDetailId).
                HasConstraintName("FK_ConactDetail_PersonalDetailContactDetail").
                IsRequired();
        }
    }
}
