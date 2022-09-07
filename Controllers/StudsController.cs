using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pagingtion.Data;
using Pagingtion.Models;

namespace Pagingtion.Controllers
{
    public class StudsController : Controller
    {
        private readonly DataCont _context;

        public StudsController(DataCont context)
        {
            _context = context;
        }

        // GET: Studs
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            return _context.studs == null ?
                        View(await PaginateList<Stud>.CreateAsync(_context.studs, pageNumber, 5)):
                          Problem("Entity set 'DataCont.studs'  is null.");
        }

        // GET: Studs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.studs == null)
            {
                return NotFound();
            }

            var stud = await _context.studs
                .FirstOrDefaultAsync(m => m.id == id);
            if (stud == null)
            {
                return NotFound();
            }

            return View(stud);
        }

        // GET: Studs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name")] Stud stud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stud);
        }

        // GET: Studs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.studs == null)
            {
                return NotFound();
            }

            var stud = await _context.studs.FindAsync(id);
            if (stud == null)
            {
                return NotFound();
            }
            return View(stud);
        }

        // POST: Studs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name")] Stud stud)
        {
            if (id != stud.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudExists(stud.id))
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
            return View(stud);
        }

        // GET: Studs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.studs == null)
            {
                return NotFound();
            }

            var stud = await _context.studs
                .FirstOrDefaultAsync(m => m.id == id);
            if (stud == null)
            {
                return NotFound();
            }

            return View(stud);
        }

        // POST: Studs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.studs == null)
            {
                return Problem("Entity set 'DataCont.studs'  is null.");
            }
            var stud = await _context.studs.FindAsync(id);
            if (stud != null)
            {
                _context.studs.Remove(stud);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudExists(int id)
        {
          return (_context.studs?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
