using System.Text.Json;

namespace CSharp.AsynchronousProgramming
{
	public class AsyncAwaitInActionExample
	{
		public static async Task<object> GetLibraries()
		{
			var API_URL = "";
			HttpClient client = new();
			var response = await client.GetAsync(API_URL);
			response.EnsureSuccessStatusCode();

			var stream = await response.Content.ReadAsStreamAsync();
			var libraries = JsonSerializer.Deserialize<List<string>>(stream); // List<Libraries>

			return libraries ?? throw new Exception("Libraries not found");
		}
	}
}