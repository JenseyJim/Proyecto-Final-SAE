using SistemaSAE.API.Models;

namespace SistemaSAE.API.Repositories.Interfaces
{
    public interface IReporteRepository
    {
        Task Crear(Reporte reporte);
        Task<IEnumerable<Reporte>> ObtenerTodos();
        Task<Reporte> ObtenerPorId(int idReporte);
        Task Eliminar(Reporte reporte);
    }
}
