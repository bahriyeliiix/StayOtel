namespace Shared.Messaging
{
    public class CreateReportMessage
    {
        public Guid ReportId { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Location { get; set; }
    }
}
