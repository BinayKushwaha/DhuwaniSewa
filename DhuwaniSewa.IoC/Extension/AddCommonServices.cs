using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Application.User;
using Microsoft.Extensions.DependencyInjection;
using DhuwaniSewa.Domain;

namespace DhuwaniSewa.IoC
{
    public static class AddCommonServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserDomain, UserDomain>();
            services.AddTransient<IUserApplication, UserApplication>();
        }
    }
}
