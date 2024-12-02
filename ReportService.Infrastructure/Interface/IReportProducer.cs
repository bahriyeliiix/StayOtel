using Shared.Messaging;

namespace ReportService.Application.Interfaces
{
    public interface IReportProducer
    {
        Task PublishReportRequestAsync(CreateReportMessage message);
    }
}
