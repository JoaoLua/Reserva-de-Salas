using Application.UseCases.BookingUseCases;
using Domain.DTOs;
using System.Security.Claims;

namespace reserva_salas.EndPoints
{
    public class BookingEndPoints
    {
        public static void MapBookingEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/bookings")
                           .WithTags("Reservas")
                           .RequireAuthorization(); 

            group.MapPost("/", async (
                CreateBookingRequest request,
                CreateBookingUseCase useCase,
                ClaimsPrincipal user) => 
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                try
                {
                    var booking = await useCase.Execute(request, userId);
                    return Results.Created($"/bookings/{booking.Id}", booking);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            group.MapGet("/{date}", async (DateTime date, GetBookingsByDateUseCase useCase) =>
            {
                var bookings = await useCase.ExecuteAsync(date);
                return Results.Ok(bookings);
            })
            .WithDescription("Retorna todas as reservas de uma data específica")
            .RequireAuthorization();
        }
    }
}
