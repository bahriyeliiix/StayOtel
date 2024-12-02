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


        Task<HotelContact> GetHotelContactByIdAsync(Guid contactId);
        Task AddHotelContactAsync(HotelContact contact);
        Task UpdateHotelContactAsync(HotelContact hotel);
        Task DeleteHotelContactAsync(Guid contactId);
        Task<List<HotelContact>> GetHotelContactsAsync(Guid hotelId);
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
                                 .Where(a => a.IsDeleted == false)
                                 .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                                 .Include(h => h.Contacts)
                                 .Include(h => h.Managers)
                                 .Where(a => a.IsDeleted == false)
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


        public async Task<HotelContact> GetHotelContactByIdAsync(Guid contactId)
        {
            return await _context.HotelContacts.FindAsync(contactId);
        }

        public async Task AddHotelContactAsync(HotelContact contact)
        {
            await _context.HotelContacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHotelContactAsync(HotelContact hotel)
        {
            _context.HotelContacts.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelContactAsync(Guid contactId)
        {
            var contact = await _context.HotelContacts.FindAsync(contactId);
            if (contact != null)
            {
                _context.HotelContacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HotelContact>> GetHotelContactsAsync(Guid hotelId)
        {
            return await _context.HotelContacts
                                 .Where(c => c.HotelId == hotelId && c.IsDeleted == false)
                                 .ToListAsync();
        }
    }
}
