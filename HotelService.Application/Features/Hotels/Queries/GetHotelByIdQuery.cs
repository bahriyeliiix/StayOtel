﻿using HotelService.Application.Features.Hotels.DTOs;
using MediatR;

namespace HotelService.Application.Features.Hotels.Queries.GetHotelById
{
    public class GetHotelByIdQuery : IRequest<HotelDto>
    {
        public GetHotelByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}