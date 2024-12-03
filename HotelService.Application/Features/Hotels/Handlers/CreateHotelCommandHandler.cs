using AutoMapper;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Repositories;
using MediatR;
using Serilog;

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
            Log.Information("Attempting to create a new hotel with name: {HotelName}", request.Name);

            try
            {
                var hotel = _mapper.Map<Hotel>(request);

                await _hotelRepository.AddAsync(hotel);

                Log.Information("Hotel created successfully with ID: {HotelId}", hotel.Id);

                return hotel.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred while creating the hotel: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
