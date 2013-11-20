using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Electra.Infrastructure
{
	public class LeapMouseInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				 
				AllTypes.FromThisAssembly()
					.Where(type => type.Name.EndsWith("Controller") ||
								   type.Name.EndsWith("Listener") ||
								   type.Name.EndsWith("Executor"))
					.LifestyleTransient()
				);
		}
	}
}
