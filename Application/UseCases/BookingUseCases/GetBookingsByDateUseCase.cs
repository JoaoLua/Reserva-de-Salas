using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.BookingUseCases
{
    public class GetBookingsByDateUseCase
    {
        private readonly IBookingRepository _repository;

        public GetBookingsByDateUseCase(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookingResponse>> ExecuteAsync(DateTime date)
        { 
            var bookings = await _repository.GetByDateAsync(date);

            return bookings.Select(b => new BookingResponse
            {
                Id = b.Id,
                Time = b.TimeSlot.ToString(@"hh\:mm"), 
                RoomName = b.Room.Name,
                ReservedBy = b.User.FullName, 
                Reason = b.Reason
            });
        }
    }
}
