using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M000.Models;

namespace M009.Controllers;

[Route("Kurs")]
public class KursScaffoldController(KursDBContext context) : Controller
{
	// GET: KursScaffold
	[Route("Index")]
	public async Task<IActionResult> Index()
	{
		return View(await context.Kurse.ToListAsync());
	}

	// GET: KursScaffold/Details/5
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var kurse = await context.Kurse
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
			context.Add(kurse);
			await context.SaveChangesAsync();
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

		var kurse = await context.Kurse.FindAsync(id);
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
				context.Update(kurse);
				await context.SaveChangesAsync();
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

		var kurse = await context.Kurse
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
		var kurse = await context.Kurse.FindAsync(id);
		if (kurse != null)
		{
			context.Kurse.Remove(kurse);
		}

		await context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool KurseExists(int id)
	{
		return context.Kurse.Any(e => e.ID == id);
	}
}
