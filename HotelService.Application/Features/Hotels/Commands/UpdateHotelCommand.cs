using HotelService.Application.Features.Hotels.DTOs;
using MediatR;

namespace HotelService.Application.Features.Hotels.Commands
{
    public class UpdateHotelCommand : IRequest<HotelDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
