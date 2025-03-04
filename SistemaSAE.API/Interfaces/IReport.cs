using SistemaSAE.API.DTOs;

namespace SistemaSAE.API.Interfaces
{
    public interface IReport
    {
        Task<ReporteDTO> GenerarReporte(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<ReporteDTO>> ObtenerReportesHistoricos();
        Task<ReporteDTO> ObtenerReportePorId(int idReporte);
        Task CrearReporte(ReporteDTO reporte);
        Task EliminarReporte(int idReporte);
    }


}
