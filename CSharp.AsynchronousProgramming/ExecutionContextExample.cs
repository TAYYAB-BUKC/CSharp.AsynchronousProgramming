using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace CSharp.AsynchronousProgramming
{
	public class ExecutionContextExample
	{
		static readonly AsyncLocal<string> asyncLocalData = new();

		public static async void RunExample()
		{
			CultureInfo.CurrentCulture = new CultureInfo("es-ES");
			Thread.CurrentPrincipal = new ClaimsPrincipal();
			asyncLocalData.Value = "Initial Value";

			PrintThreadValues();

			var mainExecutionContext = ExecutionContext.Capture() ?? throw new InvalidOperationException("ExecutionContext only null when");

			var thread = new Thread(() =>
			{
				CultureInfo.CurrentCulture = new CultureInfo("en-UK");
				Thread.CurrentPrincipal = new CustomPrinicipal();
				asyncLocalData.Value = "AsyncLocalData in custom thread";

				Console.WriteLine("Background thread after assigning values");
				PrintThreadValues();

				ExecutionContext.Run(mainExecutionContext, _ =>
				{
					Console.WriteLine("Same Background thread but using Main/Calling Thread ExecutionContext");
					PrintThreadValues();
				}, null);

				Console.WriteLine("Background thread after setting Main/Calling thread ExecutionContext");
				PrintThreadValues();
			});

			thread.Start();

			thread.Join();

			Console.WriteLine("Main thread Values");
			PrintThreadValues();

			await Task.Run(() =>
			{
				Console.WriteLine("Print Values from Task.Run()");
				PrintThreadValues();
			});

			ExecutionContext.SuppressFlow();

			await Task.Run(() =>
			{
				Console.WriteLine("Print Values from Task.Run() With Execution Context Suppressed.");
				PrintThreadValues();
			});

			// Not Working
			//await Task.Run(() =>
			//{
			//	using (ExecutionContext.SuppressFlow())
			//	{
			//		Console.WriteLine("Print Values from Task.Run() With Execution Context Suppressed.");
			//		PrintThreadValues();
			//	}
			//});

			//finally
			//{
			//	//ExecutionContext.RestoreFlow();
			//	//flow.Undo();

			//	// Always undo suppression in a finally
			//	flow.Undo();
			//}

			//ExecutionContext.RestoreFlow();

			//await Task.Run(() =>
			//{
			//	Console.WriteLine("Print Values from Task.Run() With Execution Context Restored.");
			//	PrintThreadValues();
			//});
		}

		private static void PrintThreadValues()
		{
			Console.WriteLine($"Thread ID: {Environment.CurrentManagedThreadId}");
			Console.WriteLine($"Culture Name: {CultureInfo.CurrentCulture.DisplayName}");
			Console.WriteLine($"Principal: {Thread.CurrentPrincipal?.GetType()}");
			Console.WriteLine($"AsyncLocalData: {asyncLocalData.Value}");
			Console.WriteLine($"");
		}
	}

	sealed class CustomPrinicipal() : GenericPrincipal(new CustomIdentity(), null);

	sealed class CustomIdentity() : GenericIdentity("", "");
}