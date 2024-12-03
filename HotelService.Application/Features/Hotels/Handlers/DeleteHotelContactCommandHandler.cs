using HotelService.Infrastructure.Repositories;
using MediatR;
using Shared.Exceptions;
using Serilog;

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
        Log.Information("Attempting to delete contact with ID: {ContactId} for hotel with ID: {HotelId}", request.ContactId, request.HotelId);

        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel == null)
        {
            Log.Warning("Hotel with ID {HotelId} not found", request.HotelId);
            throw new NotFoundException("Hotel not found");
        }

        var contact = hotel.Contacts.FirstOrDefault(c => c.Id == request.ContactId && c.HotelId == request.HotelId);
        if (contact == null)
        {
            Log.Warning("Contact with ID {ContactId} not found for hotel with ID {HotelId}", request.ContactId, request.HotelId);
            throw new NotFoundException("Hotel contact not found");
        }

        contact.IsDeleted = true;
        await _hotelRepository.UpdateHotelContactAsync(contact);

        Log.Information("Contact with ID {ContactId} marked as deleted for hotel with ID {HotelId}", request.ContactId, request.HotelId);
    }
}
