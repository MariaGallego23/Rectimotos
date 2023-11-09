using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases
{
    public class Ciudad
    {
        [Key]
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public int IdEstado { get; set; }
    }
}
