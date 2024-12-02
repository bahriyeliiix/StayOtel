using MediatR;
using HotelService.Domain.Entities;
using HotelService.Application.Features.HotelManagers.DTOs;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using HotelService.Application.Features.Hotels.DTOs;
using Shared.Exceptions;
using HotelService.Application.Features.HotelManagers.Commands;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class UpdateHotelManagerCommandHandler : IRequestHandler<HotelManagerUpdateCommand, HotelManagerDto>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;


        public UpdateHotelManagerCommandHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<HotelManagerDto> Handle(HotelManagerUpdateCommand request, CancellationToken cancellationToken)
        {
            var hotelManager = await _hotelManagerRepository.GetByIdAsync(request.Id);

            if (hotelManager == null)
            {
                throw new NotFoundException($"HotelManager with ID {request.Id} not found.");
            }

            hotelManager.FirstName = request.FirstName;
            hotelManager.LastName = request.LastName;
            hotelManager.Email = request.Email;
            hotelManager.PhoneNumber = request.PhoneNumber;

            await _hotelManagerRepository.UpdateAsync(hotelManager);


            return _mapper.Map<HotelManagerDto>(hotelManager);
        }
    }
}
