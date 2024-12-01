using AutoMapper;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Repositories;
using MediatR;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = _mapper.Map<Hotel>(request);
            await _hotelRepository.AddAsync(hotel);
            return hotel.Id;
        }
    }
}
