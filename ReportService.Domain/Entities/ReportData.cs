using ReportService.Domain.Enums;
using Shared.Entities;

public class ReportData : BaseEntity
{
    public DateTime RequestedAt { get; set; }
    public ReportStatus Status { get; set; }
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneCount { get; set; }
}
