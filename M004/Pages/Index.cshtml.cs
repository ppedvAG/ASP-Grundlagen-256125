using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
	public User CurrentUser;

	[FromQuery]
	public int ID { get; set; }

	public void OnGet(User u)
	{
		if (u.UserName != null)
			CurrentUser = u;
	}
}
