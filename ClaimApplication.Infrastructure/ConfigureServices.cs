using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Infrastructure.Persistence;
using ClaimApplication.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClaimApplication.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnect"));
               // options.UseLazyLoadingProxies();
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            return services;
        }
    }
}
