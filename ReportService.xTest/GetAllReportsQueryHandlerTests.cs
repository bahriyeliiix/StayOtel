using AutoMapper;
using Moq;
using ReportService.Application.Features.Handlers;
using ReportService.Application.Features.Queries;
using ReportService.Infrastructure.Repositories;
using Shared.Exceptions;

namespace HotelService.xTests
{
    public class GetAllReportsQueryHandlerTests
    {
        private readonly Mock<IReportRepository> _reportRepositoryMock;
        private readonly GetAllReportsQueryHandler _handler;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllReportsQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _reportRepositoryMock = new Mock<IReportRepository>();
            _handler = new GetAllReportsQueryHandler(_reportRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnReports_WhenReportsExist()
        {
            var reports = new List<ReportData>
            {
                new ReportData { Id = Guid.NewGuid(), Location = "loc 1", HotelCount = 4 },
                new ReportData { Id = Guid.NewGuid(), Location = "loc 2", HotelCount = 5 }
            };

            _reportRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(reports);

            var result = await _handler.Handle(new GetAllReportsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("loc 1", result.First().Location);
            Assert.Equal("loc 2", result.Last().Location);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoReportsExist()
        {
            _reportRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<ReportData>());

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetAllReportsQuery(), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenReportsExistButQueryIsEmpty()
        {
            _reportRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<ReportData>());

            var result = await _handler.Handle(new GetAllReportsQuery(), CancellationToken.None);

            Assert.Empty(result);
        }
    }
}
