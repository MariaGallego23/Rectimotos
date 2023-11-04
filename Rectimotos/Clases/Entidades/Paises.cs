using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Paises
    {
        [Key]
        public int IdPais { get; set; }
        public string Nombre { get; set; }
    }
}
