using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Domain.Entities;

namespace HotelService.Application.Features.HotelManagers.DTOs
{
    public class HotelManagerDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

    }
}
