using MediatR;
using HotelService.Application.Features.HotelManagers.Commands;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class DeleteHotelManagerCommandHandler : IRequestHandler<DeleteHotelManagerCommand>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IHotelRepository _hotelRepository;

        private readonly IMapper _mapper;

        public DeleteHotelManagerCommandHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper, IHotelRepository hotelRepository)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        public async Task Handle(DeleteHotelManagerCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var manager = hotel.Managers.FirstOrDefault(c => c.Id == request.Id && c.HotelId == request.HotelId);
            if (manager == null)
            {
                throw new NotFoundException("Hotel manager not found");
            }

            manager.IsDeleted = true;
            await _hotelManagerRepository.UpdateAsync(manager);
        }
    }
}
