using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FransfordSystem;
using FransfordSystem.Models;
using FransfordSystem.ViewModels;

namespace FransfordSystem.Controllers
{
    public class AsistenciasController : Controller
    {
        private readonly FransforDbContext _context;

        public AsistenciasController(FransforDbContext context)
        {
            _context = context;
        }

        // GET: Asistencias
        public async Task<IActionResult> Index(string searchString)
        {
            List<Usuario> usuarioLista = new List<Usuario>();
            usuarioLista = (from usuario in _context.Usuario select usuario).ToList();
            ViewBag.usuarioDeLista = usuarioLista;


            var asistencia = from a in _context.Asistencia select a;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                asistencia = asistencia.Where(s => s.idUsuario!.Contains(searchString));
            }

            return View(await asistencia.ToListAsync());
            

            return _context.Asistencia != null ?
                        View(await _context.Asistencia.ToListAsync()) :
                        Problem("Entity set 'FransforDbContext.Asistencia'  is null.");

        }

        public async Task<IActionResult> MarcarA()
        {
            List<Usuario> usuarioLista = new List<Usuario>();
            usuarioLista = (from usuario in _context.Usuario select usuario).ToList();
            ViewBag.usuarioDeLista = usuarioLista;

            return _context.Asistencia != null ?
                        View(await _context.Asistencia.ToListAsync()) :
                        Problem("Entity set 'FransforDbContext.Asistencia'  is null.");
        }

        // GET: Asistencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            List<Usuario> usuarioLista = new List<Usuario>();
            usuarioLista = (from usuario in _context.Usuario select usuario).ToList();
            ViewBag.usuarioDeLista = usuarioLista;

            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencia
                .FirstOrDefaultAsync(m => m.idAsistencia == id);
            ViewBag.Asistencias = await _context.Asistencia.Where(o => o.idAsistencia == id).ToListAsync();
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // GET: Asistencias/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Genera lista de trabajadores
                List<Usuario> usuarioLista = new List<Usuario>();
                usuarioLista = (from usuario in _context.Usuario select usuario).ToList();
                usuarioLista.Insert(0, new Usuario { Id = "", nombreTrabajador = "Seleccionar" });
                ViewBag.usuarioDeLista = usuarioLista;
                return View();
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Asistencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idAsistencia,idUsuario,horaEntrada,horaSalida")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MarcarA));
            }
            return View(asistencia);
        }

        // GET: Asistencias/Edit/5
        public async Task<IActionResult> Salida(int? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencia.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            //Genera lista de trabajadores
            ViewBag.Asistencias = await _context.Asistencia.Where(o => o.idAsistencia == id).ToListAsync();


            List<Usuario> usuarioLista = new List<Usuario>();
            usuarioLista = (from usuario in _context.Usuario select usuario).ToList();
            usuarioLista.Select(x => x.nombreTrabajador).ToString();
            ViewBag.usuarioDeLista = usuarioLista;

            return View(asistencia);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salida(int id, [Bind("idAsistencia,idUsuario,horaEntrada,horaSalida")] Asistencia asistencia)
        {
            if (id != asistencia.idAsistencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(asistencia.idAsistencia))
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
            return View(asistencia);
        }

        // GET: Asistencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            List<Usuario> usuarioLista = new List<Usuario>();
            usuarioLista = (from usuario in _context.Usuario select usuario).ToList();
            ViewBag.usuarioDeLista = usuarioLista;

            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencia
                .FirstOrDefaultAsync(m => m.idAsistencia == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asistencia == null)
            {
                return Problem("Entity set 'FransforDbContext.Asistencia'  is null.");
            }
            var asistencia = await _context.Asistencia.FindAsync(id);
            if (asistencia != null)
            {
                _context.Asistencia.Remove(asistencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaExists(int id)
        {
            return (_context.Asistencia?.Any(e => e.idAsistencia == id)).GetValueOrDefault();
        }
    }
}
