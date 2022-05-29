using System.Net;

var url = "https://localhost:7293/todo";

// WebClient
var webClient = new WebClient();
var respose = webClient.DownloadString(url);
Console.WriteLine(respose);
Console.WriteLine("========");

// HttpClient
var httpClient = new HttpClient();
var response = await httpClient.GetAsync(url);
var jsonString = await response.Content.ReadAsStringAsync();
//var list = JsonSerializer.Deserialize<List<TodoItemDto>>(jsonString);
Console.WriteLine(jsonString);
Console.WriteLine("========");

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
}
Console.WriteLine(json);
Console.WriteLine("========");
