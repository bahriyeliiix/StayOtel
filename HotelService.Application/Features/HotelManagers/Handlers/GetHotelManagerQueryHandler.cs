using MediatR;
using HotelService.Domain.Entities;
using HotelService.Application.Features.HotelManagers.Queries;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Infrastructure.Repositories;
using AutoMapper;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class GetHotelManagerQueryHandler : IRequestHandler<GetHotelManagerQuery, HotelManagerDto>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;

        public GetHotelManagerQueryHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<HotelManagerDto> Handle(GetHotelManagerQuery request, CancellationToken cancellationToken)
        {
            var manager = await _hotelManagerRepository.GetByIdAsync(request.Id);

            return _mapper.Map<HotelManagerDto>(manager);
        }
    }
}
