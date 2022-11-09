using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FransfordSystem;
using FransfordSystem.Models;

namespace FransfordSystem.Controllers
{
    public class InventariosController : Controller
    {
        private readonly FransforDbContext _context;

        public InventariosController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.Inventario.Include(p=>p.producto).ToListAsync());
        }

        public async Task<IActionResult> Informe()
        {
            return View(await _context.Inventario.Include(p => p.producto).ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .FirstOrDefaultAsync(m => m.idInventario == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventarios/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Genera lista de categorias
                List<Producto> productoLista = new List<Producto>();
                productoLista = (from producto in _context.Producto select producto).ToList();
                productoLista.Insert(0, new Producto { IdProducto = 0, nombreProducto = "Seleccionar" });
                ViewBag.productoDeLista = productoLista;
                return View();
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Inventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idInventario,idProducto,fechaVencimiento,stock,entrada,salida")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                var producto1 = _context.Producto.Find(inventario.idProducto);
                inventario.producto = producto1;

                inventario.stock = inventario.entrada;

                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventario);
        }

        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idInventario,idProducto,fechaVencimiento,stock,entrada,salida")] Inventario inventario)
        {
            if (id != inventario.idInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    

                    var anterior = await _context.Inventario.AsNoTracking().FirstOrDefaultAsync(i => i.idInventario == id);
                    var valor = inventario.salida;
                    var valorAn = anterior.salida;

                    inventario.stock = inventario.stock - inventario.salida;
                    inventario.salida = valor + valorAn;


                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.idInventario))
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
            return View(inventario);
        }

        //Entrada de producto

        public async Task<IActionResult> Entrada(int? id)
        {
            if (id == null || _context.Inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrada(int id, [Bind("idInventario,idProducto,fechaVencimiento,stock,entrada,salida")] Inventario inventario)
        {
            if (id != inventario.idInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var anterior = await _context.Inventario.AsNoTracking().FirstOrDefaultAsync(i => i.idInventario == id);
                    var valor = inventario.entrada;
                    var valorAn = anterior.entrada;
                    inventario.stock = inventario.stock + inventario.entrada;
                    inventario.entrada = valor + valorAn;
                    
                  


                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.idInventario))
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
            return View(inventario);
        }

        //Fin de entrada

        // GET: Inventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventario == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .FirstOrDefaultAsync(m => m.idInventario == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventario == null)
            {
                return Problem("Entity set 'FransforDbContext.Inventario'  is null.");
            }
            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario != null)
            {
                _context.Inventario.Remove(inventario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(int id)
        {
          return _context.Inventario.Any(e => e.idInventario == id);
        }
    }
}
