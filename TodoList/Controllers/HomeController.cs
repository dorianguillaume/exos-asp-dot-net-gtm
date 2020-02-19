using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        const string compteur = "_compteur";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        [ActionName("ContactUs")]
        public IActionResult Contact(int id, [FromQuery] string nom)
        {
            ViewData["Message"] = "Bonjour "+nom+", ton identifiant est " + id.ToString();
            return View("Contact");
        }

        public IActionResult About()
        {
            int? nbVisite = HttpContext.Session.GetInt32(compteur);

            if (nbVisite == null)
            {
                HttpContext.Session.SetInt32(compteur, 1);
            }
            else HttpContext.Session.SetInt32(compteur, (int)++nbVisite);

            nbVisite = HttpContext.Session.GetInt32(compteur);

            ViewData["Message"] = "Vous avez visité cette page :" + nbVisite;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
