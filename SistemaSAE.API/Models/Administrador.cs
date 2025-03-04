using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSAE.API.Models
{
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IDAdministrador { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;


    }
}
