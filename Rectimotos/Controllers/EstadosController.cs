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
    public class EstadosController : Controller
    {
        private readonly DataContext _context;

        public EstadosController(DataContext context)
        {
            _context = context;
        }

        private void PrepareViewDatas()
        {
            List<Estados> Estados = _context.Estados.ToList();
            ViewData["Estadoss"] = Estados;

            List<Paises> Paises = _context.Paises.ToList();
            ViewData["Paisess"] = Paises;
        }

        private void PrepareViewData(int idPais)
        {
            ViewBag.Pais = idPais;
        }

        // GET: Estados
        public async Task<IActionResult> Index(int idPais)
        {
            var estados = await _context.Estados
                .Where(e => e.IdPais == idPais)
                .ToListAsync();

            ViewBag.Pais = idPais;
            return View(estados);
        }

        // GET: Estados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados.FirstOrDefaultAsync(m => m.IdEstado == id);
            if (estados == null)
            {
                return NotFound();
            }

            return View(estados);
        }

        // GET: Estados/Create
        public IActionResult Create(int idPais)
        {
            PrepareViewDatas();
            ViewBag.Pais = idPais;

            PrepareViewData(idPais);
            return View();
        }

        // POST: Estados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,Nombre,IdPais")] Estados estados)
        {
            PrepareViewDatas();

            if (ModelState.IsValid)
            {
                _context.Add(estados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { idPais = estados.IdPais });
            }

            // Si el modelo no es válido, vuelve a cargar la vista Create
            PrepareViewData(estados.IdPais);
            return View(estados);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            PrepareViewDatas();

            if (id == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados.FindAsync(id);
            if (estados == null)
            {
                return NotFound();
            }
            return View(estados);
        }

        // POST: Estados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,Nombre,IdPais")] Estados estados)
        {
            PrepareViewDatas();

            if (id != estados.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosExists(estados.IdEstado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { idPais = estados.IdPais });
            }
            PrepareViewData(estados.IdPais);

            return View(estados);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados.FirstOrDefaultAsync(m => m.IdEstado == id);
            if (estados == null)
            {
                return NotFound();
            }

            return View(estados);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estados = await _context.Estados.FindAsync(id);
            _context.Estados.Remove(estados);
            await _context.SaveChangesAsync();
            PrepareViewData(estados.IdPais);

            return RedirectToAction(nameof(Index), new { idPais = estados.IdPais });
        }

        private bool EstadosExists(int id)
        {
            return _context.Estados.Any(e => e.IdEstado == id);
        }


    }
}
