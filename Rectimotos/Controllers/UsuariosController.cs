﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Models;
using Rectimotos.Clases.Entidades;
using Rectimotos.Clases;
using Microsoft.AspNetCore.Hosting;

namespace Rectimotos.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UsuariosController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        private void PrepareViewData()
        {
            List<Ciudad> ciudad = _context.Ciudad.ToList();
            ViewData["Ciudadess"] = ciudad;

            List<RolesUsuarios> rol = _context.RolesUsuarios.ToList();
            ViewData["Roless"] = rol;
        }


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return _context.Usuarios != null ? 
                          View(await _context.Usuarios.ToListAsync()) :
                          Problem("Entity set 'DataContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuariosViewModel = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (usuariosViewModel == null)
            {
                return NotFound();
            }

            return View(usuariosViewModel);
        }


        // GET: Usuarios/Create
        public IActionResult Create()
        {
            PrepareViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,Cedula,NombreCompleto,Telefono,IdCiudad,Direccion,Imagen,IdRol,Email,Usuario,Contraseña")] UsuariosViewModel model, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            PrepareViewData();
            var existingEmail = _context.Usuarios.FirstOrDefault(u => u.Usuario == model.Usuario);
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.Usuario == model.Usuario);
            var existingPassword = _context.Usuarios.FirstOrDefault(u => u.Contraseña == model.Contraseña);

            // Verificar si el correo ya existe en la base de datos
            if (existingEmail != null)
            {
                TempData["ErrorEmail"] = "El Email ya esta en uso, por favor elije otro";
            }
            if (existingUser != null)
            {
                TempData["ErrorUser"] = "El Usuario ya esta en uso, por favor elije otro";
            }
            // Verificar si la contraseña ya existe en la base de datos
            else if (existingPassword != null)
            {
                TempData["ErrorPassword"] = "La contraseña ya esta en uso, por favor elije otro";
            }
            else
            {
                PrepareViewData();

                // Verificar si la imagen se ha cargado
                if (model.ImagenArchivo != null && model.ImagenArchivo.Length > 0)
                {
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "Imagenes");

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagenArchivo.FileName;
                    var filePath = Path.Combine(imagePath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImagenArchivo.CopyTo(fileStream);
                    }

                    model.Imagen = Path.Combine("Imagenes", uniqueFileName);
                }

                _context.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "REGISTRO DE USUARIO EXITOSO";
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            PrepareViewData();

            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var viewModel = new UsuariosViewModel
            {
                IdUser = usuario.IdUser,
                Cedula = usuario.Cedula,
                NombreCompleto = usuario.NombreCompleto,
                Telefono = usuario.Telefono,
                IdCiudad = usuario.IdCiudad,
                Direccion = usuario.Direccion,
                Imagen = usuario.Imagen,
                IdRol = usuario.IdRol,
                Email = usuario.Email,
                Usuario = usuario.Usuario,
                Contraseña = usuario.Contraseña
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuariosViewModel model, IFormFile ImagenArchivo)
        {
            PrepareViewData();

            if (id != model.IdUser)
            {
                return NotFound();
            }
            else
            {
                if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Imagenes");

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagenArchivo.FileName;
                    var filePath = Path.Combine(imagePath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImagenArchivo.CopyTo(fileStream);
                    }

                    model.Imagen = Path.Combine("Imagenes", uniqueFileName);
                }
                var usuario = await _context.Usuarios.FindAsync(id);

                usuario.Imagen = model.Imagen;
                usuario.Cedula = model.Cedula;
                usuario.NombreCompleto = model.NombreCompleto;
                usuario.Telefono = model.Telefono;
                usuario.IdCiudad = model.IdCiudad;
                usuario.Direccion = model.Direccion;
                usuario.IdRol = model.IdRol;
                usuario.Email = model.Email;
                usuario.Usuario = model.Usuario;
                usuario.Contraseña = model.Contraseña;

                _context.Update(usuario);
                await _context.SaveChangesAsync();

                TempData["Message"] = "CAMBIO EXITOSO";

            }
            return RedirectToAction("Edit", new { id = model.IdUser });
        }
 
       
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuariosViewModel = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (usuariosViewModel == null)
            {
                return NotFound();
            }

            return View(usuariosViewModel);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DataContext.Usuario'  is null.");
            }
            var usuariosViewModel = await _context.Usuarios.FindAsync(id);
            if (usuariosViewModel != null)
            {
                _context.Usuarios.Remove(usuariosViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosViewModelExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
