using Application.UseCases.BookingUseCases;
using Application.UseCases.RoomUseCases;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reserva_salas.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


//rooms
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<CreateRoomUseCase>();
builder.Services.AddScoped<GetAvailableRoomsUseCase>();

//bookings
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<CreateBookingUseCase>();
builder.Services.AddScoped<GetBookingsByDateUseCase>();

var app = builder.Build();

app.MapGroup("/auth").MapIdentityApi<ApplicationUser>();
UsersEndPoints.MapUsersEndPoints(app);
RoomEndPoints.MapRoomEndPoints(app);
BookingEndPoints.MapBookingEndPoints(app);


app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

