namespace HotelService.Tests.Handlers
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
            // Arrange
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(new Hotel { Id = hotelId });

            // Act
            await _handler.Handle(new DeleteHotelCommand { Id = hotelId }, CancellationToken.None);

            // Assert
            _hotelRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Hotel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteHotelCommand { Id = hotelId }, CancellationToken.None));
        }
    }
}
