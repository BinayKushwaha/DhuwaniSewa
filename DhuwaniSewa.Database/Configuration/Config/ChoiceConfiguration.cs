using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id) .IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.Active).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.DisplayName).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Enum).HasMaxLength(150);

            builder.HasOne<Category>(a => a.Category).
                WithMany(b => b.Choices).
                HasForeignKey(c => c.CategoryId).
                HasConstraintName("FK_Category_Choice").IsRequired();
        }
    }
}
