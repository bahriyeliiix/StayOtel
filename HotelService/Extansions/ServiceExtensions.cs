using System.Reflection;
using System.Runtime.Loader;
using HotelService.Application.Features.Hotels.Handlers;
using HotelService.Application.Mapping;
using HotelService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.API.Extansions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(HotelServiceMappingProfile));



            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));




            services.AddScoped<IHotelRepository,HotelRepository>();
        }
    }
}
