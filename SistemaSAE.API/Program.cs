using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SistemaSAE.API.Context;
using SistemaSAE.API.Interfaces;
using SistemaSAE.API.Repositories.Implementations;
using SistemaSAE.API.Repositories.Interfaces;
using SistemaSAE.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SistemaSAE.API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SAEContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

builder.Services.AddScoped<IVehicle, VehicleService>();
builder.Services.AddScoped<IPricing, PricingService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<IReport, ReportService>();

builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<IEstacionamientoRepository, EstacionamientoRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITarifaRepository, TarifaRepository>();
builder.Services.AddScoped<IRegistroPagoRepository, RegistroPagoRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaSAE.API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

