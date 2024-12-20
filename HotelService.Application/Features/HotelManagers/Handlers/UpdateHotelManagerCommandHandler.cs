﻿using MediatR;
using HotelService.Domain.Entities;
using HotelService.Application.Features.HotelManagers.DTOs;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using HotelService.Application.Features.Hotels.DTOs;
using Shared.Exceptions;
using HotelService.Application.Features.HotelManagers.Commands;
using Serilog;

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
            Log.Information("Attempting to update Hotel Manager with ID: {ManagerId}", request.Id);

            var hotelManager = await _hotelManagerRepository.GetByIdAsync(request.Id);

            if (hotelManager == null)
            {
                Log.Warning("HotelManager with ID {ManagerId} not found", request.Id);
                throw new NotFoundException($"HotelManager with ID {request.Id} not found.");
            }

            hotelManager.FirstName = request.FirstName;
            hotelManager.LastName = request.LastName;
            hotelManager.Email = request.Email;
            hotelManager.PhoneNumber = request.PhoneNumber;

            await _hotelManagerRepository.UpdateAsync(hotelManager);

            Log.Information("Hotel Manager with ID {ManagerId} updated successfully", request.Id);

            return _mapper.Map<HotelManagerDto>(hotelManager);
        }
    }
}
