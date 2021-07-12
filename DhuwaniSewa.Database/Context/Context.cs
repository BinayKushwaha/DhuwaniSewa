using DhuwaniSewa.Database.Configuration;
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
        public DbSet<ServiceProvider> ServiceProvider { get; set; }
        public DbSet<ServiceSeeker>  ServiceSeeker { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Choice> Choice { get; set; }
        public DbSet<DocumentDetail> DocumentDetail{ get; set; }
        public DbSet<ContactDetail> ContactDetail{ get; set; }
        public DbSet<FiscalYear> FiscalYear{ get; set; }
        public DbSet<PersonalDetailContactDetail> PersonalDetailContactDetail{ get; set; }
        public DbSet<PersonalDetailDocumentDetail> PersonalDetailDocumentDetail{ get; set; }
        public DbSet<ServiceProviderVehicleDetail> ServiceProviderVehicleDetail{ get; set; }
        public DbSet<RefreshToken> RefreshToken{ get; set; }
    }
}
