using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uticode.DependencyInjectionPatterns.Tests.Utilities
{
	public static class ServiceAssert
	{
		public static void ServiceIsRegistered<TService>(IServiceCollection services)
		{
			var provider = services.BuildServiceProvider();
			var service = provider.GetService<TService>();

			Assert.IsNotNull(service);
		}
	}
}
