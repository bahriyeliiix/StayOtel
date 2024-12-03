using Moq;
using HotelService.Application.Features.Hotels.Handlers;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Domain.Entities;
using Xunit;
using AutoMapper;

namespace HotelService.xTests
{
    public class UpdateHotelCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly UpdateHotelCommandHandler _handler;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateHotelCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new UpdateHotelCommandHandler(_hotelRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateHotel_WhenHotelExists()
        {
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel
            {
                Id = hotelId,
                Name = "Old Hotel Name",
                Address = "Old Address"
            };
            var updateCommand = new UpdateHotelCommand { Id = hotelId, Name = "New Hotel Name", Address = "New Address" };

            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(hotel);
            _hotelRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Hotel>()))
                .Returns(Task.CompletedTask);

            await _handler.Handle(updateCommand, CancellationToken.None);

            Assert.Equal("New Hotel Name", hotel.Name);
            Assert.Equal("New Address", hotel.Address);
            _hotelRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Hotel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist()
        {
            var hotelId = Guid.NewGuid();
            var updateCommand = new UpdateHotelCommand { Id = hotelId, Name = "New Hotel Name", Address = "New Address" };

            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(updateCommand, CancellationToken.None));
            Assert.Equal("Hotel not found", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowInvalidOperationException_WhenHotelDataIsInvalid()
        {
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel
            {
                Id = hotelId,
                Name = "Old Hotel Name",
                Address = "Old Address"
            };
            var updateCommand = new UpdateHotelCommand { Id = hotelId, Name = "", Address = "" };

            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(hotel);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(updateCommand, CancellationToken.None));
            Assert.Equal("Hotel data is invalid", exception.Message);
        }
    }
}
