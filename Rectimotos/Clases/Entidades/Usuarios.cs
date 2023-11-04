using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Clases.Entidades
{
    public class Usuarios
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        public int Cedula { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        public int Telefono { get; set; }
        public int IdCiudad { get; set; }
        public string? Direccion { get; set; }
        public string? Imagen { get; set; }
        public int IdRol { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Contraseña { get; set; }


    }
}
