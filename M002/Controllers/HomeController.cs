using System.Diagnostics;
using M002.Models;
using Microsoft.AspNetCore.Mvc;

namespace M002.Controllers;

/// <summary>
/// Primary Constructor
/// 
/// Kompakte Schreibweise für Konstruktoren ab C# 12
/// Hier werden keine Felder dediziert definiert, sondern nur die Parameter klassenweit angreifbar gemacht
/// Vorteil: Weniger Code
/// Nachteil: Konstruktor nicht mehr veränderbar
/// </summary>
public class HomeController(ILogger<HomeController> logger, DITest t) : Controller
{
	//private readonly ILogger<HomeController> _logger;

	//private readonly DITest _diTest;

	//public HomeController(ILogger<HomeController> logger, DITest t)
	//{
	//	_logger = logger;
	//	_diTest = t;
	//}

	public IActionResult Index()
	{
		t.Counter++;
		return View();
	}

	public IActionResult Privacy()
	{
		t.Counter++;
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
