using HotelService.Infrastructure.Repositories;

public interface IHotelRepositoryFactory
{
    IHotelRepository Create();
}
