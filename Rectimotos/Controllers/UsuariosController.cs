using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Models;

namespace Rectimotos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,Cedula,NombreCompleto,Telefono,IdCiudad,Direccion,Imagen,IdRol,Email,Usuario,Contraseña")] UsuariosViewModel usuariosViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosViewModel);
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
