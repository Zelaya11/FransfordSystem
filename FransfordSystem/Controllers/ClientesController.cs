﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FransfordSystem;
using FransfordSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace FransfordSystem.Controllers
{
    public class ClientesController : Controller
    {
        private readonly FransforDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        
        public ClientesController(FransforDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                return _context.Cliente != null ?
                              View(await _context.Cliente.ToListAsync()) :
                              Problem("Entity set 'FransforDbContext.Cliente'  is null.");
            }
            return Redirect("Identity/Account/Login");

        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return  View(cliente);

            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserId(HttpContext.User);
                return View();
            }
            return Redirect("Identity/Account/Login");

        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,UserName,nombreCliente,apellidoCliente,fechaNacimiento,genero,telefono,dui")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,UserName,nombreCliente,apellidoCliente,fechaNacimiento,genero,telefono,dui")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);

            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'FransforDbContext.Cliente'  is null.");
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Estadísticas de clientes
        public async Task<IActionResult> Estadisticas_clientes()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Clientes = await _context.Cliente.ToListAsync();

                ViewBag.Cliente = await _context.Cliente.Where(c => c.IdCliente != 0).ToListAsync();
                ViewBag.Reportes = await _context.ReporteExamen.Where(rp => rp.IdReporteExamen != 0).ToListAsync();
                ViewBag.Resultados = await _context.Resultado.Where(re => re.IdResultado != 0).ToListAsync();
                ViewBag.Descripcion = await _context.Descripcion.Where(d => d.idDescripcion != 0).ToListAsync();
                ViewBag.Examenes = await _context.Examen.Where(e => e.idExamen != 0).ToListAsync();

                /*
                var Resultado = _context.Resultado;
                var cliente = _context.Cliente;
                ViewBag.Resultado = await _context.Resultado.Where(o => o. == id).ToListAsync();
                ViewBag.ReporteExamen = await _context.ReporteExamen.Where(o => o. == id).ToListAsync();
                ViewBag.ReporteExamen = await _context.ReporteExamen.Where(o => o.idCliente == cliente.).ToListAsync();
                */


                return _context.Cliente != null ?
                              View(await _context.Cliente.ToListAsync()) :
                              Problem("Entity set 'FransforDbContext.Cliente'  is null.");
            }
            return Redirect("Identity/Account/Login");

        }

        private bool ClienteExists(int id)
        {
          return (_context.Cliente?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
