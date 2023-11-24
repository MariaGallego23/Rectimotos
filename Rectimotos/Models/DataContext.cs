using Microsoft.EntityFrameworkCore;
using Rectimotos.Clases;
using Rectimotos.Clases.Entidades;

namespace Rectimotos.Models
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<UsuariosViewModel> Usuarios { get; set; }
        public DbSet<CategoriasViewModel> Categorias { get; set; }
        public DbSet<ProductosViewModel> Productos { get; set; }
        public DbSet<VentasViewModel> Ventas { get; set; }

        public DbSet<Paises> Paises { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<RolesUsuarios> RolesUsuarios { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<ProductosVentas> ProductosVentas { get; set; }
        public DbSet<ProductosCategorias> ProductosCategorias { get; set; }
        public DbSet<Reseñas> Reseñas { get; set; }
        
    }
}
