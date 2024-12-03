using AutoMapper;
using HotelService.Application.Features.Hotels.DTOs;
using MediatR;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Infrastructure.Repositories;
using Serilog;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, List<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetAllHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Fetching all hotels from the database");

            var hotels = await _hotelRepository.GetAllAsync();

            Log.Information("Fetched {HotelCount} hotels", hotels.Count);

            return _mapper.Map<List<HotelDto>>(hotels);
        }
    }
}
