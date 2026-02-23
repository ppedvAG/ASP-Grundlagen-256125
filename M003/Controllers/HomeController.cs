using M003.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003.Controllers;

/// <summary>
/// Eigener Controller
/// 
/// Login und Registrierungsportal
/// - Controller
/// - Register View
/// - Login View
/// - Container für die User (DI)
/// </summary>
public class HomeController(ILogger<HomeController> logger) : Controller
{
	/// <summary>
	/// IActionResult
	/// 
	/// Ein HTTP-Ergebnis (Daten, View, Error Code, ...)
	/// </summary>
	public IActionResult Index()
	{
		return View(); //Leitet weiter auf Views/Home/Index.cshtml

		//return BadRequest();
		//return Forbid();
		//return NotFound();
		//return StatusCode(123);
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
