﻿using FransfordSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FransfordSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public ActionResult Inicio()
        {
            return View("Home");
        }

        public ActionResult IndexTrabajador()
        {
            return RedirectToAction("Index", "Trabajadores");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}