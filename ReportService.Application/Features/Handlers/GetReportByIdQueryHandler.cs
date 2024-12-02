using MediatR;
using AutoMapper;
using Shared.Exceptions;
using ReportService.Infrastructure.Repositories;
using ReportService.Application.Features.Queries;
using ReportService.Application.Features.DTOs;

namespace ReportService.Application.Features.Handlers
{
    public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, ReportDto>
    {
        private readonly IReportRepository _ReportRepository;

        private readonly IMapper _mapper;


        public GetReportByIdQueryHandler(IMapper mapper, IReportRepository ReportRepository)
        {
            _mapper = mapper;
            _ReportRepository = ReportRepository;
        }

        public async Task<ReportDto> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
            var Report = await _ReportRepository.GetByIdAsync(request.Id);

            if (Report == null)
            {
                throw new NotFoundException("Report not found");
            }


            return _mapper.Map<ReportDto>(Report);

        }
               
    }
}
