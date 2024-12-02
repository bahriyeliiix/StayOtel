using MediatR;
using HotelService.Domain.Entities;
using HotelService.Application.Features.HotelManagers.Queries;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Infrastructure.Repositories;
using AutoMapper;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class GetHotelManagerByHotelIdQueryHandler : IRequestHandler<GetHotelManagerByHotelIdQuery, HotelManagerDto>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;

        public GetHotelManagerByHotelIdQueryHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<HotelManagerDto> Handle(GetHotelManagerByHotelIdQuery request, CancellationToken cancellationToken)
        {
            var manager = await _hotelManagerRepository.GetAllByHotelIdAsync(request.HotelId);

            return _mapper.Map<HotelManagerDto>(manager);
        }
    }
}
