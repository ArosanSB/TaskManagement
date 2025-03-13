using Domain.Interfaces;
using Application;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Setting DB 
builder.Services.AddDbContext<BusinessLogicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Interfaces
builder.Services.AddScoped<ITaskReposistory, TaskReposistory>();

// Services
builder.Services.AddApplicationServices();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging(); // Log all requests

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
