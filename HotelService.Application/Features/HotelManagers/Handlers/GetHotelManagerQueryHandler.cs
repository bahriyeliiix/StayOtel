using MediatR;
using HotelService.Domain.Entities;
using HotelService.Application.Features.HotelManagers.Queries;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Infrastructure.Repositories;
using AutoMapper;
using Serilog;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class GetHotelManagerQueryHandler : IRequestHandler<GetHotelManagerQuery, HotelManagerDto>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;

        public GetHotelManagerQueryHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<HotelManagerDto> Handle(GetHotelManagerQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Fetching hotel manager for Id: {ManagerId}", request.Id);

            try
            {
                
                var manager = await _hotelManagerRepository.GetByIdAsync(request.Id);

                if (manager == null)
                {
                    Log.Warning("No hotel manager found with Id: {ManagerId}", request.Id);
                    return null;
                }

                Log.Information("Hotel manager fetched successfully for Id: {ManagerId}", request.Id);
                return _mapper.Map<HotelManagerDto>(manager);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching hotel manager for Id: {ManagerId}", request.Id);
                throw;
            }
        }
    }
}
