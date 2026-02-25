using M009.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M009.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
	public IActionResult Index()
	{
		ViewData["Test"] = 123;
		ViewBag.Test = 123;
		return View();
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
