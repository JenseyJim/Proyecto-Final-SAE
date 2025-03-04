using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.Enums;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Repositories.Implementations
{
    public class EstacionamientoRepository : IEstacionamientoRepository
    {
        private readonly SAEContext _context;

        public EstacionamientoRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estacionamiento>> ObtenerTodos()
        {
            return await _context.Estacionamiento.ToListAsync();
        }

        public async Task<Estacionamiento> ObtenerPorTipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            return await _context.Estacionamiento
                .FirstOrDefaultAsync(e => e.TipoVehiculo == tipoVehiculo);
        }

        public async Task Actualizar(Estacionamiento estacionamiento)
        {
            _context.Estacionamiento.Update(estacionamiento);
            await _context.SaveChangesAsync();
        }

        public async Task Crear(Estacionamiento estacionamiento)
        {
            _context.Estacionamiento.Add(estacionamiento);
            await _context.SaveChangesAsync();
        }

        public async Task LiberarEspacio(string tipoVehiculo)
        {
            Console.WriteLine($"Espacio liberado para el tipo de vehículo: {tipoVehiculo}");

            await Task.CompletedTask;
        }

    }
}
