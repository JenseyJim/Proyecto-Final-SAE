using SistemaSAE.API.DTOs;
using System.Threading.Tasks;

namespace SistemaSAE.API.Interfaces
{
    public interface IAdmin
    {
        Task AdministrarCapacidad(TicketViewModel vehiculo, int nuevaCapacidad);
        Task<AdminDTO> AutenticarAdministrador(string nombreUsuario, string contrasena);
        Task<IEnumerable<EstacionamientoDTO>> ObtenerEstadoEstacionamientos();
        Task<EstacionamientoDTO> ObtenerEstacionamientoPorTipoVehiculo(string tipoVehiculo);
        Task ActualizarEstacionamiento(EstacionamientoDTO estacionamiento);

        Task CrearEstacionamiento(EstacionamientoDTO estacionamientoDTO);
    }


}
