using M005.Models;
using Microsoft.AspNetCore.Mvc;

namespace M005.Controllers;

/// <summary>
/// Eigener Controller
/// 
/// Login und Registrierungsportal
/// - Controller
/// - Register View
/// - Login View
/// - Container für die User (DI)
/// </summary>
public class LoginController : Controller
{
	private readonly List<User> users;

	public LoginController(List<User> users)
	{
		this.users = users;

		if (!users.Any(e => e.UserName == "Lukas"))
			users.Add(new User() { UserName = "Lukas", Password = "123" });
	}

	[HttpGet]
	public IActionResult Index(User u)
	{
		User theUser = u; //In C# werden Parameter nicht verändert
		string? foundCookie = HttpContext.Request.Cookies["StayLoggedIn"];
		if (foundCookie != null)
		{
			string[] credentials = foundCookie.Split(';');
			theUser = new User() { UserName = credentials[0], Password = credentials[1] };
		}

		if (theUser.UserName == null)
			return View();
		return View(theUser);
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

	/// <summary>
	/// stayLoggedIn: null oder on (hmm)
	/// </summary>
	[HttpPost]
	public IActionResult LoginUser(string user, string pw, string stayLoggedIn)
	{
		User foundUser = users.FirstOrDefault(u => u.UserName == user);

		if (foundUser == null)
			return BadRequest();

		if (foundUser.Password != pw)
			return BadRequest();

		if (stayLoggedIn == "on")
		{
			HttpContext.Response.Cookies.Append("StayLoggedIn", $"{user};{pw}", new CookieOptions() { MaxAge = TimeSpan.MaxValue });
		}
		else
		{
			HttpContext.Response.Cookies.Delete("StayLoggedIn");
		}

		//return RedirectToAction("Index", foundUser); //foundUser wird ausgepackt: ?user=Lukas&pw=123
		return View("Index", foundUser); //foundUser wird ausgepackt: ?user=Lukas&pw=123
	}

	/// <summary>
	/// async kann hier einfach hinzugefügt werden
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Upload(IFormFile dasFile)
	{
		using Stream s = dasFile.OpenReadStream(); //using-Statement: Automatisches Dispose() am Ende der Methode
		using FileStream fs = new FileStream(dasFile.FileName, FileMode.Create);
		await s.CopyToAsync(fs);

		return View("Index");
	}
}