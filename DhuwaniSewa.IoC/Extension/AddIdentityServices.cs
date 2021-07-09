using DhuwaniSewa.Database.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Domain;

namespace DhuwaniSewa.IoC.Helper
{
    public static class AddIdentityServices
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<ApplicationUsers>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<ApplicationRoles>()
            .AddEntityFrameworkStores<ApplicationContext>();
            
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddOptions();
            var appSettingsSection = configuration.GetSection("JwtConfiguration");
            services.Configure<JwtConfiguration>(a=>appSettingsSection.Bind(a));

            // configure jwt authentication  
            var serviceConfiguration = appSettingsSection.Get<JwtConfiguration>();
            var JwtSecretkey = Encoding.ASCII.GetBytes(serviceConfiguration.JwtSetting.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;

            });

            services.Configure<IdentityOptions>(opt =>
            {
                // password settings
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            });
        }
    }
}
