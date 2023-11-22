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
    public class CategoriasController : Controller
    {
        private readonly DataContext _context;

        public CategoriasController(DataContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
              return _context.Categorias != null ? 
                          View(await _context.Categorias.ToListAsync()) :
                          Problem("Entity set 'DataContext.Categorias'  is null.");
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoriasViewModel = await _context.Categorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoriasViewModel == null)
            {
                return NotFound();
            }

            return View(categoriasViewModel);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,Nombre")] CategoriasViewModel categoriasViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriasViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriasViewModel);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoriasViewModel = await _context.Categorias.FindAsync(id);
            if (categoriasViewModel == null)
            {
                return NotFound();
            }
            return View(categoriasViewModel);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,Nombre")] CategoriasViewModel categoriasViewModel)
        {
            if (id != categoriasViewModel.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriasViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasViewModelExists(categoriasViewModel.IdCategoria))
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
            return View(categoriasViewModel);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoriasViewModel = await _context.Categorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoriasViewModel == null)
            {
                return NotFound();
            }

            return View(categoriasViewModel);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'DataContext.Categorias'  is null.");
            }
            var categoriasViewModel = await _context.Categorias.FindAsync(id);
            if (categoriasViewModel != null)
            {
                _context.Categorias.Remove(categoriasViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
public async Task<IActionResult> FilterByCategory(int? categoriaId)
{
    if (categoriaId == null)
    {
        return RedirectToAction(nameof(Index));
    }

    // Filtrar productos por categoría
    var productosFiltrados = await _context.Productos
        .Where(p => p.Categorias.Any(pc => pc.IdCategorias == categoriaId))
        .ToListAsync();

    // Puedes necesitar adaptar esto según la estructura real de tu modelo y base de datos

    // Pasa los productos filtrados a la vista
    return View("Index", productosFiltrados);
}

        private bool CategoriasViewModelExists(int id)
        {
          return (_context.Categorias?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
