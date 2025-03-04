using SistemaSAE.API.Models;

namespace SistemaSAE.API.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> ObtenerPorCodigo(string codigo);
        Task<IEnumerable<Ticket>> ObtenerActivos();
        Task Crear(Ticket ticket);
        Task Actualizar(Ticket ticket);
        Task Eliminar(Ticket ticket);
    }
}
