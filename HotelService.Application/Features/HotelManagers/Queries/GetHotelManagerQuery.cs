using MediatR;
using HotelService.Domain.Entities;
using System;
using HotelService.Application.Features.HotelManagers.DTOs;

namespace HotelService.Application.Features.HotelManagers.Queries
{
    public class GetHotelManagerQuery : IRequest<HotelManagerDto>
    {
        public Guid Id { get; set; }

    }
}
