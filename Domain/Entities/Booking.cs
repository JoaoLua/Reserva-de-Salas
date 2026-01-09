using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
