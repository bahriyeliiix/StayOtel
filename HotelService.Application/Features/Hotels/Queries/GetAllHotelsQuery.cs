using HotelService.Application.Features.Hotels.DTOs;
using MediatR;

namespace HotelService.Application.Features.Hotels.Queries
{
    public class GetAllHotelsQuery : IRequest<List<HotelDto>>
    {
    }
}
