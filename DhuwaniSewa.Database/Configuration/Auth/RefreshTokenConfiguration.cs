using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Database.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(a => a.Token).IsRequired();
            builder.HasKey(a => a.Token);
            builder.Property(a => a.JwtId).IsRequired();
            builder.Property(a => a.ExpiryDate).IsRequired();
            builder.Property(a => a.CreationDate).IsRequired();

            builder.Property(a => a.UserId).IsRequired();
            builder.HasOne(a => a.User).WithOne(b => b.RefreshToken)
                .HasConstraintName("FK_RefreshToken_User").OnDelete(DeleteBehavior.Restrict);
        }
    }
}
