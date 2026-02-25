using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M000.Models;

namespace M010.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KursesController : ControllerBase
{
	private readonly KursDBContext _context;

	public KursesController(KursDBContext context)
	{
		_context = context;
	}

	// GET: api/Kurses
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Kurse>>> GetKurse()
	{
		return await _context.Kurse.ToListAsync();
	}

	// GET: api/Kurses/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Kurse>> GetKurse(int id)
	{
		var kurse = await _context.Kurse.FindAsync(id);

		if (kurse == null)
		{
			return NotFound();
		}

		return kurse;
	}

	// PUT: api/Kurses/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutKurse(int id, Kurse kurse)
	{
		if (id != kurse.ID)
		{
			return BadRequest();
		}

		_context.Entry(kurse).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!KurseExists(id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	// POST: api/Kurses
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<Kurse>> PostKurse(Kurse kurse)
	{
		_context.Kurse.Add(kurse);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetKurse", new { id = kurse.ID }, kurse);
	}

	// DELETE: api/Kurses/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteKurse(int id)
	{
		var kurse = await _context.Kurse.FindAsync(id);
		if (kurse == null)
		{
			return NotFound();
		}

		_context.Kurse.Remove(kurse);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool KurseExists(int id)
	{
		return _context.Kurse.Any(e => e.ID == id);
	}
}
