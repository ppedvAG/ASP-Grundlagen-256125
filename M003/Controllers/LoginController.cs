using M003.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003.Controllers;

public class LoginController(List<User> users) : Controller
{
	[HttpGet]
	public IActionResult Index(User u)
	{
		if (u.UserName == null)
			return View();
		return View(u);
	}

	public IActionResult Einloggen() => View();

	public IActionResult Registrieren() => View();

	[HttpPost]
	public IActionResult CreateUser(string user, string pw)
	{
		if (users.Any(e => e.UserName == user))
			return BadRequest();

		User u = new User
		{
			UserName = user,
			Password = pw
		};

		users.Add(u);

		//Redirect: Leite den User auf eine andere Page weiter
		//Wichtig wegen URL
		return RedirectToAction("Index"); //Führt die Methode "Index" im Controller aus
	}

	[HttpPost]
	public IActionResult LoginUser(string user, string pw)
	{
		User foundUser = users.FirstOrDefault(u => u.UserName == user);

		if (foundUser == null)
			return BadRequest();

		if (foundUser.Password != pw)
			return BadRequest();

		//return RedirectToAction("Index", foundUser); //foundUser wird ausgepackt: ?user=Lukas&pw=123
		return View("Index", foundUser); //foundUser wird ausgepackt: ?user=Lukas&pw=123
	}
}