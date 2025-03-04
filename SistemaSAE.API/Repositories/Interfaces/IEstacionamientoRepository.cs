using SistemaSAE.API.Enums;
using SistemaSAE.API.Models;

namespace SistemaSAE.API.Repositories.Interfaces
{
    public interface IEstacionamientoRepository
    {
        Task<IEnumerable<Estacionamiento>> ObtenerTodos();
        Task<Estacionamiento> ObtenerPorTipoVehiculo(TipoVehiculo tipoVehiculo);
        Task Actualizar(Estacionamiento estacionamiento);
        Task Crear(Estacionamiento estacionamiento);
        
        Task LiberarEspacio(string tipoVehiculo);

    }
}
