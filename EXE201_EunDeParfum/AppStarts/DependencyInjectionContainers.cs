using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Repository;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Service.Service;
using EunDeParfum_Service.Service.Implement;
using Microsoft.EntityFrameworkCore;

namespace EXE201_EunDeParfum.AppStarts
{
    public static class DependencyInjectionContainers
    {
        public static void ServiceContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            //Add_DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("hosting"));
            });

            //AddService
            services.AddScoped<ICustomerService, CustomerService>();
            //AddRepository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
