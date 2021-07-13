using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public sealed class UserConfiguartion : IEntityTypeConfiguration<ApplicationUsers>
    {
        public void Configure(EntityTypeBuilder<ApplicationUsers> builder)
        {
            builder.ToTable(name: "Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.UserName).IsRequired().HasMaxLength(350);

            builder.HasOne(x => x.AppUsers).WithOne(x => x.Users)
                .HasForeignKey<AppUsers>(x => x.UserId)
                .HasConstraintName("FK_AspUser_ApplicationUser").OnDelete(DeleteBehavior.Restrict);
        }
    }
}
