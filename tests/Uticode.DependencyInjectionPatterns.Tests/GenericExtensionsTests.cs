using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Patterns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uticode.DependencyInjectionPatterns.Tests.TestServices;
using Uticode.DependencyInjectionPatterns.Tests.Utilities;

namespace Uticode.DependencyInjectionPatterns.Tests
{
	[TestClass]
	public class GenericExtensionsTests
	{
		[DataTestMethod]
		[DataRow(ServiceLifetime.Transient)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Singleton)]
		public void Add_RegisterServiceAndImplementation_ResolvedServiceIsNotNull(ServiceLifetime lifetime)
		{
			IServiceCollection services = new ServiceCollection();
			services.Add<IFoo, Foo>(lifetime);

			ServiceAssert.ServiceIsRegistered<IFoo>(services);
		}

		[DataTestMethod]
		[DataRow(ServiceLifetime.Transient)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Singleton)]
		public void Add_RegisterServiceAndImplementation_ResolvedServiceIsOfImplementationType(ServiceLifetime lifetime)
		{
			IServiceCollection services = new ServiceCollection();
			services.Add<IFoo, Foo>(lifetime);

			var provider = services.BuildServiceProvider();
			var foo = provider.GetService<IFoo>();

			Assert.IsTrue(foo is Foo);
		}

		[DataTestMethod]
		[DataRow(ServiceLifetime.Transient)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Singleton)]
		public void Add_RegisterService_ResolvedServiceIsNotNull(ServiceLifetime lifetime)
		{
			IServiceCollection services = new ServiceCollection();
			services.Add<Foo>(lifetime);

			ServiceAssert.ServiceIsRegistered<Foo>(services);
		}
	}
}
