using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Clases;
using Rectimotos.Clases.Entidades;
using Rectimotos.Models;

namespace Rectimotos.Controllers
{
    public class CiudadController : Controller
    {
        private readonly DataContext _context;

        public CiudadController(DataContext context)
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

        private void PrepareViewData(int idEstado)
        {
            ViewBag.Estado = idEstado;
        }

        // GET: Ciudad
        public async Task<IActionResult> Index(int idEstado)
        {
            var ciudades = await _context.Ciudad
                .Where(c => c.IdEstado == idEstado)
                .ToListAsync();

            ViewBag.Estado = idEstado;
            return View(ciudades);
        }

        // GET: Ciudad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudad.FirstOrDefaultAsync(m => m.IdCiudad == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // GET: Ciudad/Create
        public IActionResult Create(int idEstado)
        {
            PrepareViewDatas();
            ViewBag.Estado = idEstado;

            PrepareViewData(idEstado);
            return View();
        }

        // POST: Ciudad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCiudad,Nombre,IdEstado")] Ciudad ciudad)
        {
            PrepareViewDatas();

            if (ModelState.IsValid)
            {
                _context.Add(ciudad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { idEstado = ciudad.IdEstado });
            }

            // Si el modelo no es válido, vuelve a cargar la vista Create
            PrepareViewData(ciudad.IdEstado);
            return View(ciudad);
        }

        // GET: Ciudad/Edit/5
        public async Task<IActionResult> Edit(int? id, int idEstado)
        {
            PrepareViewDatas();

            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudad.FindAsync(id);
            ViewBag.Estado = idEstado;

            if (ciudad == null)
            {
                return NotFound();
            }
            return View(ciudad);
        }

        // POST: Ciudad/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCiudad,Nombre,IdEstado")] Ciudad ciudad)
        {
            PrepareViewDatas();

            if (id != ciudad.IdCiudad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadExists(ciudad.IdCiudad))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { idEstado = ciudad.IdEstado });
            }
            PrepareViewData(ciudad.IdEstado);
            return View(ciudad);
        }

        // GET: Ciudad/Delete/5
        public async Task<IActionResult> Delete(int? id, int idEstado)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudad.FirstOrDefaultAsync(m => m.IdCiudad == id);
            ViewBag.Estado = idEstado;

            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudad = await _context.Ciudad.FindAsync(id);
            _context.Ciudad.Remove(ciudad);
            await _context.SaveChangesAsync();

            PrepareViewData(ciudad.IdEstado);

            return RedirectToAction(nameof(Index), new { idEstado = ciudad.IdEstado });
        }

        private bool CiudadExists(int id)
        {
            return _context.Ciudad.Any(e => e.IdCiudad == id);
        }

    }
}
