using Moq;
using HotelService.Application.Features.Hotels.Handlers;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using AutoMapper;
using HotelService.Application.Features.Hotels.Queries.GetHotelById;
using HotelService.Domain.Entities;
using Xunit;
using HotelService.Application.Features.Hotels.DTOs;

namespace HotelService.xTests
{
    public class GetHotelByIdQueryHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetHotelByIdQueryHandler _handler;

        public GetHotelByIdQueryHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetHotelByIdQueryHandler(_mapperMock.Object, _hotelRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnHotel_WhenHotelExists()
        {
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel { Id = hotelId, Name = "Test Hotel" };
            var hotelDto = new HotelDetailDto { Id = hotelId, Name = "Test Hotel" }; // HotelDto'ya dönüştürülmesi beklenen sonuç
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(hotel);

            _mapperMock.Setup(m => m.Map<HotelDetailDto>(It.IsAny<Hotel>()))
                .Returns(hotelDto);

            var result = await _handler.Handle(new GetHotelByIdQuery(hotelId), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(hotelId, result.Id);
            Assert.Equal("Test Hotel", result.Name);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist()
        {
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetHotelByIdQuery(hotelId), CancellationToken.None));
            Assert.Equal("Hotel not found", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist_AndHotelIsNull()
        {
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId)).ReturnsAsync((Hotel)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetHotelByIdQuery(hotelId), CancellationToken.None));
            Assert.Equal("Hotel not found", exception.Message);
        }
    }
}
