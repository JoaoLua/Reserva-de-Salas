using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RoomUseCases
{
    public class GetAvailableRoomsUseCase
    {
        private readonly IRoomRepository _repository;

        public GetAvailableRoomsUseCase(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomResponse>> ExecuteAsync(DateTime date, TimeSpan timeSlot)
        {
            var rooms = await _repository.GetAvailableRoomsAsync(date, timeSlot);
            return rooms.Select(r => new RoomResponse
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                Type = r.Type,
                Floor = r.Floor
            });
        }
    }
}
