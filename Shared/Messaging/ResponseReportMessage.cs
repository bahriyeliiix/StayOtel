using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public class ResponseReportMessage
    {
        public Guid ReportId { get; set; }
        public DateTime RequestedAt { get; set; }
        public int Status { get; set; }
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
