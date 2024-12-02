using MediatR;
using System;
using System.Collections.Generic;
using HotelService.Domain.Entities;

namespace HotelService.Application.Features.Hotels.Queries
{
    public class GetHotelContactsQuery : IRequest<List<HotelContactDto>>
    {
        public Guid HotelId { get; set; }
    }
}
