using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers {
	[ApiController]
	//[Route("[controller]")]
	[Route("weather-forecast")]
	[Route("forecast")]

	// WeatherForecast - PascalCase
	// weatherForecast - camelCase
	// weather_forecast - snake_case
	// weather-forecast - kabab-case
	// weatherforecast - lowercase

	public class WeatherForecastController : ControllerBase {
		private static readonly string[] Summaries = new[] {
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger) {
			_logger = logger;
		}

		// GET weather-forecast/random/2?name=HOT
		// GET weather-forecast/random/2
		// GET weather-forecast/random
		// GET weather-forecast/random/liron - BAD!
		[HttpGet("random/{count:int?}")]
		public IEnumerable<WeatherForecast> GetRandom(
				[FromRoute] int? count,
				[FromQuery] string name = null
			) {
			_logger.LogInformation("WeatherForecast.Get was invoked");

			return
				Enumerable.Range(1, count ?? 5)
				.Select(index => new WeatherForecast {
					Date = DateTime.Now.AddDays(index),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = name ?? Summaries[Random.Shared.Next(Summaries.Length)]
				})
			.ToArray();
		}

		[HttpGet("random/{value}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public JsonResult GetValue(string value) {
			return new JsonResult($"GetValue: {value}");
		}
	}
}