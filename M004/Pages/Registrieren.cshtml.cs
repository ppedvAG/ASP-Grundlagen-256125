using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004.Pages;

/// <summary>
/// WICHTIG: Bei Razor Pages ist das Model immer die Klasse hinter der View
/// 
/// Hier können auch ohne Probleme Variablen angelegt werden
/// </summary>
public class RegistrierenModel(List<User> users) : PageModel
{
	public IActionResult OnPostCreateUser(string user, string pw)
	{
		if (users.Any(e => e.UserName == user))
			return BadRequest();

		User u = new User
		{
			UserName = user,
			Password = pw
		};

		users.Add(u);

		return RedirectToPage("Index"); //WICHTIG: RedirectToPage statt Action
	}
}