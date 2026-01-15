using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.BookingUseCases
{
    public class CancelBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        public CancelBookingUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task ExecuteAsync(Guid bookingId, string userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new Exception("Reserva não encontrada");
            }
            if (booking.UserId != userId)
            {
                throw new Exception("Você só pode cancelar suas próprias reservas.");
            }
            await _bookingRepository.DeleteAsync(booking);
        }
    }
}
