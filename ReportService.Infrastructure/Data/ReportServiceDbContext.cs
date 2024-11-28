using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Data
{
    public class ReportServiceDbContext : DbContext
    {
        public ReportServiceDbContext(DbContextOptions<ReportServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
