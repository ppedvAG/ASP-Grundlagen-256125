using Microsoft.Extensions.Primitives;

namespace M008.Middleware;

public class RequestLocalizationMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		string language = "en"; //Standard, wenn der Accept-Language Header nicht mitgesendet wird

		//Header
		StringValues sv = context.Request.Headers["Accept-Language"];
		if (sv.Count != 0)
		{
			string l = sv.ToString().Split(";")[0];

			string[] languages = l.Split(",");

			language = languages[0];
		}

		//Cookies
		string? lang = context.Request.Cookies["Lang"];
		if (lang != null)
		{
			language = lang;
		}

		//Sprache zwischenspeichern
		//In einem Cookie, Session, Query Parameter, Route, ...
		context.Response.Cookies.Append("Lang", language, new CookieOptions() { MaxAge = TimeSpan.FromDays(1) });

		await next(context);
	}
}