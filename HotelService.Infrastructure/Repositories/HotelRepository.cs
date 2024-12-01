using HotelService.Domain.Entities;
using HotelService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetByIdAsync(Guid id);
        Task<List<Hotel>> GetAllAsync();
        Task AddAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(Hotel hotel);
    }

    public class HotelRepository : IHotelRepository
    {
        private readonly HotelServiceDbContext _context;

        public HotelRepository(HotelServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> GetByIdAsync(Guid id)
        {
            return await _context.Hotels
                                 .Include(h => h.Contacts)
                                 .Include(h => h.Managers)
                                 .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                                 .Include(h => h.Contacts)
                                 .Include(h => h.Managers)
                                 .ToListAsync();
        }

        public async Task AddAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Hotel hotel)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }
}
