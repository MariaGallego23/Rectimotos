using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Clases.Entidades;

namespace Rectimotos.Clases
{
    public class DataContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }



        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Paises> Paises { get; set; }
        public DbSet<RolesUsuarios> RolesUsuarios { get; set; } 
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<ProductosVentas> ProductosVentas { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<ProductosCategorias> ProductosCategorias { get; set; }
        public DbSet<Reseñas> Reseñas { get; set; }
    }
}
