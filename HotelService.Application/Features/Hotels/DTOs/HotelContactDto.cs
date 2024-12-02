using HotelService.Domain.Enums;

namespace HotelService.Application.Features.Hotels.Queries
{
    public class HotelContactDto
    {
        public ContactType Type { get; set; }
        public string ContactDetail { get; set; }
    }
}
