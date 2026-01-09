using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> AddAsync(Room room);
        Task<Room> GetByIdAsync(Guid id);
        Task<IEnumerable<Room>> GetAllAsync();
        Task UpdateAsync(Room room);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime date, TimeSpan timeSlot);
        Task<bool> IsAvailable(Guid roomId, DateTime date, TimeSpan timeSlot);
        Task<bool> RoomExistsAsync(Guid id);
    }
}
