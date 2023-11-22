using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class CiudadViewModel
    {
        [Key]
        public int IdCiudad { get; set; }
        public string? Nombre { get; set; }
        public int IdEstado { get; set; }
    }
}
