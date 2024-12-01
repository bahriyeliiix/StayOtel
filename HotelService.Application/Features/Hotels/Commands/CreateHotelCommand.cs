using MediatR;

namespace HotelService.Application.Features.Hotels.Commands
{
    public class CreateHotelCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
