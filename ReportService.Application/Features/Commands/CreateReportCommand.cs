using MediatR;

namespace ReportService.Application.Features.Commands
{
    public class CreateReportCommand : IRequest<Guid>
    {
        public string Location { get; set; }
    }
}
