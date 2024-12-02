using System.Reflection;
using ReportService.Application.Mappings;

namespace ReportService.API.Extansions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportProfile));
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


        }
    }
}
