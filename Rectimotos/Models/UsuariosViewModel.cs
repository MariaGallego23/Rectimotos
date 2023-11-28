using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class UsuariosViewModel
    {
        [Key]
        public int IdUser { get; set; }
        public int Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string? Telefono { get; set; }
        public int? IdCiudad { get; set; }
        public string? Direccion { get; set; }
        public int? Imagen { get; set; }
        public int IdRol { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
