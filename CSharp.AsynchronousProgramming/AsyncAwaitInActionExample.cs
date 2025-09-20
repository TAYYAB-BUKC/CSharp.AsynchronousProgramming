using System.Text.Json;

namespace CSharp.AsynchronousProgramming
{
	public class AsyncAwaitInActionExample
	{
		/// <summary>
		/// 1. Caller Thread enters GetLibraries method.
		/// 2. Caller Thread executes until line 24 when we hit the first await (GetAsync).
		/// 3. Caller Thread does not wait here; it returns to the calling line of GetLibraries.
		/// 4. GetAsync starts an asynchronous I/O operation. Once it completes, the Caller Thread is notified to resume execution.
		/// 5. Caller Thread resumes and executes until line 27 when we hit the next await (ReadAsStreamAsync).
		/// 6. Caller Thread does not wait here; it returns to the calling line of GetLibraries.
		/// 7. ReadAsStreamAsync starts another asynchronous I/O operation. Once it completes, the Caller Thread is notified to resume execution.
		/// 8. Caller Thread resumes and executes until line 28 when we hit the next await (DeserializeAsync).
		/// 9. Caller Thread does not wait here; it returns to the calling line of GetLibraries.
		/// 10. DeserializeAsync performs JSON deserialization (may use background threads for CPU work). Once done, the Caller Thread is notified to resume execution.
		/// 11. Caller Thread resumes and executes line 30, either returning the libraries or throwing an exception if libraries are null.
		/// </summary>
		public static async Task<object> GetLibraries()
		{
			var API_URL = "";
			HttpClient client = new();
			var response = await client.GetAsync(API_URL);
			response.EnsureSuccessStatusCode();

			var stream = await response.Content.ReadAsStreamAsync();
			var libraries = await JsonSerializer.DeserializeAsync<List<string>>(stream); // List<Libraries>

			return libraries ?? throw new Exception("Libraries not found");
		}
	}
}