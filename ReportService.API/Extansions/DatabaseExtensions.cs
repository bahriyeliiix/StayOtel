using Microsoft.EntityFrameworkCore;
using ReportService.Infrastructure.Data;

namespace ReportService.API.Extansions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ReportServiceDb");

            services.AddDbContext<ReportServiceDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
