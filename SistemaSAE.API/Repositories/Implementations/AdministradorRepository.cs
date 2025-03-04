using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Repositories.Implementations
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly SAEContext _context;

        public AdministradorRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task<Administrador> AutenticarAdministrador(string nombreUsuario, string contrasena)
        {
            return await _context.Administrador
                .FirstOrDefaultAsync(a => a.NombreUsuario == nombreUsuario && a.Contrasena == contrasena);
        }
    }
}
