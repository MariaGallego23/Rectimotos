﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
              return _context.Paises != null ? 
                          View(await _context.Paises.ToListAsync()) :
                          Problem("Entity set 'DataContext.Paises'  is null.");
        }

        // GET: Paises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paises == null)
            {
                return NotFound();
            }

            var paisViewModel = await _context.Paises
                .FirstOrDefaultAsync(m => m.IdPais == id);
            if (paisViewModel == null)
            {
                return NotFound();
            }

            return View(paisViewModel);
        }

        // GET: Paises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPais,Nombre")] PaisViewModel paisViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paisViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paisViewModel);
        }

        // GET: Paises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paises == null)
            {
                return NotFound();
            }

            var paisViewModel = await _context.Paises.FindAsync(id);
            if (paisViewModel == null)
            {
                return NotFound();
            }
            return View(paisViewModel);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPais,Nombre")] PaisViewModel paisViewModel)
        {
            if (id != paisViewModel.IdPais)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paisViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisViewModelExists(paisViewModel.IdPais))
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
            return View(paisViewModel);
        }

        // GET: Paises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paises == null)
            {
                return NotFound();
            }

            var paisViewModel = await _context.Paises
                .FirstOrDefaultAsync(m => m.IdPais == id);
            if (paisViewModel == null)
            {
                return NotFound();
            }

            return View(paisViewModel);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paises == null)
            {
                return Problem("Entity set 'DataContext.Paises'  is null.");
            }
            var paisViewModel = await _context.Paises.FindAsync(id);
            if (paisViewModel != null)
            {
                _context.Paises.Remove(paisViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisViewModelExists(int id)
        {
          return (_context.Paises?.Any(e => e.IdPais == id)).GetValueOrDefault();
        }
    }
}
