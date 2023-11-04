using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Categorias
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
    }
}
