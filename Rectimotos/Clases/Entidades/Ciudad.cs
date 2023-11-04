using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Ciudad
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEstado { get; set; }
    }
}
