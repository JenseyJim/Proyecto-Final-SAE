using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SistemaSAE.API.Enums;

namespace SistemaSAE.API.Models
{
    public class Estacionamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDEstacionamiento { get; set; }
                
        public TipoVehiculo TipoVehiculo { get; set; }

       
        public int CapacidadTotal { get; set; }

        
        public int Ocupados { get; set; }

        
        public int Disponibles => CapacidadTotal - Ocupados;


    }
}
