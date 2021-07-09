using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DhuwaniSewa.Domain;

namespace DhuwaniSewa.IoC
{
    public static class AddCommonServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
