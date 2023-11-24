using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Clases.Entidades;
using Rectimotos.Models;

namespace Rectimotos.Controllers
{
    public class PaisesController : Controller
    {
        private readonly DataContext _context;

        public PaisesController(DataContext context)
        {
            _context = context;
        }

        // GET: Paises
        public async Task<IActionResult> Index()
        {
            var paises = await _context.Paises.ToListAsync();
            return View(paises);
        }

        // GET: Paises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises.FirstOrDefaultAsync(m => m.IdPais == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // GET: Paises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPais,Nombre")] Paises pais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: Paises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Paises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPais,Nombre")] Paises pais)
        {
            if (id != pais.IdPais)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisesExists(pais.IdPais))
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
            return View(pais);
        }

        // GET: Paises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises.FirstOrDefaultAsync(m => m.IdPais == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisesExists(int id)
        {
            return _context.Paises.Any(e => e.IdPais == id);
        }
    }
}