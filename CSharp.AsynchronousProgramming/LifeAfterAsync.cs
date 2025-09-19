namespace CSharp.AsynchronousProgramming
{
	public class LifeAfterAsync
	{
		public static async Task RunExample()
		{
			Console.WriteLine("Cooking Started");

			var turkey = new Turkey();
			await turkey.Cook();

			var gravy = new Gravy();
			await gravy.Cook();

			Console.WriteLine("Ready to eat");
		}
	}
}