using Microsoft.AspNetCore.Mvc;

namespace M010.Controllers;

[ApiController]
[Route("api")]
//[Produces("application/json")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
	private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

	[HttpGet]
	[Route("GetWeatherForecast")]
	[Produces("text/csv")]
	public IEnumerable<WeatherForecast> Get()
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}).ToArray();
	}
}
