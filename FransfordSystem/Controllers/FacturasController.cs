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
    public class FacturasController : Controller
    {
        private readonly FransforDbContext _context;

        public FacturasController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Factura.Include(v => v.cliente).ToListAsync());
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Factura == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.Include(c=> c.cliente)
                .FirstOrDefaultAsync(m => m.IdFactura == id);


            if (factura == null)
            {
                return NotFound();
            }

            ViewBag.idFac = id;
            List<FacturaExamen> examFacLista = new List<FacturaExamen>();
            examFacLista = (from facturaexamen in _context.FacturaExamen.Include(f => f.Examen) select facturaexamen).ToList();
            ViewBag.examFacDeLista = examFacLista;
            


            return View(factura);
            }
            else
                {
                    return Redirect("Identity/Account/Login");
                }
            }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Genera lista de clientes
                List<Cliente> clientesLista = new List<Cliente>();
            clientesLista = (from cliente in _context.Cliente select cliente).ToList();
            clientesLista.Insert(0, new Cliente { IdCliente = 0, nombreCliente = "Seleccionar" });
            ViewBag.clienteDeLista = clientesLista;

            return View();
            }
            else
                {
                    return Redirect("Identity/Account/Login");
                }
            }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,fechaFactura,totalFactura,idCliente")] Factura factura)
        {
            var cliente1 = _context.Cliente.Find(factura.idCliente);
            factura.cliente = cliente1;


            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();

                Factura model = new Factura();
                model.IdFactura = factura.IdFactura;
                model.idCliente = factura.idCliente;
                TempData["valorIdFactura"] = factura.IdFactura;
                TempData["valorIdCliente"] = factura.idCliente;
                return Redirect("~/FacturaExamen/Create");


               
            }
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Factura == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,fechaFactura,totalFactura,idCliente")] Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.IdFactura))
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
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Factura == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Factura == null)
            {
                return Problem("Entity set 'FransforDbContext.Factura'  is null.");
            }
            var factura = await _context.Factura.FindAsync(id);
            var facExamen = _context.FacturaExamen.Where(r => r.idFactura == id).ToList();
            if (factura != null)
            {
                if (facExamen != null)
                {
                    _context.FacturaExamen.RemoveRange(facExamen);
                }
                _context.Factura.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
          return _context.Factura.Any(e => e.IdFactura == id);
        }

        public async Task<IActionResult> Informe_Facturas(string? searchString)
        {
            if (User.Identity.IsAuthenticated)
            {

                ViewBag.Clientes = await _context.Cliente.ToListAsync();

                ViewBag.Cliente = await _context.Cliente.Where(c => c.IdCliente != 0).ToListAsync();
                ViewBag.Reportes = await _context.ReporteExamen.Where(rp => rp.IdReporteExamen != 0).ToListAsync();
                ViewBag.Resultados = await _context.Resultado.Where(re => re.IdResultado != 0).ToListAsync();
                ViewBag.Descripcion = await _context.Descripcion.Where(d => d.idDescripcion != 0).ToListAsync();
                ViewBag.Examenes = await _context.Examen.Where(e => e.idExamen != 0).ToListAsync();
                ViewBag.Facturas = await _context.Factura.Where(f => f.IdFactura != 0).ToListAsync();

                //Nuevo código
                ViewData["CurrentFilter"] = searchString;

                var facturas = from s in _context.Factura
                               select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    facturas = facturas.Where(s => s.fechaFactura.ToString().Contains(searchString));
                }
                //Nuevo código


                return View(await facturas.AsNoTracking().ToListAsync());
            }
            return Redirect("Identity/Account/Login");
        }
    }
}
