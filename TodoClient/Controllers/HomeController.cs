using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using TodoClient.Models;

namespace TodoClient.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger) {
			_logger = logger;
		}

		public async Task<IActionResult> IndexAsync() {
			var url = "https://localhost:7293/todo";

			// WebClient
			var webClient = new WebClient();
			var respose = webClient.DownloadString(url);


			// HttpClient
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(url);
			var jsonString = await response.Content.ReadAsStringAsync();
			//var list = JsonSerializer.Deserialize<List<TodoItemDto>>(jsonString);


			// HttpWebRequest

			// WebRequest
			var webRequest = WebRequest.Create(url);
			var webResponse = webRequest.GetResponse();
			string json;
			using (Stream dataStream = webResponse.GetResponseStream()) {
				// Open the stream using a StreamReader for easy access.
				StreamReader reader = new StreamReader(dataStream);
				// Read the content.
				json = reader.ReadToEnd();
				// Display the content.
				Console.WriteLine(json);
			}



			return View();
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}