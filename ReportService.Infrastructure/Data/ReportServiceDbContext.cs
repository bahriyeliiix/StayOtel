using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Data
{
    public class ReportServiceDbContext : DbContext
    {
        public DbSet<ReportData> Reports { get; set; }

        public ReportServiceDbContext(DbContextOptions<ReportServiceDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.Location).IsRequired();
            });
        }
    }
}
