using HotelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data
{
    public class HotelServiceDbContext : DbContext
    {
        public HotelServiceDbContext(DbContextOptions<HotelServiceDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelManager> HotelManagers { get; set; }
        public DbSet<HotelContact> HotelContacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<HotelManager>()
                .HasOne(hm => hm.Hotel)
                .WithMany(h => h.Managers)
                .HasForeignKey(hm => hm.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HotelContact>()
                .HasOne(hc => hc.Hotel)
                .WithMany(h => h.Contacts)
                .HasForeignKey(hc => hc.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .Property(h => h.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<HotelManager>()
                .Property(hm => hm.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<HotelContact>()
                .Property(hc => hc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<HotelContact>()
                .Property(hc => hc.ContactDetail)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<HotelManager>()
                .Property(hm => hm.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<HotelManager>()
                .Property(hm => hm.LastName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<HotelManager>()
                .Property(hm => hm.Email)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<HotelManager>()
                .Property(hm => hm.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
