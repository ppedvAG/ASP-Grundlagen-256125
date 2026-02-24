using M007.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M007.Controllers;

/// <summary>
/// Model Binding
/// Daten empfangen und diese sofort auf der Page anzeigen
/// </summary>
public class HomeController(ILogger<HomeController> logger) : Controller
{
	/// <summary>
	/// Die From Attribute können auch Klassenvariablen attributieren
	/// Wird jetzt bei jeder Methode gefangen
	/// 
	/// Bei mehreren Attributen wird das oberste priorisiert
	/// </summary>
	[FromHeader]
	[FromQuery]
	public string Sprache { get; set; }

	[HttpGet]
	public IActionResult Index([FromQuery] int zahl)
	{
		return View(zahl);
	}

	/// <summary>
	/// Bind
	/// 
	/// Ermöglicht das Mapping von Daten in ein Objekt hinein
	/// z.B.: API
	/// </summary>
	public IActionResult Privacy([Bind] User u)
	{
		return View(u);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
