using SistemaSAE.API.DTOs;

namespace SistemaSAE.API.Interfaces
{
    public interface IPricing
    {
        Task<decimal> CalcularPago(string codigoTicket, DateTime horaSalida);
        Task<IEnumerable<PricingDTO>> ObtenerTarifas();
        Task<PricingDTO> ObtenerTarifaPorTipoVehiculo(string tipoVehiculo);
        Task ActualizarTarifa(PricingDTO tarifa);
        Task CrearTarifa(PricingDTO tarifa);
        Task EliminarTarifa(int idTarifa);
    }


}
