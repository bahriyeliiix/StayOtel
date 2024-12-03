using MediatR;
using HotelService.Domain.Entities;
using HotelService.Application.Features.HotelManagers.Queries;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Infrastructure.Repositories;
using AutoMapper;
using Serilog;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class GetHotelManagerByHotelIdQueryHandler : IRequestHandler<GetHotelManagerByHotelIdQuery, HotelManagerDto>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;

        public GetHotelManagerByHotelIdQueryHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<HotelManagerDto> Handle(GetHotelManagerByHotelIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Fetching hotel manager for HotelId: {HotelId}", request.HotelId);

            try
            {
                
                var manager = await _hotelManagerRepository.GetAllByHotelIdAsync(request.HotelId);

                if (manager == null)
                {
                    Log.Warning("No hotel manager found for HotelId: {HotelId}", request.HotelId);
                    return null;
                }

                Log.Information("Hotel manager fetched successfully for HotelId: {HotelId}", request.HotelId);
                return _mapper.Map<HotelManagerDto>(manager);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching hotel manager for HotelId: {HotelId}", request.HotelId);
                throw; 
            }
        }
    }
}
