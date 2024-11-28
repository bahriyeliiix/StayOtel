using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ReportService.API.Extansions
{
    public static class LoggingExtensions
    {
        public static void AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });
        }
    }
}
