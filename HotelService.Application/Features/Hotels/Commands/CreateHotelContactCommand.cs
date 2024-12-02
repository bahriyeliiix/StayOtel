using MediatR;
using HotelService.Domain.Enums;
using System;

namespace HotelService.Application.Features.Hotels.Commands
{
    public class CreateHotelContactCommand : IRequest<Guid>
    {
        public Guid HotelId { get; set; }
        public ContactType ContactType { get; set; }
        public string ContactDetail { get; set; }
    }
}
