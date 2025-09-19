namespace CSharp.AsynchronousProgramming
{
	public class LifeBeforeAsync
	{
		public static async Task RunExample()
		{
			#region Call Back Hell Example Before Async/Await
			Console.WriteLine("Cooking Started");
			
			var turkey = new Turkey();
			turkey.Cook()
				  .ContinueWith((prevTask) =>
				  {
					  var gravy = new Gravy();
					  gravy.Cook()
						   .ContinueWith((prevTask) =>
						   {
							   Console.WriteLine("Ready to eat");
						   });
				  });
			#endregion
		}
	}
}