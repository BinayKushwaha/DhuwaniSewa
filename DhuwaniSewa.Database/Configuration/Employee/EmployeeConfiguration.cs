using Microsoft.EntityFrameworkCore;
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

            builder.Property(a => a.AppUserId).IsRequired();
            builder.HasOne(a => a.AppUsers).WithOne(b => b.Employee).
                HasForeignKey<Employee>(c => c.AppUserId).
                HasConstraintName("FK_Employee_To_AppUser");
        }
    }
}
