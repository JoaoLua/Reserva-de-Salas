using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RoomUseCases
{
    public class CreateRoomUseCase
    {
        private readonly IRoomRepository _repository;
        public CreateRoomUseCase(IRoomRepository repository)
        {
            _repository = repository;
        }
        public async Task<Room> Execute(CreateRoomRequest request)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Capacity = request.Capacity,
                Type = request.Type,
                Floor = request.Floor,
                IsActive = true
            };
            return await _repository.AddAsync(room);
        }
    }
}
