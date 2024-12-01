using MediatR;
using Microsoft.EntityFrameworkCore;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Infrastructure.Data;
using HotelService.Application.Features.Hotels.Queries.GetHotelById;

namespace HotelService.Application.Features.Hotels.Handlers
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDto>
    {
        private readonly HotelServiceDbContext _context;

        public GetHotelByIdQueryHandler(HotelServiceDbContext context)
        {
            _context = context;
        }

        public async Task<HotelDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (hotel == null)
                return null;

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country
            };
        }
    }
}
