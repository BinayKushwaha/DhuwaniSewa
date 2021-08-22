using DhuwaniSewa.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.IoC
{
    public static class AddDomainServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServiceProviderService, ServiceProviderService>();
            services.AddTransient<IPersonDetailService, PersonDetailService>();
            services.AddTransient<IFiscalYearService, FiscalYearService>();
            services.AddTransient<ISerialNumberSevice, SerialNumberService>();
            services.AddTransient<IOtpService, OtpService>();

        }
    }
}
