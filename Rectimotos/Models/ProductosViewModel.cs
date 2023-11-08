﻿using System.ComponentModel.DataAnnotations;

namespace Rectimotos.Models
{
    public class ProductosViewModel
    {
        [Key]
        public int IdProducto { get; set; } 
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int Existencias { get; set; }
    }
}