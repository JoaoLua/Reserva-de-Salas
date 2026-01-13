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
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _context.Bookings.FindAsync(id);
        }
        public async Task<IEnumerable<Booking>> GetByDateAsync(DateTime date)
        {
            return await _context.Bookings
                .Include(b => b.Room) 
                .Include(b => b.User) 
                .Where(b => b.Date.Date == date.Date)
                .OrderBy(b => b.TimeSlot)
                .ToListAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> HasConflictingBooking(Guid roomId, DateTime date, TimeSpan timeSlot)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                b.Date.Date == date.Date &&
                b.TimeSlot == timeSlot);
        }
    }
}
