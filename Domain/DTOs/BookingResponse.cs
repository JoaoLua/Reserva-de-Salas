using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class BookingResponse
    {
        public Guid Id { get; set; }
        public string Time { get; set; }       
        public string RoomName { get; set; }  
        public string ReservedBy { get; set; }
        public string? Reason { get; set; }      
    }
}
