using Electra.Controllers;
using Electra.Infrastructure;
using System;

namespace Electra
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = Bootstrapper.InitializeContainer();
			var leapMouseController = container.Resolve<LeapMouseController>();

			//// Have the sample listener receive events from the controller
			leapMouseController.Init();

			// Keep this process running until Enter is pressed
			Console.WriteLine("Press Enter to quit...");
			Console.ReadLine();

			// Remove the sample listener when done
			leapMouseController.Finalize();
			
		}
	}
}
