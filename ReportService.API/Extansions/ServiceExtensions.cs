using System.Reflection;
using ReportService.Application.Interfaces;
using ReportService.Application.Mappings;
using ReportService.Infrastructure.Messaging;
using ReportService.Infrastructure.Repositories;

namespace ReportService.API.Extansions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportProfile));
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportProducer, ReportProducer>();


        }
    }
}
