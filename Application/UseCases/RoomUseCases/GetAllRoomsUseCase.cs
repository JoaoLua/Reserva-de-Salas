using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RoomUseCases
{
    public class GetAllRoomsUseCase
    {
        private readonly IRoomRepository _repository;
        public GetAllRoomsUseCase(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomResponse>> ExecuteAsync()
        {
            var rooms = await _repository.GetAllAsync();

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
