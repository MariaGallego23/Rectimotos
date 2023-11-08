using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class PaisViewModel
    {
        [Key]
        public int IdPais { get; set; }
        public string Nombre { get; set; }
    }
}
