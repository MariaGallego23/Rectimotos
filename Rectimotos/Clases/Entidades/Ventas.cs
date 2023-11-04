using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Ventas
    {
        [Key]
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
        public string? Observaciones { get; set; }

    }
}
