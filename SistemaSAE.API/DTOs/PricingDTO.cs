namespace SistemaSAE.API.DTOs
{
    public class PricingDTO
    {
        public int IDTarifa { get; set; }

        public string TipoVehiculo { get; set; }

        public decimal TarifaPorHora { get; set; }

        public int TiempoCortesia { get; set; }
    }
}
