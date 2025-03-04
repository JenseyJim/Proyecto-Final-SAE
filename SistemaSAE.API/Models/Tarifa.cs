using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaSAE.API.Models
{
    public class Tarifa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IDTarifa { get; set; }

        public string TipoVehiculo { get; set; } = string.Empty;

        public decimal TarifaPorHora { get; set; }

        public int TiempoCortesia { get; set; }

    }
}
