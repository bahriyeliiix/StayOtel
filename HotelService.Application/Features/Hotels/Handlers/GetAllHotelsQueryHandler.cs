using AutoMapper;
using HotelService.Application.Features.Hotels.DTOs;
using MediatR;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Infrastructure.Repositories;

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
            var hotels = await _hotelRepository.GetAllAsync();
            return _mapper.Map<List<HotelDto>>(hotels);
        }
    }
}
