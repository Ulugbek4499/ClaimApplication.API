using ClaimApplication.API.Commons.Services;
using ClaimApplication.Application.Commons.Interfaces;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Serilog;
using System.Text.Json.Serialization;
using Telegram.Bot;

namespace ClaimApplication.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
          //  SerilogSettings(configuration);

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

        //public static void SerilogSettings(IConfiguration configuration)
        //{
        //    Log.Logger = new LoggerConfiguration()
        //       .ReadFrom.Configuration(configuration)
        //       .MinimumLevel.Information()
        //       .WriteTo.Console()
        //       .Enrich.FromLogContext()
        //       .Enrich.WithEnvironmentUserName()
        //       .Enrich.WithMachineName()
        //       .Enrich.WithClientIp()
        //       .WriteTo.TeleSink(
        //        telegramApiKey: configuration.GetConnectionString("TelegramToken"),
        //        telegramChatId: "-1001856623462",
        //        minimumLevel: LogEventLevel.Error)
        //       .CreateLogger();
        //}
    }
}
