using HotelService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

public class HotelRepositoryFactory : IHotelRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public HotelRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IHotelRepository Create()
    {
        return _serviceProvider.GetRequiredService<IHotelRepository>();
    }
}
