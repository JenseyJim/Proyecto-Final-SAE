using SistemaSAE.API.Models;

namespace SistemaSAE.API.Repositories.Interfaces
{
    public interface ITarifaRepository
    {
        Task<IEnumerable<Tarifa>> ObtenerTodas();
        Task<Tarifa> ObtenerPorTipoVehiculo(string tipoVehiculo);
        Task Crear(Tarifa tarifa);
        Task Actualizar(Tarifa tarifa);
        Task Eliminar(Tarifa tarifa);
    }
}
