using MediatR;
using Microsoft.EntityFrameworkCore;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Infrastructure.Data;
using HotelService.Application.Features.Hotels.Queries.GetHotelById;
using HotelService.Domain.Entities;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using Serilog;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDetailDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelByIdQueryHandler(IMapper mapper, IHotelRepository hotelRepository)
        {
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        public async Task<HotelDetailDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Fetching hotel with ID: {HotelId}", request.Id);

            var hotel = await _hotelRepository.GetByIdAsync(request.Id);

            if (hotel == null)
            {
                Log.Warning("Hotel with ID {HotelId} not found", request.Id);
                throw new NotFoundException("Hotel not found");
            }

            Log.Information("Hotel with ID {HotelId} found, mapping to DTO", request.Id);

            return _mapper.Map<HotelDetailDto>(hotel);
        }
    }
}
