﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rectimotos.Clases;
using Rectimotos.Models;

namespace Rectimotos.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        private void PrepareViewData()
        {
            List<CategoriasViewModel> Categoria = _context.Categorias.ToList();
            ViewData["Categoriass"] = Categoria;
        }

        // // GET: Productos
        public async Task<IActionResult> Index()
        {
              return _context.Productos != null ?
                          View(await _context.Productos.ToListAsync()) :
                          Problem("Entity set 'DataContext.Productos'  is null.");
        }

        //// GET: Productos/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Productos == null)
        //    {
        //        return NotFound();
        //    }

        //    var productosViewModel = await _context.Productos
        //        .FirstOrDefaultAsync(m => m.IdProducto == id);
        //    if (productosViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productosViewModel);
        //}

        // GET: Productos/Create
        public IActionResult Create()
        {
            PrepareViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,IdCategoria,Precio,Existencias")] ProductosViewModel productosViewModel)
        {
            PrepareViewData();
            if (ModelState.IsValid)
            {
                PrepareViewData();

                _context.Add(productosViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productosViewModel);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            PrepareViewData();

            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productosViewModel = await _context.Productos.FindAsync(id);
            if (productosViewModel == null)
            {
                return NotFound();
            }
            return View(productosViewModel);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,IdCategoria,Precio,Existencias")] ProductosViewModel productosViewModel)
        {
            PrepareViewData();
            if (id != productosViewModel.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosViewModelExists(productosViewModel.IdProducto))
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
            return View(productosViewModel);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productosViewModel = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productosViewModel == null)
            {
                return NotFound();
            }

            return View(productosViewModel);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'DataContext.Productos'  is null.");
            }
            var productosViewModel = await _context.Productos.FindAsync(id);
            if (productosViewModel != null)
            {
                _context.Productos.Remove(productosViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosViewModelExists(int id)
        {
          return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
