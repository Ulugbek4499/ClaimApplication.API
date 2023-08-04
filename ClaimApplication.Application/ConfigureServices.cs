using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClaimApplication.Application.Commons.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ClaimApplication.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //  services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(option =>
            {
                option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                option.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                option.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            });
            //services.AddScoped<IUserRefreshToken, RefreshToken>();
            //services.AddScoped<IJwtToken, JwtToken>();
            //services.AddScoped<GenericExcelReport>();

            return services;
        }
    }
}
