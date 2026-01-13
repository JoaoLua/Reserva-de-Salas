using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> AddAsync(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetByDateAsync(DateTime date);
        Task DeleteAsync(Booking booking);
        Task<bool> HasConflictingBooking(Guid roomId, DateTime date, TimeSpan timeSlot);
    }
}

