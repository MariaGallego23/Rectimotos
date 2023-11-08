using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class EstadosViewModel
    {
        [Key]
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }

    }
}
