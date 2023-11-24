using System;
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

namespace Rectimotos.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("IdUser,Cedula,NombreCompleto,Telefono,IdCiudad,Direccion,Imagen,IdRol,Email,Usuario,Contraseña")] UsuariosViewModel model)
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
                model.Telefono = null;
                model.Imagen = null;

                _context.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "REGISTRO DE USUARIO EXITOSO";
            }
            return RedirectToAction("Index");
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuariosViewModel = await _context.Usuarios.FindAsync(id);
            if (usuariosViewModel == null)
            {
                return NotFound();
            }
            return View(usuariosViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,Cedula,NombreCompleto,Telefono,IdCiudad,Direccion,Imagen,IdRol,Email,Usuario,Contraseña")] UsuariosViewModel usuariosViewModel)
        {
            if (id != usuariosViewModel.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosViewModelExists(usuariosViewModel.IdUser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosViewModel);
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
