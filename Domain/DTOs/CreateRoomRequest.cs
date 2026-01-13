using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateRoomRequest
    {
        public string Name { get; init; }
        public int Capacity { get; init; }
        public string Type { get; init; }
        public string Floor { get; init; }
    }
}
