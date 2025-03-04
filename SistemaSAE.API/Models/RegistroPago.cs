using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSAE.API.Models
{
    public class RegistroPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDRegistroPago { get; set; }
        public string CodigoTicket { get; set; } = string.Empty;

        public string TipoVehiculo { get; set; } = string.Empty;

        public DateTime HoraIngreso { get; set; }

        public DateTime HoraSalida { get; set; }

        public decimal MontoCobrado { get; set; }

        public int DuracionEstadia { get; set; }
    }
}
