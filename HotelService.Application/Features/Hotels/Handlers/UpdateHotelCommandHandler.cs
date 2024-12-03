using HotelService.Application.Features.Hotels.Commands;
using MediatR;
using AutoMapper;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using Serilog;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<HotelDto> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Attempting to update Hotel with ID: {HotelId}", request.Id);

                var hotel = await _hotelRepository.GetByIdAsync(request.Id);

                if (hotel == null)
                {
                    Log.Warning("Hotel with ID {HotelId} not found", request.Id);
                    throw new NotFoundException("Hotel not found");
                }

                hotel.Name = request.Name;
                hotel.Address = request.Address;
                hotel.City = request.City;
                hotel.Country = request.Country;

                await _hotelRepository.UpdateAsync(hotel);

                Log.Information("Hotel with ID {HotelId} updated successfully", request.Id);

                return _mapper.Map<HotelDto>(hotel);
            }
            catch (NotFoundException ex)
            {
                Log.Error(ex, "Hotel with ID {HotelId} not found", request.Id);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while updating hotel with ID: {HotelId}", request.Id);
                throw new ApplicationException("An unexpected error occurred", ex);
            }
        }
    }
}
