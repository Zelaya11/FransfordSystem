using FransfordSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FransfordSystem.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Identity;


namespace FransfordSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly FransforDbContext _context;
        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> userManager,FransforDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            if (!_context.Users.Any())
            {
                var usuario = new Usuario
                {
                    nombreTrabajador = "Edwin",
                    apellidoTrabajador = "Fransford",
                    fechaNacimiento = DateTime.Now,
                    genero = 'M',
                    UserName = "administrador@gmail.com",
                    NormalizedUserName = "ADMINISTRADOR@GMAIL.COM",
                    Email = "administrador@gmail.com",
                    NormalizedEmail = "ADMINISTRADOR@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };
                string pass = "Admin@123";
                var result = await _userManager.CreateAsync(usuario, pass);
                var u = await _userManager.FindByEmailAsync(usuario.Email);
                await _userManager.AddToRoleAsync(u, "Administrador");
            }
            return Redirect("Identity/Account/Login");
            
        }

        public IActionResult Login()
        {

            return View();

        }

        public IActionResult Desarrollo()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return Redirect("Identity/Account/Login");


        }

        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return Redirect("Identity/Account/Login");
        }

        public ActionResult IndexCliente()
        {
            return RedirectToAction("Index", "Clientes");
        }
        public ActionResult IndexReporte()
        {
            return RedirectToAction("Index", "ReporteExamen");
        }
        public ActionResult IndexExamen()
        {
            return RedirectToAction("Index", "Examenes");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult IndexUsuario()
        {
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult MarcarA()
        {
            return RedirectToAction("MarcarA", "Asistencias");
        }

        public ActionResult IndexCita()
        {
            return RedirectToAction("Index", "Citas");
        }


    }
}