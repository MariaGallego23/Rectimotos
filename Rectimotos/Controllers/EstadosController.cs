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
    public class EstadosController : Controller
    {
        private readonly DataContext _context;

        public EstadosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var estados = await _context.Estados
                .Where(e => e.IdPais == id).ToListAsync();

            ViewBag.Pais = id;
            return View(estados);
        }


        // GET: Estados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estadosViewModel = await _context.Estados
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (estadosViewModel == null)
            {
                return NotFound();
            }

            return View(estadosViewModel);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,Nombre,IdPais")] EstadosViewModel estadosViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosViewModel);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estadosViewModel = await _context.Estados.FindAsync(id);
            if (estadosViewModel == null)
            {
                return NotFound();
            }
            return View(estadosViewModel);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,Nombre,IdPais")] EstadosViewModel estadosViewModel)
        {
            if (id != estadosViewModel.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadosViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosViewModelExists(estadosViewModel.IdEstado))
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
            return View(estadosViewModel);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estadosViewModel = await _context.Estados
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (estadosViewModel == null)
            {
                return NotFound();
            }

            return View(estadosViewModel);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'DataContext.Estados'  is null.");
            }
            var estadosViewModel = await _context.Estados.FindAsync(id);
            if (estadosViewModel != null)
            {
                _context.Estados.Remove(estadosViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosViewModelExists(int id)
        {
          return (_context.Estados?.Any(e => e.IdEstado == id)).GetValueOrDefault();
        }
    }
}
