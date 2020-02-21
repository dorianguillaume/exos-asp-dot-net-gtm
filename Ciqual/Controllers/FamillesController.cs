using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ciqual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ciqual.Controllers
{
    public class FamillesController : Controller
    {
        private readonly CiqualContext _context;

        public FamillesController(CiqualContext context)
        {
            _context = context;
        }
        // GET: Familles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Famille.ToListAsync());
        }
    }
}