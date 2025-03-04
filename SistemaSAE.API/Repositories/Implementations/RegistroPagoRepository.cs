using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Repositories.Implementations
{
    public class RegistroPagoRepository : IRegistroPagoRepository
    {
        private readonly SAEContext _context;

        public RegistroPagoRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task Crear(RegistroPago registroPago)
        {
            _context.RegistroPago.Add(registroPago);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistroPago>> ObtenerPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.RegistroPago
                .Where(r => r.HoraIngreso >= fechaInicio && r.HoraSalida <= fechaFin)
                .ToListAsync();
        }
    }
}
