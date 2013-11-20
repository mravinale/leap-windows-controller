using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Electra.Infrastructure
{
	public static class Bootstrapper
	{
		public static IWindsorContainer InitializeContainer()
		{
			// Windsor configuration
			var container = new WindsorContainer();
			
			container.Install(FromAssembly.This());

			return container;
		}

		public static void Release(IWindsorContainer container)
		{
			container.Dispose();
		}
	}
}
