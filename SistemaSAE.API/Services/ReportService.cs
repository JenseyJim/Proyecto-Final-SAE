using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Interfaces;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Services
{
    public class ReportService : IReport
    {
        private readonly IRegistroPagoRepository _registroPagoRepository;
        private readonly IReporteRepository _reporteRepository;

        public ReportService(IRegistroPagoRepository registroPagoRepository, IReporteRepository reporteRepository)
        {
            _registroPagoRepository = registroPagoRepository;
            _reporteRepository = reporteRepository;
        }

        public async Task<ReporteDTO> GenerarReporte(DateTime fechaInicio, DateTime fechaFin)
        {
            fechaInicio = fechaInicio.Date;
            fechaFin = fechaFin.Date.AddDays(1).AddTicks(-1);

            var registros = await _registroPagoRepository.ObtenerPorRangoFechas(fechaInicio, fechaFin);

            var ingresosTotales = registros.Sum(r => r.MontoCobrado);

            var totalDias = (fechaFin - fechaInicio).TotalDays + 1;
            var ocupacionPromedio = totalDias > 0 ? (int)(registros.Count() / totalDias) : 0;

            var reporte = new Reporte
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                IngresosTotales = ingresosTotales,
                OcupacionPromedio = ocupacionPromedio
            };

            await _reporteRepository.Crear(reporte);

            return new ReporteDTO
            {
                IDReporte = reporte.IDReporte,
                FechaInicio = reporte.FechaInicio,
                FechaFin = reporte.FechaFin,
                IngresosTotales = reporte.IngresosTotales,
                OcupacionPromedio = reporte.OcupacionPromedio
            };
        }

        public async Task<IEnumerable<ReporteDTO>> ObtenerReportesHistoricos()
        {
            var reportes = await _reporteRepository.ObtenerTodos();

            return reportes.Select(r => new ReporteDTO
            {
                IDReporte = r.IDReporte,
                FechaInicio = r.FechaInicio,
                FechaFin = r.FechaFin,
                IngresosTotales = r.IngresosTotales,
                OcupacionPromedio = r.OcupacionPromedio
            });
        }

        public async Task<ReporteDTO> ObtenerReportePorId(int idReporte)
        {
            var reporte = await _reporteRepository.ObtenerPorId(idReporte);

            if (reporte == null)
                return null;

            return new ReporteDTO
            {
                IDReporte = reporte.IDReporte,
                FechaInicio = reporte.FechaInicio,
                FechaFin = reporte.FechaFin,
                IngresosTotales = reporte.IngresosTotales,
                OcupacionPromedio = reporte.OcupacionPromedio
            };
        }

        public async Task CrearReporte(ReporteDTO reporteDTO)
        {
            var reporte = new Reporte
            {
                FechaInicio = reporteDTO.FechaInicio,
                FechaFin = reporteDTO.FechaFin,
                IngresosTotales = reporteDTO.IngresosTotales,
                OcupacionPromedio = reporteDTO.OcupacionPromedio
            };

            await _reporteRepository.Crear(reporte);
        }

        public async Task EliminarReporte(int idReporte)
        {
            var reporte = await _reporteRepository.ObtenerPorId(idReporte);

            if (reporte != null)
            {
                await _reporteRepository.Eliminar(reporte);
            }
        }
    }
}
