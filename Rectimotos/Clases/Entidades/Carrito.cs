using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
    }
}
