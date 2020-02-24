using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ciqual.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Ciqual.Controllers
{
    [Authorize]
    public class AlimentsController : Controller
    {
        private readonly CiqualContext _context;

        public AlimentsController(CiqualContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> ListByFirstLetter(string letter, int page=1)
        {
            IQueryable<Aliment> reqAliments;
            ViewBag.Letter = letter;

            if (letter == null)
            {
                reqAliments =  _context.Aliment.Include(a => a.IdFamilleNavigation);

            } else reqAliments =  _context.Aliment.Where(a => a.Nom.StartsWith(letter)).Include(a => a.IdFamilleNavigation);

            var aliments = await PageItems<Aliment>.CreateAsync(reqAliments, page, 20);
            return View(aliments);
        }

        [AllowAnonymous]
        // GET: Aliments
        public async Task<IActionResult> Index(int? idFamille)
        {
            FamillesAlim famillesAlim = new FamillesAlim();
            List<AlimentConsti> alimentsConsti;
            var familles = await _context.Famille.OrderBy(f => f.Nom).ToListAsync();

            if (idFamille != null)
            {
                alimentsConsti = await _context.Aliment.Where(a => a.IdFamille == idFamille).Select(a => new AlimentConsti(a.IdAliment, a.Nom, a.IdFamille, a.Composition.Count)).ToListAsync();
            } else alimentsConsti = await _context.Aliment.Where(a => a.IdFamille == familles[0].IdFamille).Select(a => new AlimentConsti(a.IdAliment, a.Nom, a.IdFamille, a.Composition.Count)).ToListAsync();

            
            famillesAlim.Aliments = alimentsConsti;
            famillesAlim.Famille = familles;
            idFamille = idFamille != null ? idFamille : 0;
            ViewBag.SelectId = idFamille;

            HttpContext.Session.SetInt32("Famille", (int)idFamille);
;
            return View(famillesAlim);
        }

        [AllowAnonymous]
        // GET: Aliments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliment
                .Include(a => a.IdFamilleNavigation)
                .FirstOrDefaultAsync(m => m.IdAliment == id);
            if (aliment == null)
            {
                return NotFound();
            }

            return View(aliment);
        }

        // GET: Aliments/Create
        public IActionResult Create()
        {

            int? famille = HttpContext.Session.GetInt32("Famille");

            ViewData["IdFamille"] = new SelectList(_context.Famille, "IdFamille", "Nom", famille);
            return View();
        }

        // POST: Aliments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAliment,Nom,IdFamille")] Aliment aliment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aliment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFamille"] = new SelectList(_context.Famille, "IdFamille", "Nom", aliment.IdFamille);
            return View(aliment);
        }

        // GET: Aliments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliment.FindAsync(id);
            if (aliment == null)
            {
                return NotFound();
            }
            ViewData["IdFamille"] = new SelectList(_context.Famille, "IdFamille", "Nom", aliment.IdFamille);
            return View(aliment);
        }

        // POST: Aliments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAliment,Nom,IdFamille")] Aliment aliment)
        {
            if (id != aliment.IdAliment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aliment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlimentExists(aliment.IdAliment))
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
            ViewData["IdFamille"] = new SelectList(_context.Famille, "IdFamille", "Nom", aliment.IdFamille);
            return View(aliment);
        }

        // GET: Aliments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliment
                .Include(a => a.IdFamilleNavigation)
                .FirstOrDefaultAsync(m => m.IdAliment == id);
            if (aliment == null)
            {
                return NotFound();
            }

            return View(aliment);
        }

        // POST: Aliments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aliment = await _context.Aliment.FindAsync(id);
            _context.Aliment.Remove(aliment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlimentExists(int id)
        {
            return _context.Aliment.Any(e => e.IdAliment == id);
        }
    }
}
