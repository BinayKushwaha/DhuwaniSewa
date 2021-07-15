using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    class UserPersonDetailConfiguration : IEntityTypeConfiguration<UserPersonDetail>
    {
        public void Configure(EntityTypeBuilder<UserPersonDetail> builder)
        {
            builder.HasKey(a => new { a.UserId, a.PersonId });

            builder.HasOne(a => a.User).
                WithMany(b=>b.PersonalDetail).
                HasForeignKey(c=>c.UserId).
                HasConstraintName("FK_User_UserPersonDetail").IsRequired();

            builder.HasOne(a => a.PersonalDetail).
                WithOne(b => b.AppUsers).
                HasForeignKey<UserPersonDetail>(c => c.PersonId).
                HasConstraintName("FK_UserPersonDetail_Persondetail").IsRequired();
        }
    }
}
