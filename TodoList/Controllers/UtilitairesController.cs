using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class UtilitairesController : Controller
    {
        public IActionResult Index()
        {

            return View(new Calcul { DateInitiale = DateTime.Now, DateFinale = DateTime.Now });
        }

        [ActionName("calculsdates")]
        public IActionResult AjouterJours(Calcul calcul)
        {
            ModelState.Remove("DateFinale");
            calcul.DateFinale = calcul.DateInitiale.AddDays(calcul.Jour);
            //return View("Index", calcul);

            ViewBag.DateFinale = calcul.DateFinale.ToString("dd/MM/yyyy");
            return View("Index");
        }

        public FileResult Download()
        {
            string fileName = "test.txt";
            byte[] fileBytes = System.IO.File.ReadAllBytes($"wwwroot/files/{fileName}");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


    }
}