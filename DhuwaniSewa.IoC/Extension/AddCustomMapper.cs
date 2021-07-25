using DhuwaniSewa.Domain;
using DhuwaniSewa.Model.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.IoC
{
    public static class AddCustomMapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPersonalDetailMapper, PersonDetailMapper>();
            services.AddTransient<IServiceProviderMapper, ServiceProviderMapper>();

        }
    }
}
