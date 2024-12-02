
using MediatR;
using HotelService.Domain.Entities;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using HotelService.Application.Features.HotelManagers.Commands;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class CreateHotelManagerCommandHandler : IRequestHandler<CreateHotelManagerCommand, Guid>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;

        private readonly IMapper _mapper;

        public CreateHotelManagerCommandHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateHotelManagerCommand request, CancellationToken cancellationToken)
        {
            var hotelManager = _mapper.Map<HotelManager>(request);
            await _hotelManagerRepository.AddAsync(hotelManager);
            return hotelManager.Id;
        }
    }
}
