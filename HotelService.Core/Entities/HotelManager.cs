using Shared.Entities;

namespace HotelService.Domain.Entities
{
    public class HotelManager : BaseEntity
    {
        public Guid HotelId { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Hotel Hotel { get; set; } 
    }
}
