using MediatR;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using AutoMapper;

namespace HotelService.Application.Features.Hotels.Queries
{
    public class GetHotelContactsQueryHandler : IRequestHandler<GetHotelContactsQuery, List<HotelContactDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;


        public GetHotelContactsQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<List<HotelContactDto>> Handle(GetHotelContactsQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var hotelContacts = hotel.Contacts.Select(contact => new HotelContactDto
            {
                Type = contact.ContactType,
                ContactDetail = contact.ContactDetail
            }).ToList();

            return hotelContacts;
        }
    }
}
