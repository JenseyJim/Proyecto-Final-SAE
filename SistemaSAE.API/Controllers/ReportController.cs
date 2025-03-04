using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Interfaces;

namespace SistemaSAE.API.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReport _reportService;

        public ReportController(IReport reportService)
        {
            _reportService = reportService;
        }

        // GET /api/report/generate
        [HttpGet("generate")]
        public async Task<ActionResult<ReporteDTO>> GenerateReport([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var reporte = await _reportService.GenerarReporte(fechaInicio, fechaFin);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET /api/report/historical
        [HttpGet("historical")]
        public async Task<ActionResult<IEnumerable<ReporteDTO>>> GetHistoricalReports()
        {
            var reportes = await _reportService.ObtenerReportesHistoricos();
            return Ok(reportes);
        }

        // GET /api/report/{idReporte}
        [HttpGet("{idReporte}")]
        public async Task<ActionResult<ReporteDTO>> GetReportById(int idReporte)
        {
            var reporte = await _reportService.ObtenerReportePorId(idReporte);
            if (reporte == null)
                return NotFound(new { message = "Reporte no encontrado." });

            return Ok(reporte);
        }

        // POST /api/report
        [HttpPost]
        public async Task<ActionResult> CreateReport([FromBody] ReporteDTO reporteDTO)
        {
            try
            {
                await _reportService.CrearReporte(reporteDTO);
                return Ok(new { message = "Reporte creado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/report/{idReporte}
        [HttpDelete("{idReporte}")]
        public async Task<ActionResult> DeleteReport(int idReporte)
        {
            try
            {
                await _reportService.EliminarReporte(idReporte);
                return Ok(new { message = "Reporte eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
