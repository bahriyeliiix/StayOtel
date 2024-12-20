﻿using HotelService.Domain.Enums;
using Shared.Entities;

namespace HotelService.Domain.Entities
{
    public class HotelContact : BaseEntity
    {
        public Guid HotelId { get; set; }
        public ContactType ContactType { get; set; }
        public string ContactDetail { get; set; }
        public Hotel Hotel { get; set; }

    }
}
