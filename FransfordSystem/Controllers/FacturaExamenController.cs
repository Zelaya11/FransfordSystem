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
    public class FacturaExamenController : Controller
    {
        private readonly FransforDbContext _context;

        public FacturaExamenController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: FacturaExamen
        public async Task<IActionResult> Index()
        {
              return View(await _context.FacturaExamen.ToListAsync());
        }

        // GET: FacturaExamen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FacturaExamen == null)
            {
                return NotFound();
            }

            var facturaExamen = await _context.FacturaExamen
                .FirstOrDefaultAsync(m => m.idFacEx == id);
            if (facturaExamen == null)
            {
                return NotFound();
            }

            return View(facturaExamen);
        }

        // GET: FacturaExamen/Create
        public IActionResult Create()
        {
            //
            string valor1 = TempData["valorIdFactura"].ToString();
            string valor2 = TempData["valorIdCliente"].ToString();

            //Genera lista de clientes

            List<Examen> examenesLista = new List<Examen>();
            examenesLista = (from examen in _context.Examen select examen).ToList();
            examenesLista.Insert(0, new Examen { idExamen = 0, nombreExamen = "Seleccionar" });
            ViewBag.examenDeLista = examenesLista;
            ViewBag.idFac = valor1;
            ViewBag.idCli = valor2;




            return View();
        }

        // POST: FacturaExamen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idFacEx,idExamen,idFactura,idCliente,precioExamen")] FacturaExamen facturaExamen)
        {

            var factura1 = _context.Factura.Find(facturaExamen.idFactura);
            facturaExamen.Factura = factura1;
            var examen1 = _context.Examen.Find(facturaExamen.idExamen);
            facturaExamen.Examen = examen1;

            if (ModelState.IsValid)
            {

                _context.Add(facturaExamen);
                await _context.SaveChangesAsync();

                TempData["valorIdFactura"] = facturaExamen.idFactura;
                TempData["valorIdCliente"] = facturaExamen.idCliente;

                string valor1 = TempData["valorIdFactura"].ToString();
                string valor2 = TempData["valorIdCliente"].ToString();
                ViewBag.idFac = valor1;
                ViewBag.idCli = valor2;
                List<Examen> examenesLista = new List<Examen>();
                examenesLista = (from examen in _context.Examen select examen).ToList();
                examenesLista.Insert(0, new Examen { idExamen = 0, nombreExamen = "Seleccionar" });
                ViewBag.examenDeLista = examenesLista;


                return View();
            }
            return View(facturaExamen);
        }

        // GET: FacturaExamen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FacturaExamen == null)
            {
                return NotFound();
            }

            var facturaExamen = await _context.FacturaExamen.FindAsync(id);
            if (facturaExamen == null)
            {
                return NotFound();
            }
            return View(facturaExamen);
        }

        // POST: FacturaExamen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idFacEx,idExamen,idFactura,idCliente,precioExamen")] FacturaExamen facturaExamen)
        {
            if (id != facturaExamen.idFacEx)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaExamen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExamenExists(facturaExamen.idFacEx))
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
            return View(facturaExamen);
        }

        // GET: FacturaExamen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacturaExamen == null)
            {
                return NotFound();
            }

            var facturaExamen = await _context.FacturaExamen
                .FirstOrDefaultAsync(m => m.idFacEx == id);
            if (facturaExamen == null)
            {
                return NotFound();
            }

            return View(facturaExamen);
        }

        // POST: FacturaExamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FacturaExamen == null)
            {
                return Problem("Entity set 'FransforDbContext.FacturaExamen'  is null.");
            }
            var facturaExamen = await _context.FacturaExamen.FindAsync(id);
            if (facturaExamen != null)
            {
                _context.FacturaExamen.Remove(facturaExamen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExamenExists(int id)
        {
          return _context.FacturaExamen.Any(e => e.idFacEx == id);
        }
    }
}
