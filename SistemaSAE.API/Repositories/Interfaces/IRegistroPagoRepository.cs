using SistemaSAE.API.Models;

namespace SistemaSAE.API.Repositories.Interfaces
{
    public interface IRegistroPagoRepository
    {
        Task Crear(RegistroPago registroPago);
        Task<IEnumerable<RegistroPago>> ObtenerPorRangoFechas(DateTime fechaInicio, DateTime fechaFin);
    }
}
