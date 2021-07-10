using DhuwaniSewa.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DhuwaniSewa.IoC.Helper;

namespace DhuwaniSewa.IoC
{
    public static class IServiceCollectionExtension
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:ConnectionDbString"];

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            AddIdentityServices.ConfigureService(services,configuration);
            AddRepositoryServices.ConfiguerServices(services);
            AddCustomMapper.ConfigureServices(services);
            AddCommonServices.ConfigureServices(services);
            AddDomainServices.ConfigureServices(services);
        }
    }
}
