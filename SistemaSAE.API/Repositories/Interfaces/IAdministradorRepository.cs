using SistemaSAE.API.Models;

namespace SistemaSAE.API.Repositories.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<Administrador> AutenticarAdministrador(string nombreUsuario, string contrasena);
    }
}
