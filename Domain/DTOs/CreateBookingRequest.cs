using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateBookingRequest
    {
        public Guid RoomId { get; set; }
        public DateTime Date { get; set; } 
        public TimeSpan TimeSlot { get; set; }
        public string? Reason { get; set; }
    }
}
