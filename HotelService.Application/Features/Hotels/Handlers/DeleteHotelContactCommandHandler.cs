using HotelService.Infrastructure.Repositories;
using MediatR;
using Shared.Exceptions;

public class DeleteHotelContactCommand : IRequest
{
    public Guid HotelId { get; set; }
    public Guid ContactId { get; set; }
}

public class DeleteHotelContactCommandHandler : IRequestHandler<DeleteHotelContactCommand>
{
    private readonly IHotelRepository _hotelRepository;

    public DeleteHotelContactCommandHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task Handle(DeleteHotelContactCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel == null)
        {
            throw new NotFoundException("Hotel not found");
        }

        var contact = hotel.Contacts.FirstOrDefault(c => c.Id == request.ContactId && c.HotelId == request.HotelId);
        if (contact == null)
        {
            throw new NotFoundException("Hotel contact not found");
        }

        contact.IsDeleted = true;
        await _hotelRepository.UpdateHotelContactAsync(contact);

    }
}
