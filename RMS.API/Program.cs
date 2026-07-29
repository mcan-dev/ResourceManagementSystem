using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;
using RMS.RepositoryLayer.Repositories;
using RMS.ServiceLayer;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using RMS.ServiceLayer.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITitleRepository, TitleRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
builder.Services.AddScoped<IProjectRoleRepository, ProjectRoleRepository>();
builder.Services.AddScoped<ILeaveStatusRepository, LeaveStatusRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<IEmployeePriorityRepository, EmployeePriorityRepository>();
builder.Services.AddScoped<ITaskAssignmentRepository, TaskAssignmentRepository>();
builder.Services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeCapacityRepository, EmployeeCapacityRepository>();
builder.Services.AddScoped<IWorkCalendarRepository, WorkCalendarRepository>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IEmployeeCapacityService, EmployeeCapacityService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddDbContext<RmsContext>(options =>


{
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
Console.WriteLine("Connection String:");
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

// Program.cs i�erisine ekle (builder.Build() sat�r�ndan �nce)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular'�n adresi
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();

app.MapControllers();
// app.Run() sat�r�ndan �nce middleware'i �a��r
app.UseCors("AllowAngular");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

