using System.ComponentModel.DataAnnotations;

namespace M007.Validation;

public class PasswordValidator : ValidationAttribute
{
	public static ValidationResult Validate(object value)
	{
		string? s = value as string;

		if (s == null)
			return null;

		if (s.Length > 3 && s.Any(char.IsNumber) && s.Any(char.IsLower) && s.Any(char.IsUpper))
		{
			return null;
		}
		else
		{
			return new ValidationResult("Das Passwort muss komplex sein");
		}
	}
}
