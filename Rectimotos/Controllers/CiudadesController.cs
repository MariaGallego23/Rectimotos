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
    public class CiudadesController : Controller
    {
        private readonly DataContext _context;

        public CiudadesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var ciudades = await _context.Ciudad
                .Where(c => c.IdEstado == id).ToListAsync();

            ViewBag.Estado = id;
            return View(ciudades);
        }


        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ciudad == null)
            {
                return NotFound();
            }

            var ciudadViewModel = await _context.Ciudad
                .FirstOrDefaultAsync(m => m.IdCiudad == id);
            if (ciudadViewModel == null)
            {
                return NotFound();
            }

            return View(ciudadViewModel);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCiudad,Nombre,IdEstado")] CiudadViewModel ciudadViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudadViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciudadViewModel);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ciudad == null)
            {
                return NotFound();
            }

            var ciudadViewModel = await _context.Ciudad.FindAsync(id);
            if (ciudadViewModel == null)
            {
                return NotFound();
            }
            return View(ciudadViewModel);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCiudad,Nombre,IdEstado")] CiudadViewModel ciudadViewModel)
        {
            if (id != ciudadViewModel.IdCiudad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudadViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadViewModelExists(ciudadViewModel.IdCiudad))
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
            return View(ciudadViewModel);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ciudad == null)
            {
                return NotFound();
            }

            var ciudadViewModel = await _context.Ciudad
                .FirstOrDefaultAsync(m => m.IdCiudad == id);
            if (ciudadViewModel == null)
            {
                return NotFound();
            }

            return View(ciudadViewModel);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ciudad == null)
            {
                return Problem("Entity set 'DataContext.Ciudad'  is null.");
            }
            var ciudadViewModel = await _context.Ciudad.FindAsync(id);
            if (ciudadViewModel != null)
            {
                _context.Ciudad.Remove(ciudadViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadViewModelExists(int id)
        {
          return (_context.Ciudad?.Any(e => e.IdCiudad == id)).GetValueOrDefault();
        }
    }
}
