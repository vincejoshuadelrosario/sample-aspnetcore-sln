using sample_dotnet_6_0.Data;
using sample_dotnet_6_0.Models;
using sample_dotnet_6_0.Repositories;
using sample_dotnet_6_0.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IDataAccess<Employee>, EmployeesDataAccess>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
