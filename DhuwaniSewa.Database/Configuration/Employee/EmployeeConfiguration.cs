﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Database.Configuration
{
    public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Desigination).IsRequired();

            builder.HasOne(a => a.AppUsers).WithMany(b => b.Employee).
                HasForeignKey(c => c.UserId).
                HasConstraintName("FK_Employee_To_AppUser").IsRequired();
        }
    }
}
