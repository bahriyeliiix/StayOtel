using AutoMapper;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Repositories;
using MediatR;
using Serilog;

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
            Log.Information("Creating a new hotel contact for hotel with ID: {HotelId}", request.HotelId);

            var hotelContact = _mapper.Map<HotelContact>(request);
            await _hotelRepository.AddHotelContactAsync(hotelContact);

            Log.Information("Hotel contact created successfully with ID: {ContactId}", hotelContact.Id);

            return hotelContact.Id;
        }
    }
}
