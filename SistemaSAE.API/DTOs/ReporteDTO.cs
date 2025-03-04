namespace SistemaSAE.API.DTOs
{
    public class ReporteDTO
    {
        public int IDReporte { get; set; }

       
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
        public decimal IngresosTotales { get; set; }

        
        public int OcupacionPromedio { get; set; }
    }
}
