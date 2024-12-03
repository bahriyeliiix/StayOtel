using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportService.Infrastructure.Data;

namespace ReportService.Infrastructure.Repositories
{
    public interface IReportRepository
    {
        Task<ReportData> AddAsync(ReportData report);
        Task<ReportData?> GetByIdAsync(Guid id);
        Task<List<ReportData>> GetAllAsync();

        Task<bool> UpdateAsync(ReportData report);
    }
    public class ReportRepository : IReportRepository
    {
        private readonly ReportServiceDbContext _context;

        public ReportRepository(ReportServiceDbContext context)
        {
            _context = context;
        }

        public async Task<ReportData> AddAsync(ReportData report)
        {
            var localDateTime = DateTime.Now;
            var utcDateTime = localDateTime.ToUniversalTime();

            report.CreatedAt = utcDateTime;
            report.RequestedAt = utcDateTime;
            await _context.Reports.AddAsync(report);
            _context.SaveChanges();

            return report;
        }

        public async Task<ReportData?> GetByIdAsync(Guid id)
        {
            return await _context.Reports
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<ReportData>> GetAllAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<bool> UpdateAsync(ReportData report)
        {

            var localDateTime = DateTime.Now;
            var utcDateTime = localDateTime.ToUniversalTime();

            var already = _context.Reports.FirstOrDefault(a => a.Id == report.Id);

            already.PhoneCount = report.PhoneCount;
            already.Status = Domain.Enums.ReportStatus.Completed;
            already.HotelCount = report.HotelCount;
            already.UpdatedAt = utcDateTime;


            _context.Reports.Update(already);
            _context.SaveChanges();

            return true;

        }
    }
}
