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
    public class ResultadoesController : Controller
    {
        private readonly FransforDbContext _context;

        public ResultadoesController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: Resultadoes
        public async Task<IActionResult> Index()
        {
              return _context.Resultado != null ? 
                          View(await _context.Resultado.ToListAsync()) :
                          Problem("Entity set 'FransforDbContext.Resultado'  is null.");
        }

        // GET: Resultadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resultado == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultado
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // GET: Resultadoes/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Genera lista de clientes
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

        // POST: Resultadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResultado,resultadoExamen,idDescripcion,idReporteExamen")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resultado);
        }

        // GET: Resultadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resultado == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultado.FindAsync(id);
            if (resultado == null)
            {
                return NotFound();
            }
            return View(resultado);
        }

        // POST: Resultadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResultado,resultadoExamen,idDescripcion,idReporteExamen")] Resultado resultado)
        {
            if (id != resultado.IdResultado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadoExists(resultado.IdResultado))
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
            return View(resultado);
        }

        // GET: Resultadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resultado == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultado
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // POST: Resultadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resultado == null)
            {
                return Problem("Entity set 'FransforDbContext.Resultado'  is null.");
            }
            var resultado = await _context.Resultado.FindAsync(id);
            if (resultado != null)
            {
                _context.Resultado.Remove(resultado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadoExists(int id)
        {
          return (_context.Resultado?.Any(e => e.IdResultado == id)).GetValueOrDefault();
        }
    }
}
