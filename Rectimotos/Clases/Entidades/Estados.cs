using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Estados
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }

    }
}
