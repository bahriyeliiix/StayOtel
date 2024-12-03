using Moq;
using Xunit;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Application.Features.Hotels.Handlers;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using HotelService.Domain.Entities;

namespace HotelService.xTests
{
    public class DeleteHotelContactCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly DeleteHotelContactCommandHandler _handler;

        public DeleteHotelContactCommandHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new DeleteHotelContactCommandHandler(_hotelRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteHotelContact_WhenContactExists()
        {
            var hotelId = Guid.NewGuid();
            var contactId = Guid.NewGuid();
            var hotel = new Hotel
            {
                Id = hotelId,
                Contacts = new List<HotelContact> { new HotelContact { Id = contactId, HotelId = hotelId } }
            };
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(hotel);

            await _handler.Handle(new DeleteHotelContactCommand { HotelId = hotelId, ContactId = contactId }, CancellationToken.None);

            _hotelRepositoryMock.Verify(x => x.UpdateHotelContactAsync(It.IsAny<HotelContact>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelNotFound()
        {
            var hotelId = Guid.NewGuid();
            var contactId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteHotelContactCommand { HotelId = hotelId, ContactId = contactId }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenContactNotFound()
        {
            var hotelId = Guid.NewGuid();
            var contactId = Guid.NewGuid();
            var hotel = new Hotel { Id = hotelId, Contacts = new List<HotelContact>() };
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(hotel);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteHotelContactCommand { HotelId = hotelId, ContactId = contactId }, CancellationToken.None));
        }
    }
}
