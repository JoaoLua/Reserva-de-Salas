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
                    var errorMessage = ex.InnerException?.Message ?? ex.Message;
                    Console.WriteLine($"ERRO AO SALVAR: {errorMessage}");
                    return Results.BadRequest(new { message = errorMessage });
                }
            });

            group.MapGet("/{date}", async (DateTime date, GetBookingsByDateUseCase useCase) =>
            {
                var bookings = await useCase.ExecuteAsync(date);
                return Results.Ok(bookings);
            })
            .WithDescription("Retorna todas as reservas de uma data específica")
            .RequireAuthorization();

            group.MapDelete("/{id}", async (Guid id, ClaimsPrincipal user, CancelBookingUseCase useCase) =>
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                try
                {
                    await useCase.ExecuteAsync(id, userId);
                    return Results.NoContent(); 
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithDescription("Cancela uma reserva existente")
            .RequireAuthorization();

        }
    }
}
