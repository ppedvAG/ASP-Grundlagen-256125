using M012.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace M012.Controllers;

public class HomeController
(
	UserManager<IdentityUser> um, 
	SignInManager<IdentityUser> sim, 
	RoleManager<IdentityRole> rm
) : Controller
{
	public async Task<IActionResult> Index()
	{
		return View();
	}

	//[Authorize(Roles = "Chef")]
	public IActionResult Privacy()
	{
		return !HttpContext.User.Claims.Any(e => e.Value == "Privacy") ?  Forbid() : View();
	}

	public async Task<IActionResult> LoginUser()
	{
		IdentityUser user = await um.FindByNameAsync("Admin");
		await sim.SignInAsync(user, true);

		return View("Index");
	}

	public async Task<IActionResult> CreateUser()
	{
		////Aufgabe: Admin User erstellen, mit Beispielrechten
		//IdentityUser admin = new IdentityUser("Admin");
		//IdentityUser foundUser = await um.FindByNameAsync(admin.UserName);
		//if (foundUser != null)
		//{
		//	await um.DeleteAsync(foundUser);
		//}

		////Admin in der DB erzeugen
		//await um.CreateAsync(admin);

		IdentityUser admin = await um.RecreateUser("Admin");
		await um.AddPasswordAsync(admin, "123456");

		//Rolle erstellen + Rechte geben
		IdentityRole adminRole = new IdentityRole("Chef");
		IdentityRole foundRole = await rm.FindByNameAsync(adminRole.Name);
		if (foundRole != null)
			await rm.DeleteAsync(adminRole);

		await rm.CreateAsync(adminRole);

		Claim c1 = new Claim("Recht", "Index");
		Claim c2 = new Claim("Recht", "Privacy");
		await rm.AddClaimAsync(adminRole, c1);
		await rm.AddClaimAsync(adminRole, c2);

		await um.AddToRoleAsync(admin, adminRole.Name);

		return View("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}

public static class IdentityExtensions
{
	public static async Task<IdentityUser> RecreateUser(this UserManager<IdentityUser> um, string name)
	{
		IdentityUser admin = new IdentityUser(name);
		IdentityUser? foundUser = await um.FindByNameAsync(admin.UserName);
		if (foundUser != null)
		{
			await um.DeleteAsync(foundUser);
		}

		//Admin in der DB erzeugen
		await um.CreateAsync(admin);

		return admin;
	}
}