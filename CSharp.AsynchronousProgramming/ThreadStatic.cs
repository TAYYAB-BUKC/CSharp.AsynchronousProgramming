namespace CSharp.AsynchronousProgramming
{
	public class ThreadStatic
	{
		[ThreadStatic]
		private static int threadSpecificValue;

		public static void RunExample()
		{
			threadSpecificValue = 100;

			Console.WriteLine($"Calling Thread - threadSpecificValue: {threadSpecificValue}");

			Thread thread1 = new Thread(ThreadMethod);
			Thread thread2 = new Thread(ThreadMethod);

			thread1.Start();
			thread2.Start();

			thread1.Join();
			thread2.Join();

			Console.WriteLine($"Calling Thread after threads finished - threadSpecificValue: {threadSpecificValue}");
		}

		public static void ThreadMethod()
		{
			threadSpecificValue = Random.Shared.Next(1, 100);
			Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} - threadSpecificValue: {threadSpecificValue}");
		}
	}
}