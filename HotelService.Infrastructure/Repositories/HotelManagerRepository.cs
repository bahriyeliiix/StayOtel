using HotelService.Domain.Entities;
using HotelService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories
{
    public interface IHotelManagerRepository
    {
        Task<HotelManager> GetByIdAsync(Guid id);
        Task<List<HotelManager>> GetAllByHotelIdAsync(Guid hotelId);
        Task AddAsync(HotelManager hotelManager);
        Task UpdateAsync(HotelManager hotelManager);
        Task DeleteAsync(Guid id);
    }


    public class HotelManagerRepository : IHotelManagerRepository
    {
        private readonly HotelServiceDbContext _context;

        public HotelManagerRepository(HotelServiceDbContext context)
        {
            _context = context;
        }

        public async Task<HotelManager> GetByIdAsync(Guid id)
        {
            return await _context.HotelManagers.FindAsync(id);
        }

        public async Task<List<HotelManager>> GetAllByHotelIdAsync(Guid hotelId)
        {
            return await _context.HotelManagers
                                 .Where(manager => manager.HotelId == hotelId && manager.IsDeleted == false)
                                 .ToListAsync();
        }

        public async Task AddAsync(HotelManager hotelManager)
        {
            await _context.HotelManagers.AddAsync(hotelManager);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HotelManager hotelManager)
        {
            _context.HotelManagers.Update(hotelManager);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var hotelManager = await _context.HotelManagers.FindAsync(id);
            if (hotelManager != null)
            {
                _context.HotelManagers.Remove(hotelManager);
                await _context.SaveChangesAsync();
            }
        }
    }
}
