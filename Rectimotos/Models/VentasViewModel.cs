using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class VentasViewModel
    {
        [Key]
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
        public string? Observaciones { get; set; }
        public int IdEstado { get; set; }

    }
}
