using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace reserva_salas.EndPoints
{
    public static class UsersEndPoints
    {
        public static void MapUsersEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/users")
                            .WithTags("Usuarios")
                            .WithDescription("User management endpoints");
            group.MapPost("/register", async (
            [FromBody] RegisterRequestDto registration,
            UserManager<ApplicationUser> userManager) =>
            {
                if (registration == null) return Results.BadRequest("Dados inválidos.");

                var user = new ApplicationUser
                {
                    UserName = registration.Email,
                    Email = registration.Email,
                    FullName = registration.FullName // Propriedade customizada
                };

                var result = await userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    return Results.Created($"/users/{user.Id}", new { message = "Usuário criado com sucesso!" });
                }

                // Retorna os erros do Identity (senha fraca, email duplicado, etc)
                return Results.BadRequest(result.Errors);
            });
        }
    }
}
