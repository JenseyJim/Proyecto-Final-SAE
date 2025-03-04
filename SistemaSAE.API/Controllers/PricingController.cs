using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Interfaces;

namespace SistemaSAE.API.Controllers
{
    [ApiController]
    [Route("api/pricing")]
    public class PricingController : ControllerBase
    {
        private readonly IPricing _pricingService;

        public PricingController(IPricing pricingService)
        {
            _pricingService = pricingService;
        }

        // GET /api/pricing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PricingDTO>>> GetTarifas()
        {
            var tarifas = await _pricingService.ObtenerTarifas();
            return Ok(tarifas);
        }

        // GET /api/pricing/{tipoVehiculo}
        [HttpGet("{tipoVehiculo}")]
        public async Task<ActionResult<PricingDTO>> GetTarifaByTipoVehiculo(string tipoVehiculo)
        {
            var tarifa = await _pricingService.ObtenerTarifaPorTipoVehiculo(tipoVehiculo);
            if (tarifa == null)
                return NotFound(new { message = "Tarifa no encontrada." });

            return Ok(tarifa);
        }

        // POST /api/pricing
        [HttpPost]
        public async Task<ActionResult> CreateTarifa([FromBody] PricingDTO tarifaDTO)
        {
            try
            {
                await _pricingService.CrearTarifa(tarifaDTO);
                return Ok(new { message = "Tarifa creada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT /api/pricing
        [HttpPut]
        public async Task<ActionResult> UpdateTarifa([FromBody] PricingDTO tarifaDTO)
        {
            try
            {
                await _pricingService.ActualizarTarifa(tarifaDTO);
                return Ok(new { message = "Tarifa actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/pricing/{idTarifa}
        [HttpDelete("{idTarifa}")]
        public async Task<ActionResult> DeleteTarifa(int idTarifa)
        {
            try
            {
                await _pricingService.EliminarTarifa(idTarifa);
                return Ok(new { message = "Tarifa eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/pricing/calculate-payment
        [HttpPost("calculate-payment")]
        public async Task<ActionResult<decimal>> CalculatePayment([FromBody] PaymentCalculationDTO paymentDTO)
        {
            try
            {
                var totalPago = await _pricingService.CalcularPago(paymentDTO.CodigoTicket, paymentDTO.HoraSalida);
                return Ok(totalPago);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
