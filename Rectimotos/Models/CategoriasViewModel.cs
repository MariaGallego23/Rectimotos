using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class CategoriasViewModel
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? Nombre { get; set; }
    }
}
