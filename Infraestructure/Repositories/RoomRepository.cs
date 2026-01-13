using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Room> AddAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task<Room> GetByIdAsync(Guid id)
        {
            return await _context.Rooms.FindAsync(id);
        }
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var room = await GetByIdAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RoomExistsAsync(Guid id)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == id);
        }
        public async Task<bool> IsAvailable(Guid roomId, DateTime date, TimeSpan timeSlot)
        {
            bool exists = await _context.Bookings
                .AnyAsync(b => b.RoomId == roomId
                            && b.Date.Date == date.Date
                            && b.TimeSlot == timeSlot);

            return !exists;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime date, TimeSpan timeSlot)
        {
            return await _context.Rooms
                .Where(room => room.IsActive) 
                .Where(room => !_context.Bookings.Any(booking =>
                    booking.RoomId == room.Id &&
                    booking.Date.Date == date.Date &&
                    booking.TimeSlot == timeSlot))
                .ToListAsync();
        }


    }
}
