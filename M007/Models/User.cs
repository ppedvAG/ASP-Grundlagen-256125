using M007.Validation;
using System.ComponentModel.DataAnnotations;

namespace M007.Models;

public class User
{
	[StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Der Benutzername muss zw. 3 und 15 Zeichen haben!")]
	public string UserName { get; set; }

	[StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Das Passwort muss zw. 3 und 15 Zeichen haben!")]
	[CustomValidation(typeof(PasswordValidator), "Validate", ErrorMessage = "Password muss komplex sein")]
	public string Password { get; set; }
}