using AutoMapper;
using MediatR;
using ReportService.Application.Features.Commands;
using ReportService.Application.Features.DTOs;
using ReportService.Infrastructure.Data;
using ReportService.Infrastructure.Repositories;

namespace ReportService.Application.Features.CreateReport
{
    public class CreateReportHandler : IRequestHandler<CreateReportCommand, Guid>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public CreateReportHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var report = _mapper.Map<ReportData>(request);

            await _reportRepository.AddAsync(report);

            return report.Id;
        }
    }
}
