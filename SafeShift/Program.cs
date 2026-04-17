using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SafeShift.BLL.Services;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.DAL.Data;
using SafeShift.DAL.Repositories;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SafeShiftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SafeShift API",
        Version = "v1",
        Description = "Workplace Safety and Incident Management Web API for authentication, incidents, inspections, shifts, and users."
    });

    var apiXmlPath = Path.Combine(AppContext.BaseDirectory, "SafeShift.xml");
    if (File.Exists(apiXmlPath))
    {
        options.IncludeXmlComments(apiXmlPath);
    }

    var modelsXmlPath = Path.Combine(AppContext.BaseDirectory, "SafeShift.Models.xml");
    if (File.Exists(modelsXmlPath))
    {
        options.IncludeXmlComments(modelsXmlPath);
    }

    options.OperationFilter<AuthExamplesOperationFilter>();
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<IInspectionService, InspectionService>();
builder.Services.AddScoped<IShiftService, ShiftService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
