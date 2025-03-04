using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Models;
using SistemaSAE.API.Enums;

namespace SistemaSAE.API.Context
{
    public class SAEContext : DbContext
    {
        public SAEContext(DbContextOptions<SAEContext> options) : base(options) { }

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Tarifa> Tarifa { get; set; }
        public DbSet<Estacionamiento> Estacionamiento { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<RegistroPago> RegistroPago { get; set; }
        public DbSet<Reporte> Reporte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(TipoVehiculo))
                .ToList()
                .ForEach(prop => prop.SetColumnType("nvarchar(max)"));
        }


    }
}
