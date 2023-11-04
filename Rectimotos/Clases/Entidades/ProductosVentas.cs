using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class ProductosVentas
    {
        [Key]
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
    }
}
