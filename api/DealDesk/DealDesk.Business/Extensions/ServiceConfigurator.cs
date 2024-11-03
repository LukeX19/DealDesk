using DealDesk.Business.Interfaces;
using DealDesk.Business.Services;
using DealDesk.DataAccess.Interfaces;
using DealDesk.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DealDesk.Business.Extensions
{
    public static class ServiceConfigurator
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
        }

        public static void AddApplicationAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
