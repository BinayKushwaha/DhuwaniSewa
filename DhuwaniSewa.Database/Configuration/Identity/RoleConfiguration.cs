using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Database.Configuration
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<ApplicationRoles>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoles> builder)
        {
            builder.ToTable(name: "Roles");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}
