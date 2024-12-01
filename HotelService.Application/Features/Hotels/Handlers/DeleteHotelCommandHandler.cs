using HotelService.Application.Features.Hotels.Commands;
using HotelService.Infrastructure.Repositories;
using MediatR;
using Shared.Exceptions;

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
            var hotel = await _hotelRepository.GetByIdAsync(request.Id);

            if (hotel == null)
                throw new NotFoundException("Hotel not found");

            hotel.IsDeleted = true;

            await _hotelRepository.UpdateAsync(hotel);
        }
    }
}
