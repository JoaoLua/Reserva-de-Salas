using Application.UseCases.RoomUseCases;
using Domain.DTOs;


namespace reserva_salas.EndPoints
{
    public static class RoomEndPoints
    {
        public static void MapRoomEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/rooms")
                            .WithTags("Salas")
                            .WithDescription("Room management endpoints");

            group.MapGet("/", async (GetAllRoomsUseCase useCase) =>
            {
                var rooms = await useCase.ExecuteAsync();
                return Results.Ok(rooms);
            })
            .WithDescription("Lista todas as salas cadastradas no sistema")
            .RequireAuthorization();
            group.MapPost("/", async (CreateRoomRequest request, CreateRoomUseCase createRoomUseCase) =>
            {
                if (request == null)
                {
                    return Results.BadRequest("Invalid room data.");
                }
                var room = await createRoomUseCase.Execute(request);
                return Results.Created($"/rooms/{room.Id}", room);
            });

            group.MapGet("/available", async (
                DateTime date,
                string slot,
                GetAvailableRoomsUseCase useCase) =>
            {
                if (!TimeSpan.TryParse(slot, out var timeSlot))
                    return Results.BadRequest("Formato de horário inválido. Use HH:mm.");

                var availableRooms = await useCase.ExecuteAsync(date, timeSlot);
                return Results.Ok(availableRooms);
            })
            .RequireAuthorization();
        }
    }
}
