using M008.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;

namespace M008.Controllers;

public class HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> loc) : Controller
{
	public IActionResult Index()
	{
		string? lang = HttpContext.Request.Cookies["Lang"];
		CultureInfo.CurrentCulture = new CultureInfo(lang ?? "en");
		CultureInfo.CurrentUICulture = new CultureInfo(lang ?? "en");

		LocalizedString str = loc["Test"];
		return View("Index", str.Value);
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

	[HttpPost]
	public IActionResult SetLanguage(string languageCode, string returnUrl)
	{
		CultureInfo.CurrentCulture = new CultureInfo(languageCode);
		CultureInfo.CurrentUICulture = new CultureInfo(languageCode);
		HttpContext.Response.Cookies.Append("Lang", languageCode, new CookieOptions() { MaxAge = TimeSpan.FromDays(1) });

		return RedirectToAction(returnUrl);
	}
}