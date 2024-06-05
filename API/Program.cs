using GamedreamAPI.Business;
using GamedreamAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVideogameService, VideogameService>();
builder.Services.AddScoped<IUserRepository, UserEFRepository>();
builder.Services.AddScoped<IVideogameRepository, VideogameEFRepository>();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ServerDB_dockernet");

builder.Services.AddDbContext<GamedreamContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:5076") // Reemplaza con la URL permitida
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();