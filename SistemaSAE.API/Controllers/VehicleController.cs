using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Interfaces;

namespace SistemaSAE.API.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicle _vehicleService;

        public VehicleController(IVehicle vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // POST /api/vehicles/entry
        [HttpPost("entry")]
        public async Task<ActionResult<TicketDTO>> Entry([FromBody] TicketViewModel vehiculoDTO)
        {
            try
            {
                var ticket = await _vehicleService.GenerarTicket(vehiculoDTO);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/vehicles/exit
        [HttpPost("exit")]
        public async Task<ActionResult> Exit([FromBody] TicketExitDTO exitDTO)
        {
            try
            {
                var resultado = await _vehicleService.RegistrarSalida(exitDTO.CodigoTicket);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // GET /api/vehicles/active-tickets
        [HttpGet("active-tickets")]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetActiveTickets()
        {
            var tickets = await _vehicleService.ObtenerTicketsActivos();
            return Ok(tickets);
        }

        // GET /api/vehicles/{codigo}
        [HttpGet("{codigo}")]
        public async Task<ActionResult<TicketDTO>> GetTicketByCodigo(string codigo)
        {
            var ticket = await _vehicleService.ObtenerTicketPorCodigo(codigo);
            if (ticket == null)
                return NotFound(new { message = "Ticket no encontrado." });

            return Ok(ticket);
        }

        // PUT /api/vehicles/update
        [HttpPut("update")]
        public async Task<ActionResult> UpdateTicket([FromBody] TicketDTO ticketDTO)
        {
            try
            {
                await _vehicleService.ActualizarTicket(ticketDTO);
                return Ok(new { message = "Ticket actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/vehicles/{codigo}
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> DeleteTicket(string codigo)
        {
            try
            {
                bool eliminado = await _vehicleService.EliminarTicket(codigo);

                if (eliminado)
                {
                    return Ok(new { message = "Ticket eliminado exitosamente." });
                }
                else
                {
                    return NotFound(new { message = "Ticket no encontrado." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}