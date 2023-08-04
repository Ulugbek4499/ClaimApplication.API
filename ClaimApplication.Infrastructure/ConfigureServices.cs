﻿using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Infrastructure.Persistence.Interceptors;
using ClaimApplication.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
         //   services.AddScoped<IDeleteImg, DeleteImg>();
         //   services.AddScoped<ISaveImg, SaveImg>();
            return services;
        }
    }
}
