using System.ComponentModel.DataAnnotations;
using Rectimotos.Clases.Entidades;

namespace Rectimotos.Models
{
    public class ProductosViewModel
    {
        [Key]
        public int IdProducto { get; set; } 
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }
    }
  
}
