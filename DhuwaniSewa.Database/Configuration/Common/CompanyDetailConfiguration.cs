﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Database.Configuration
{
    public sealed class CompanyDetailConfiguration : IEntityTypeConfiguration<CompanyDetail>
    {
        public void Configure(EntityTypeBuilder<CompanyDetail> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(250);

            builder.HasOne(a => a.AppUsers).WithMany(b => b.CompanyDetail).
                HasForeignKey(c => c.AppUserId).
                HasConstraintName("FK_Companydetail_To_AppUsers").IsRequired();
        }
    }
}
