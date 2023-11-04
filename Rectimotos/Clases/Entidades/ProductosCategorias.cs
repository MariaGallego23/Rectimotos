using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class ProductosCategorias
    {
        [Key]
        public int Id { get; set; }
        public int IdProductos { get; set; }
        public int IdCategorias { get; set; } 
    }
}
