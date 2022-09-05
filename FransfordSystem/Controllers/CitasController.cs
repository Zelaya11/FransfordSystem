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
    public class CitasController : Controller
    {
        private readonly FransforDbContext _context;

        public CitasController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
              return _context.Cita != null ? 
                          View(await _context.Cita.ToListAsync()) :
                          Problem("Entity set 'FransforDbContext.Cita'  is null.");
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .FirstOrDefaultAsync(m => m.idCita == id);

            ViewBag.Clientes = await _context.Cliente.Where(o => o.IdCliente == id).ToListAsync();
            //Genera lista de clientes
            List<Cliente> clienteLista = new List<Cliente>();
            clienteLista = (from cliente in _context.Cliente select cliente).ToList();
            clienteLista.Insert(0, new Cliente { IdCliente = 0, nombreCliente = "Seleccionar" });
            ViewBag.clienteDeLista = clienteLista;

            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Genera lista de clientes
                List<Cliente> clienteLista = new List<Cliente>();
                clienteLista = (from cliente in _context.Cliente select cliente).ToList();
                clienteLista.Insert(0, new Cliente { IdCliente = 0, nombreCliente = "Seleccionar" });
                ViewBag.clienteDeLista = clienteLista;
                return View();
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCita,idCliente,nombreEmpresa,fechaCita,horaCita")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Cita == null)
                {
                    return NotFound();
                }

                var cita = await _context.Cita.FindAsync(id);
                if (cita == null)
                {
                    return NotFound();
                }

                //Genera lista de clientes
                List<Cliente> clienteLista = new List<Cliente>();
                clienteLista = (from cliente in _context.Cliente select cliente).ToList();
                clienteLista.Insert(0, new Cliente { IdCliente = 0, nombreCliente = "Seleccionar" });
                ViewBag.clienteDeLista = clienteLista;
                
                return View(cita);
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idCita,idCliente,nombreEmpresa,fechaCita,horaCita")] Cita cita)
        {
            if (id != cita.idCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.idCita))
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
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .FirstOrDefaultAsync(m => m.idCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cita == null)
            {
                return Problem("Entity set 'FransforDbContext.Cita'  is null.");
            }
            var cita = await _context.Cita.FindAsync(id);
            if (cita != null)
            {
                _context.Cita.Remove(cita);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
          return (_context.Cita?.Any(e => e.idCita == id)).GetValueOrDefault();
        }
    }
}
