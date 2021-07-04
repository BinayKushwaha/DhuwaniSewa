using DhuwaniSewa.Model.DbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DhuwaniSewa.Database.Context
{
    public sealed class ApplicationContext : IdentityDbContext<ApplicationUsers>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet<ApplicationRoles> ApplicationRoles { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<PersonalDetail> PersonalDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CompanyDetail> CompanyDetails { get; set; }
    }
}
