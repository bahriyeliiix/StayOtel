using HotelService.Application.Mapping;
using HotelService.Infrastructure.Repositories;

namespace HotelService.API.Extansions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(HotelServiceMappingProfile));

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));



            services.AddScoped<IHotelRepository,HotelRepository>();
            services.AddScoped<IHotelManagerRepository,HotelManagerRepository>();
            services.AddScoped<IHotelRepositoryFactory, HotelRepositoryFactory>();
            services.AddHostedService<RabbitMqBackgroundService>();

        }
    }
}
