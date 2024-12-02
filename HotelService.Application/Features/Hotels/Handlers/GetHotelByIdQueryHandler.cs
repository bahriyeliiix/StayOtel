using MediatR;
using Microsoft.EntityFrameworkCore;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Infrastructure.Data;
using HotelService.Application.Features.Hotels.Queries.GetHotelById;
using HotelService.Domain.Entities;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDetailDto>
    {
        private readonly IHotelRepository _hotelRepository;

        private readonly IMapper _mapper;


        public GetHotelByIdQueryHandler( IMapper mapper, IHotelRepository hotelRepository)
        {
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        public async Task<HotelDetailDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(request.Id);

            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }


            return _mapper.Map<HotelDetailDto>(hotel);
         
        }
    }
}
