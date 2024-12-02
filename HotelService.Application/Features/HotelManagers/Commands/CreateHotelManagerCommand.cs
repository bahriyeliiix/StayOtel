using HotelService.Domain.Entities;
using MediatR;

namespace HotelService.Application.Features.HotelManagers.Commands
{
    public class CreateHotelManagerCommand : IRequest<Guid>
    {
        public Guid HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
