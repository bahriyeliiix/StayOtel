using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelService.Application.Features.HotelManagers.DTOs;
using MediatR;

namespace HotelService.Application.Features.HotelManagers.Queries
{
    public class GetHotelManagerByHotelIdQuery : IRequest<HotelManagerDto>
    {
        public Guid HotelId { get; set; }
    }
}
