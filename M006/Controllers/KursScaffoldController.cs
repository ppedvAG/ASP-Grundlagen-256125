using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using M006.Models;

namespace M006.Controllers
{
    public class KursScaffoldController : Controller
    {
        private readonly KursDBContext _context;

        public KursScaffoldController(KursDBContext context)
        {
            _context = context;
        }

        // GET: KursScaffold
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kurse.ToListAsync());
        }

        // GET: KursScaffold/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurse = await _context.Kurse
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kurse == null)
            {
                return NotFound();
            }

            return View(kurse);
        }

        // GET: KursScaffold/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KursScaffold/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,KursName,DauerInTagen,Aktiv")] Kurse kurse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kurse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kurse);
        }

        // GET: KursScaffold/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurse = await _context.Kurse.FindAsync(id);
            if (kurse == null)
            {
                return NotFound();
            }
            return View(kurse);
        }

        // POST: KursScaffold/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,KursName,DauerInTagen,Aktiv")] Kurse kurse)
        {
            if (id != kurse.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KurseExists(kurse.ID))
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
            return View(kurse);
        }

        // GET: KursScaffold/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurse = await _context.Kurse
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kurse == null)
            {
                return NotFound();
            }

            return View(kurse);
        }

        // POST: KursScaffold/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kurse = await _context.Kurse.FindAsync(id);
            if (kurse != null)
            {
                _context.Kurse.Remove(kurse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KurseExists(int id)
        {
            return _context.Kurse.Any(e => e.ID == id);
        }
    }
}
