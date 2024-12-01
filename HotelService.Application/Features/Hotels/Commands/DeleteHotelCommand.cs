using MediatR;

namespace HotelService.Application.Features.Hotels.Commands
{
    public class DeleteHotelCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
