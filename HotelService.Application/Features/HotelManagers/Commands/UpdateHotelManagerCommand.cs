using HotelService.Application.Features.HotelManagers.DTOs;
using MediatR;

namespace HotelService.Application.Features.HotelManagers.Commands
{
    public class HotelManagerUpdateCommand : IRequest<HotelManagerDto>
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
