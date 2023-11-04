using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Reseñas
    {
        [Key]
        public int IdReseña { get; set; }
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int Idproductos { get; set; }

    }
}
