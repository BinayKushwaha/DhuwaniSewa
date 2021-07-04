using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;

namespace DhuwaniSewa.IoC.Helper
{
    public static class IdentityDataInitializer
    {
        public static void SeedIdentity(UserManager<ApplicationUsers> userManager
            ,RoleManager<ApplicationRoles> roleManager)
        {
            SeedRole(roleManager);
            SeedUser(userManager);
        }
        public static void SeedUser(UserManager<ApplicationUsers> userManager)
        {
            if (userManager.FindByNameAsync(User.Admin.ToString()).Result == null)
            {
                ApplicationUsers user = new ApplicationUsers();
                user.UserName = User.Admin.ToString();
                user.Email = "binaykushwaha2018@gmail.com";
                user.NormalizedUserName = User.Admin.ToString();
                user.EmailConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString();

                var passwordHaser = new PasswordHasher<ApplicationUsers>();
                var hashedPassword = passwordHaser.HashPassword(user, "P@ssword1");

                user.PasswordHash = hashedPassword;
                var result=userManager.CreateAsync(user);
                if (result.Result.Succeeded)
                    userManager.AddToRoleAsync(user, UserRole.Administrator.ToString()).Wait();
            }
        }
        public static void SeedRole(RoleManager<ApplicationRoles> roleManager) 
        {
            if (!roleManager.RoleExistsAsync(UserRole.Administrator.ToString()).Result)
            {
                ApplicationRoles role = new ApplicationRoles();
                role.Name = UserRole.Administrator.ToString();
                role.NormalizedName = UserRole.Administrator.ToString();
                role.Description = UserRole.Administrator.ToString();
                IdentityResult result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
