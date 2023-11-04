using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class RolesUsuarios
    {
        [Key]
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
    }
}
