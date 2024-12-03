using Moq;
using Xunit;
using HotelService.Application.Features.Hotels.Handlers;
using HotelService.Infrastructure.Repositories;
using HotelService.Application.Features.Hotels.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Domain.Entities;
using Shared.Exceptions;

namespace HotelService.xTests
{
    public class GetAllHotelsQueryHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly GetAllHotelsQueryHandler _handler;

        public GetAllHotelsQueryHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new GetAllHotelsQueryHandler(_hotelRepositoryMock.Object, Mock.Of<IMapper>());
        }

        [Fact]
        public async Task Handle_ShouldReturnHotels_WhenHotelsExist()
        {
            var hotels = new List<Hotel> { new Hotel { Id = Guid.NewGuid() } };
            _hotelRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(hotels);

            var result = await _handler.Handle(new GetAllHotelsQuery(), CancellationToken.None);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoHotelsExist()
        {
            _hotelRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Hotel>());

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetAllHotelsQuery(), CancellationToken.None));
        }
    }
}
