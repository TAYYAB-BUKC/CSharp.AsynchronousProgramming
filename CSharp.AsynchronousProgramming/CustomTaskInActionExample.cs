namespace CSharp.AsynchronousProgramming
{
	public class CustomTaskInActionExample
	{
		public static void RunExample()
		{
			Console.WriteLine($"Caller Thread Id: {Environment.CurrentManagedThreadId}");

			//CustomTask
			//	.Run(() => Console.WriteLine($"First CustomTask Thread Id: {Environment.CurrentManagedThreadId}"))
			//	.ContinueWith(() => Console.WriteLine($"Second CustomTask Thread Id: {Environment.CurrentManagedThreadId}"));

			//CustomTask.Run(() => Console.WriteLine($"NEW CustomTask Thread Id: {Environment.CurrentManagedThreadId}"));

			var task = CustomTask
						.Run(() =>
						{
							Console.WriteLine($"First CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
						});

			task.ContinueWith(() =>
			{	
				Console.WriteLine($"Second CustomTask Thread Id: {Environment.CurrentManagedThreadId}");

				CustomTask
						.Run(() =>
						{
							Console.WriteLine($"Third Inner CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
						});

			});
		}
	}
}