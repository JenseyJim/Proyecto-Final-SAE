using SistemaSAE.API.DTOs;

namespace SistemaSAE.API.Interfaces
{
    public interface IVehicle
    {
        Task<TicketDTO> GenerarTicket(TicketViewModel vehiculo);
        
        Task<SalidaResponseDTO> RegistrarSalida(string codigoTicket);

        Task<IEnumerable<TicketDTO>> ObtenerTicketsActivos();
        Task<TicketDTO> ObtenerTicketPorCodigo(string codigo);
        Task ActualizarTicket(TicketDTO ticket);
        Task<bool> EliminarTicket(string codigo);

        

    }

}
