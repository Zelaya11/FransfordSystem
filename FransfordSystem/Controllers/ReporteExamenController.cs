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
    public class ReporteExamenController : Controller
    {
        private readonly FransforDbContext _context;

        public ReporteExamenController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: ReporteExamen
        public async Task<IActionResult> Index()
        {
              return _context.ReporteExamen != null ? 
                          View(await _context.ReporteExamen.ToListAsync()) :
                          Problem("Entity set 'FransforDbContext.ReporteExamen'  is null.");
        }

        // GET: ReporteExamen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReporteExamen == null)
            {
                return NotFound();
            }

            var reporteExamen = await _context.ReporteExamen
                .FirstOrDefaultAsync(m => m.IdReporteExamen == id);
            if (reporteExamen == null)
            {
                return NotFound();
            }

            return View(reporteExamen);
        }

        // GET: ReporteExamen/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated) {
                //Genera lista de clientes
                List<Cliente> clientesLista = new List<Cliente>();
                clientesLista = (from cliente in _context.Cliente select cliente).ToList();
                clientesLista.Insert(0, new Cliente { IdCliente = 0, nombreCliente = "Seleccionar" });
                ViewBag.clienteDeLista = clientesLista;
                

                //Genera lista de examenes

                List<Examen> examenesLista = new List<Examen>();
                examenesLista = (from examen in _context.Examen select examen).ToList();
                examenesLista.Insert(0, new Examen { idExamen = 0, nombreExamen = "Seleccionar" });
                ViewBag.examenDeLista = examenesLista;
                return View();
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

      

        // POST: ReporteExamen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReporteExamen,fechaReporte,idCliente")] ReporteExamen reporteExamen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reporteExamen);
                await _context.SaveChangesAsync();
                return Redirect("~/Resultadoes/Create");
            }
            return View(reporteExamen);
        }

        // GET: ReporteExamen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReporteExamen == null)
            {
                return NotFound();
            }

            var reporteExamen = await _context.ReporteExamen.FindAsync(id);
            if (reporteExamen == null)
            {
                return NotFound();
            }
            return View(reporteExamen);
        }

        // POST: ReporteExamen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReporteExamen,fechaReporte,idCliente")] ReporteExamen reporteExamen)
        {
            if (id != reporteExamen.IdReporteExamen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reporteExamen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteExamenExists(reporteExamen.IdReporteExamen))
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
            return View(reporteExamen);
        }

        // GET: ReporteExamen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReporteExamen == null)
            {
                return NotFound();
            }

            var reporteExamen = await _context.ReporteExamen
                .FirstOrDefaultAsync(m => m.IdReporteExamen == id);
            if (reporteExamen == null)
            {
                return NotFound();
            }

            return View(reporteExamen);
        }

        // POST: ReporteExamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReporteExamen == null)
            {
                return Problem("Entity set 'FransforDbContext.ReporteExamen'  is null.");
            }
            var reporteExamen = await _context.ReporteExamen.FindAsync(id);
            if (reporteExamen != null)
            {
                _context.ReporteExamen.Remove(reporteExamen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReporteExamenExists(int id)
        {
          return (_context.ReporteExamen?.Any(e => e.IdReporteExamen == id)).GetValueOrDefault();
        }
    }
}
