using SistemaSAE.API.Enums;
using System.Text.Json.Serialization;

namespace SistemaSAE.API.DTOs
{
    public class EstacionamientoDTO
    {
        public int IDEstacionamiento { get; set; }
                
        public string TipoVehiculo { get; set; }
        public int CapacidadTotal { get; set; }
        public int Ocupados { get; set; }
        public int Disponibles => CapacidadTotal - Ocupados;
    }
}
