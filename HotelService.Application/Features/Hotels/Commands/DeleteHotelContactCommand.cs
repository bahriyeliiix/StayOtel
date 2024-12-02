using System;
using MediatR;

namespace HotelService.Application.Features.Hotels.Commands
{
    public class DeleteHotelContactCommand : IRequest
    {
        public Guid HotelId { get; set; }
        public Guid ContactId { get; set; }
    }
}
