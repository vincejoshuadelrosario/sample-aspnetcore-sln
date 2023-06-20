using sample_dotnet_6_0.Data;
using sample_dotnet_6_0.Models;
using sample_dotnet_6_0.Repositories;
using sample_dotnet_6_0.Repositories.Cache;
using sample_dotnet_6_0.Services;
using sample_dotnet_6_0.Services.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IDataAccess<Employee>, EmployeesDataAccess>();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddSingleton<ICacheRepository<Employee>, EmployeesCacheRepository>();
builder.Services.AddSingleton<IEmployeesService, EmployeesService>();
builder.Services.AddSingleton<IEmployeesCacheService, EmployeesCacheService>();
builder.Services.AddMemoryCache();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// app.UsePathBase(new PathString("/api/v1"));
// app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
