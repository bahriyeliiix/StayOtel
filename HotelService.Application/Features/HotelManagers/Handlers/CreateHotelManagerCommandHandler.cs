using MediatR;
using HotelService.Domain.Entities;
using AutoMapper;
using HotelService.Infrastructure.Repositories;
using HotelService.Application.Features.HotelManagers.Commands;
using Serilog;

namespace HotelService.Application.Features.HotelManagers.Handlers
{
    public class CreateHotelManagerCommandHandler : IRequestHandler<CreateHotelManagerCommand, Guid>
    {
        private readonly IHotelManagerRepository _hotelManagerRepository;
        private readonly IMapper _mapper;

        public CreateHotelManagerCommandHandler(IHotelManagerRepository hotelManagerRepository, IMapper mapper)
        {
            _hotelManagerRepository = hotelManagerRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateHotelManagerCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Creating hotel manager for HotelId: {HotelId}", request.HotelId);

            try
            {
                var hotelManager = _mapper.Map<HotelManager>(request);

                await _hotelManagerRepository.AddAsync(hotelManager);

                Log.Information("Hotel manager created successfully with ID: {ManagerId}", hotelManager.Id);
                return hotelManager.Id;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while creating hotel manager for HotelId: {HotelId}", request.HotelId);
                throw; 
            }
        }
    }
}
