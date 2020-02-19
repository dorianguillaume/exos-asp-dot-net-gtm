using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TachesController : Controller
    {
        private readonly TodoListContext _context;

        public TachesController(TodoListContext context)
        {
            _context = context;
        }

        // GET: Taches
        public async Task<IActionResult> Index()
        {
            ViewBag.NbTachesTerminee = _context.Taches.Where(t => t.Terminee == true).Count();
            ViewBag.ListeTache = await _context.Taches.ToListAsync();
            return View(await _context.Taches.ToListAsync());
        }

        // GET: Taches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tache == null)
            {
                return NotFound();
            }

            return View(tache);
        }

        // GET: Taches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DateCreation,DateEcheance,Terminee")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tache);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tache);
        }

        // GET: Taches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches.FindAsync(id);
            if (tache == null)
            {
                return NotFound();
            }
            return View(tache);
        }

        // POST: Taches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DateCreation,DateEcheance,Terminee")] Tache tache)
        {
            if (id != tache.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tache);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacheExists(tache.Id))
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
            return View(tache);
        }

        // GET: Taches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tache == null)
            {
                return NotFound();
            }

            return View(tache);
        }

        // POST: Taches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tache = await _context.Taches.FindAsync(id);
            _context.Taches.Remove(tache);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult InitSaisie()
        {
            return View("CreationGroupee");
        }

        [HttpPost]
        public IActionResult InitSaisie(int nbTaches)
        {
            var taches = new List<Tache>();
            for (int i = 0; i < nbTaches; i++)
            {
                taches.Add(new Tache { DateEcheance = DateTime.Now.AddDays(7) });
            }
            return View("CreationGroupee", taches);
        }

        [HttpPost]
        public async Task<IActionResult> CreationGroupee(List<Tache> taches)
        {
            foreach (var item in taches)
            {
                item.DateCreation = DateTime.Now;
                _context.Taches.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool TacheExists(int id)
        {
            return _context.Taches.Any(e => e.Id == id);
        }
    }
}
