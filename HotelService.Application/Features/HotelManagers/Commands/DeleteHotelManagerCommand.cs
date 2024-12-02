using MediatR;
using System;

namespace HotelService.Application.Features.HotelManagers.Commands
{
    public class DeleteHotelManagerCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
    }
}
