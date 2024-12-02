using ReportService.Domain.Enums;

namespace ReportService.Application.Features.DTOs
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; }
        public ReportStatus Status { get; set; }
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
