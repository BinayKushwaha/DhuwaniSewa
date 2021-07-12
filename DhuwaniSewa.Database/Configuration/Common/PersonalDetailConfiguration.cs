using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Database.Configuration
{
    public class PersonalDetailConfiguration : IEntityTypeConfiguration<PersonalDetail>
    {
        public void Configure(EntityTypeBuilder<PersonalDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.MiddleName).HasMaxLength(200);

            builder.HasOne(a => a.AppUsers).WithMany(b => b.PersonalDetail).
                HasForeignKey(c => c.AppUserId).
                HasConstraintName("FK_PersonalDetail_To_AppUsers").IsRequired();
        }
    }
}
