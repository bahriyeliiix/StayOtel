using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Application.Features.Hotels.Queries;

namespace HotelService.Application.Features.Hotels.DTOs
{
    public class HotelDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<HotelManagerDto> Managers { get; set; }
        public List<HotelContactDto> Contacts { get; set; }

    }
}
