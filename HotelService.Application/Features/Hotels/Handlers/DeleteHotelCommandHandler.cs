using HotelService.Application.Features.Hotels.Commands;
using HotelService.Infrastructure.Repositories;
using MediatR;
using Shared.Exceptions;
using Serilog;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Attempting to delete hotel with ID: {HotelId}", request.Id);

            var hotel = await _hotelRepository.GetByIdAsync(request.Id);

            if (hotel == null)
            {
                Log.Warning("Hotel with ID {HotelId} not found", request.Id);
                throw new NotFoundException("Hotel not found");
            }

            hotel.IsDeleted = true;

            await _hotelRepository.UpdateAsync(hotel);

            Log.Information("Hotel with ID {HotelId} marked as deleted successfully", request.Id);
        }
    }
}
