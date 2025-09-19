namespace CSharp.AsynchronousProgramming
{
	public class ParallelProgramming
	{
		public static async Task RunExample()
		{
			Console.WriteLine("Cooking Started");

			var turkey = new Turkey();
			var gravy = new Gravy();

			await Task.WhenAll(turkey.Cook(), gravy.Cook());
			
			Console.WriteLine("Ready to eat");
		}
	}
}