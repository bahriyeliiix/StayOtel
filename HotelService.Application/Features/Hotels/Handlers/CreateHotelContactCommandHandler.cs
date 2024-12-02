using AutoMapper;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Repositories;
using MediatR;

namespace HotelService.Application.Features.Hotels.Commands
{
    public class CreateHotelContactCommandHandler : IRequestHandler<CreateHotelContactCommand, Guid>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;


        public CreateHotelContactCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateHotelContactCommand request, CancellationToken cancellationToken)
        {
         
            var hotelContact = _mapper.Map<HotelContact>(request);
            await _hotelRepository.AddHotelContactAsync(hotelContact);

            return hotelContact.Id;
        }
    }
}
