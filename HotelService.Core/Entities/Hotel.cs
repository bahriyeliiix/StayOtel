using Shared.Entities;

namespace HotelService.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<HotelContact> Contacts { get; set; }
        public ICollection<HotelManager> Managers { get; set; }
    }
}
