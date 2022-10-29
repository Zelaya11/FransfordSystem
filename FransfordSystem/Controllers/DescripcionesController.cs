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
    public class DescripcionesController : Controller
    {
        private readonly FransforDbContext _context;

        public DescripcionesController(FransforDbContext context)
        {
            _context = context;
        }

        //GET: Descripciones
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return _context.Descripcion != null ?
                            View(await _context.Descripcion.ToListAsync()) :
                            Problem("Entity set 'FransforDbContext.Descripcion'  is null.");
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }

        }

        public async Task<IActionResult> Filtrado(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (id == null || _context.Descripcion == null)
            {
                return NotFound();
            }

            var descripcion = await _context.Descripcion
                .FirstOrDefaultAsync(m => m.idExamen == id);
            ViewBag.Descripciones = await _context.Descripcion.Where(o => o.idExamen == id).ToListAsync();
            if (descripcion == null)
            {
                return Redirect("/Descripciones/Nodescripcion");
            }

            return View(descripcion);
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        public IActionResult Nodescripcion()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }
        }


        // GET: Descripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Descripcion == null)
            {
                return NotFound();
            }

            var descripcion = await _context.Descripcion
                .FirstOrDefaultAsync(m => m.idDescripcion == id);
            
            if (descripcion == null)
            {
                return NotFound();
            }

            return View(descripcion);

            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // GET: Descripciones/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {

                //Genera lista de exámenes
                List<Examen> examenLista = new List<Examen>();
                examenLista = (from examen in _context.Examen select examen).ToList();
                examenLista.Insert(0, new Examen { idExamen = 0, nombreExamen = "Select" });
                ViewBag.examenDeLista = examenLista;

                //Genera lista de unidades
                List<Unidad> unidadLista = new List<Unidad>();
                unidadLista = (from unidad in _context.Unidad select unidad).ToList();
                unidadLista.Insert(0, new Unidad { idUnidad = 0, nombreUnidad = "Seleccionar" });
                ViewBag.unidadDeLista = unidadLista;

                return View();
            }
            else
            {
                return Redirect("../Identity/Account/Login");
            }

        }

        // POST: Descripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idDescripcion,idExamen,descripcionExamen,valorMinimo,valorMaximo,idUnidad")] Descripcion descripcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Examenes");
            }
            return View(descripcion);
        }

        // GET: Descripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Descripcion == null)
            {
                return NotFound();
            }

            var descripcion = await _context.Descripcion.FindAsync(id);
            if (descripcion == null)
            {
                return NotFound();
            }
            return View(descripcion);
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Descripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idDescripcion,idExamen,descripcionExamen,valorMinimo,valorMaximo,idUnidad")] Descripcion descripcion)
        {
            if (id != descripcion.idDescripcion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescripcionExists(descripcion.idDescripcion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("../Filtrado/" + descripcion.idExamen);
            }
            return View(descripcion);
        }

        // GET: Descripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Descripcion == null)
            {
                return NotFound();
            }

            var descripcion = await _context.Descripcion
                .FirstOrDefaultAsync(m => m.idDescripcion == id);
            if (descripcion == null)
            {
                return NotFound();
            }

            return View(descripcion);

            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Descripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Descripcion == null)
            {
                return Problem("Entity set 'FransforDbContext.Descripcion'  is null.");
            }
            var descripcion = await _context.Descripcion.FindAsync(id);
            if (descripcion != null)
            {
                _context.Descripcion.Remove(descripcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexExamen));
        }

        private bool DescripcionExists(int id)
        {
          return (_context.Descripcion?.Any(e => e.idDescripcion == id)).GetValueOrDefault();
        }

        public ActionResult IndexExamen()
        {
            return RedirectToAction("Index", "Examenes");
        }
    }
}
