using MediatR;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using AutoMapper;
using Serilog;

namespace HotelService.Application.Features.Hotels.Queries
{
    public class GetHotelContactsQueryHandler : IRequestHandler<GetHotelContactsQuery, List<HotelContactDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelContactsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelContactDto>> Handle(GetHotelContactsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching hotel contacts for Hotel ID: {HotelId}", request.HotelId);

                var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    Log.Warning("Hotel with ID {HotelId} not found", request.HotelId);
                    throw new NotFoundException("Hotel not found");
                }

                var hotelContacts = hotel.Contacts.Select(contact => new HotelContactDto
                {
                    Type = contact.ContactType,
                    ContactDetail = contact.ContactDetail
                }).ToList();

                Log.Information("Successfully retrieved contacts for Hotel ID: {HotelId}", request.HotelId);
                return hotelContacts;
            }
            catch (NotFoundException ex)
            {
                Log.Error(ex, "Error retrieving hotel contacts for Hotel ID: {HotelId}", request.HotelId);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while fetching hotel contacts for Hotel ID: {HotelId}", request.HotelId);
                throw new ApplicationException("An unexpected error occurred", ex);
            }
        }
    }
}
