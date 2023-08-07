using ClaimApplication.API.Commons.Services;
using ClaimApplication.Application.Commons.Interfaces;
using System.Text.Json.Serialization;
using Telegram.Bot;

namespace ClaimApplication.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITelegramBotClient>(
                new TelegramBotClient(configuration?.GetConnectionString("TelegramToken")));

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();

            return services;
        }
    }
}
