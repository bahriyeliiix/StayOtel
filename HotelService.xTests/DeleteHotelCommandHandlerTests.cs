using HotelService.Application.Features.Hotels.Commands;
using HotelService.Application.Features.Hotels.Handlers;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Repositories;
using Moq;
using Shared.Exceptions;

namespace HotelService.xTests
{
    public class DeleteHotelCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly DeleteHotelCommandHandler _handler;

        public DeleteHotelCommandHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new DeleteHotelCommandHandler(_hotelRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteHotel_WhenHotelExists()
        {
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(new Hotel { Id = hotelId });

            await _handler.Handle(new DeleteHotelCommand { Id = hotelId }, CancellationToken.None);

            _hotelRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Hotel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist()
        {
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteHotelCommand { Id = hotelId }, CancellationToken.None));
        }
    }
}
