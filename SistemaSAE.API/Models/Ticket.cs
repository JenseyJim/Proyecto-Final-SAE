using SistemaSAE.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaSAE.API.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Codigo { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
        public DateTime HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public decimal TotalPago { get; set; }



    }
}
