using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.BookingUseCases
{
    public class CreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        public CreateBookingUseCase(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Booking> Execute(CreateBookingRequest request, string userId)
        {
            if (!await _roomRepository.RoomExistsAsync(request.RoomId))
            {
                throw new Exception("Room does not exist.");
            }
            if (!await _roomRepository.IsAvailable(request.RoomId, request.Date, request.TimeSlot))
            {
                throw new Exception("Room is not available for the selected date and time slot.");
            }
            bool isBusy = await _bookingRepository.HasConflictingBooking(request.RoomId, request.Date, request.TimeSlot);
            if (isBusy)
            {
                throw new Exception("There is already a booking for this room at the selected date and time slot.");
            }
            var booking = new Booking
            {
                RoomId = request.RoomId,
                Date = request.Date,
                TimeSlot = request.TimeSlot,
                UserId = userId,
                Reason = request.Reason
            };
            await _bookingRepository.AddAsync(booking);
            return booking;
        }
    }
}
