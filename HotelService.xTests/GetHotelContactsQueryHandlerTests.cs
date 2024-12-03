using Moq;
using HotelService.Infrastructure.Repositories;
using Shared.Exceptions;
using AutoMapper;
using HotelService.Domain.Entities;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Domain.Enums;

namespace HotelService.xTests
{
    public class GetHotelContactsQueryHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetHotelContactsQueryHandler _handler;

        public GetHotelContactsQueryHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetHotelContactsQueryHandler(_hotelRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnHotelContacts_WhenHotelExists()
        {
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel
            {
                Id = hotelId,
                Contacts = new List<HotelContact>
                {
                    new HotelContact { Id = Guid.NewGuid(), ContactType = Domain.Enums.ContactType.Phone, ContactDetail = "1234567890" },
                    new HotelContact { Id = Guid.NewGuid(), ContactType = Domain.Enums.ContactType.Email, ContactDetail = "example@example.com" }
                }
            };

            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync(hotel);

            _mapperMock.Setup(m => m.Map<List<HotelContactDto>>(It.IsAny<List<HotelContact>>()))
                .Returns(hotel.Contacts.Select(c => new HotelContactDto
                {
                    Type = c.ContactType,
                    ContactDetail = c.ContactDetail
                }).ToList());
            var result = await _handler.Handle(new GetHotelContactsQuery { HotelId = hotelId }, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(ContactType.Phone, result[0].Type);
            Assert.Equal("1234567890", result[0].ContactDetail);
            Assert.Equal(ContactType.Email, result[1].Type);
            Assert.Equal("example@example.com", result[1].ContactDetail);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenHotelDoesNotExist()
        {
            var hotelId = Guid.NewGuid();
            _hotelRepositoryMock.Setup(x => x.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetHotelContactsQuery { HotelId = hotelId }, CancellationToken.None));
            Assert.Equal("Hotel not found", exception.Message);
        }
    }
}
