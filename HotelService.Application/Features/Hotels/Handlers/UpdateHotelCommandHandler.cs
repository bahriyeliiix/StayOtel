using HotelService.Application.Features.Hotels.Commands;
using MediatR;
using AutoMapper;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;

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
            var hotel = await _hotelRepository.GetByIdAsync(request.Id);

            if (hotel == null)
                throw new NotFoundException("Hotel not found");


            hotel.Name = request.Name;
            hotel.Address = request.Address;
            hotel.City = request.City;
            hotel.Country = request.Country;

            await _hotelRepository.UpdateAsync(hotel);

            return _mapper.Map<HotelDto>(hotel);
        }
    }
}
