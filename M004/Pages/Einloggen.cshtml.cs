using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004.Pages;

public class EinloggenModel(List<User> users) : PageModel
{
	public IActionResult OnGet()
	{
		return Page(); //Alternative zu return View();
	}

	public IActionResult OnPostLoginUser(string user, string pw)
	{
		User foundUser = users.FirstOrDefault(u => u.UserName == user);

		if (foundUser == null)
			return BadRequest();

		if (foundUser.Password != pw)
			return BadRequest();

		return RedirectToPage("Index", foundUser);
	}
}