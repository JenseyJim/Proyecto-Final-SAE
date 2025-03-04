using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Repositories.Implementations
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly SAEContext _context;

        public ReporteRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task Crear(Reporte reporte)
        {
            _context.Reporte.Add(reporte);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reporte>> ObtenerTodos()
        {
            return await _context.Reporte.ToListAsync();
        }

        public async Task<Reporte> ObtenerPorId(int idReporte)
        {
            return await _context.Reporte.FirstOrDefaultAsync(r => r.IDReporte == idReporte);
        }

        public async Task Eliminar(Reporte reporte)
        {
            _context.Reporte.Remove(reporte);
            await _context.SaveChangesAsync();
        }
    }
}
