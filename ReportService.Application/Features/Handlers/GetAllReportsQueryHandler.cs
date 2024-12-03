using MediatR;
using AutoMapper;
using Shared.Exceptions;
using Serilog;
using ReportService.Application.Features.DTOs;
using ReportService.Infrastructure.Repositories;
using ReportService.Application.Features.Queries;

namespace ReportService.Application.Features.Handlers
{
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, List<ReportDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetAllReportsQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<List<ReportDto>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Attempting to retrieve all reports");

                var reports = await _reportRepository.GetAllAsync();

                if (reports == null || !reports.Any())
                {
                    Log.Warning("No reports found");
                    throw new NotFoundException("No reports found");
                }

                Log.Information("Successfully retrieved {ReportCount} reports", reports.Count);

                return _mapper.Map<List<ReportDto>>(reports);
            }
            catch (NotFoundException ex)
            {
                Log.Error(ex, "No reports found");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while retrieving reports");
                throw new ApplicationException("An unexpected error occurred", ex);
            }
        }
    }
}
