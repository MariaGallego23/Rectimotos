using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Models;

namespace Rectimotos.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private readonly DataContext _context;

        public VentasController(DataContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
              return _context.Ventas != null ? 
                          View(await _context.Ventas.ToListAsync()) :
                          Problem("Entity set 'DataContext.Ventas'  is null.");
        }

        public IActionResult PedidosUser()
        {
            return View();
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var ventasViewModel = await _context.Ventas
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventasViewModel == null)
            {
                return NotFound();
            }

            return View(ventasViewModel);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,Fecha,IdUsuario,Cantidad,Observaciones")] VentasViewModel ventasViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventasViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ventasViewModel);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var ventasViewModel = await _context.Ventas.FindAsync(id);
            if (ventasViewModel == null)
            {
                return NotFound();
            }
            return View(ventasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,Fecha,IdUsuario,Cantidad,Observaciones")] VentasViewModel ventasViewModel)
        {
            if (id != ventasViewModel.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventasViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasViewModelExists(ventasViewModel.IdVenta))
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
            return View(ventasViewModel);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var ventasViewModel = await _context.Ventas
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventasViewModel == null)
            {
                return NotFound();
            }

            return View(ventasViewModel);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'DataContext.Ventas'  is null.");
            }
            var ventasViewModel = await _context.Ventas.FindAsync(id);
            if (ventasViewModel != null)
            {
                _context.Ventas.Remove(ventasViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasViewModelExists(int id)
        {
          return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
