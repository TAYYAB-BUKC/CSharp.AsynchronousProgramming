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

			#region Without Wait
			//var task = CustomTask
			//			.Run(() =>
			//			{
			//				Console.WriteLine($"First CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
			//			});

			//task.ContinueWith(() =>
			//{
			//	Console.WriteLine($"Second CustomTask Thread Id: {Environment.CurrentManagedThreadId}");

			//	CustomTask
			//			.Run(() =>
			//			{
			//				Console.WriteLine($"Third Inner CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
			//			});
			//});
			#endregion

			#region With Wait
			//CustomTask.Run(() =>
			//			{
			//				Console.WriteLine($"First CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
			//			}).Wait();

			//Console.WriteLine($"Second CustomTask Thread Id: {Environment.CurrentManagedThreadId}");

			//CustomTask.Run(() =>
			//			{
			//				Console.WriteLine($"Third Inner CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
			//			}).Wait();
			#endregion

			#region With Wait And Delay
			CustomTask.Run(() =>
			{
				Console.WriteLine($"First CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
			}).Wait();

			CustomTask.Delay(TimeSpan.FromSeconds(3)).Wait();

			Console.WriteLine($"Second CustomTask Thread Id: {Environment.CurrentManagedThreadId}");

			CustomTask.Delay(TimeSpan.FromSeconds(3)).Wait();

			CustomTask.Run(() =>
			{
				Console.WriteLine($"Third Inner CustomTask Thread Id: {Environment.CurrentManagedThreadId}");
			}).Wait();
			#endregion
		}
	}
}