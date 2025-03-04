using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Repositories.Implementations
{
    public class TarifaRepository : ITarifaRepository
    {
        private readonly SAEContext _context;

        public TarifaRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarifa>> ObtenerTodas()
        {
            return await _context.Tarifa.ToListAsync();
        }

        public async Task<Tarifa> ObtenerPorTipoVehiculo(string tipoVehiculo)
        {
            return await _context.Tarifa.FirstOrDefaultAsync(t => t.TipoVehiculo == tipoVehiculo);
        }

        public async Task Crear(Tarifa tarifa)
        {
            _context.Tarifa.Add(tarifa);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Tarifa tarifa)
        {
            _context.Tarifa.Update(tarifa);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(Tarifa tarifa)
        {
            _context.Tarifa.Remove(tarifa);
            await _context.SaveChangesAsync();
        }
    }
}
