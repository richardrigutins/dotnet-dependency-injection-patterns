using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Patterns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uticode.DependencyInjectionPatterns.Tests.TestServices;
using Uticode.DependencyInjectionPatterns.Tests.Utilities;

namespace Uticode.DependencyInjectionPatterns.Tests
{
	[TestClass]
	public class DecoratorExtensionsTests
	{
		[TestMethod]
		public void Decorate_DecoratedSingletonService_ResolvedServiceIsNotNull()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddSingleton<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();

			ServiceAssert.ServiceIsRegistered<IFoo>(services);
		}

		[TestMethod]
		public void Decorate_DecoratedScopedService_ResolvedServiceIsNotNull()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddScoped<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();

			ServiceAssert.ServiceIsRegistered<IFoo>(services);
		}

		[TestMethod]
		public void Decorate_DecoratedTransientService_ResolvedServiceIsNotNull()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddTransient<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();

			ServiceAssert.ServiceIsRegistered<IFoo>(services);
		}

		[TestMethod]
		public void Decorate_DecoratedSingletonService_ResolvedServiceIsOfDecoratorType()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddSingleton<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();

			CheckResolvedServiceIsOfDecoratorType(services);
		}

		[TestMethod]
		public void Decorate_DecoratedScopedService_ResolvedServiceIsOfDecoratorType()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddScoped<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();

			CheckResolvedServiceIsOfDecoratorType(services);
		}

		[TestMethod]
		public void Decorate_DecoratedTransientService_ResolvedServiceIsOfDecoratorType()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddTransient<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();

			CheckResolvedServiceIsOfDecoratorType(services);
		}

		private void CheckResolvedServiceIsOfDecoratorType(IServiceCollection services)
		{
			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();

			Assert.IsTrue(foo is DecoratorFoo);
		}

		[TestMethod]
		public void Decorate_DecoratedSingletonService_ResolvedServiceHasInnerService()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddSingleton<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();
			CheckResolvedServiceHasInnerService(services);
		}

		[TestMethod]
		public void Decorate_DecoratedScopedService_ResolvedServiceHasInnerService()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddScoped<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();
			CheckResolvedServiceHasInnerService(services);
		}

		[TestMethod]
		public void Decorate_DecoratedTransientService_ResolvedServiceHasInnerService()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddTransient<IFoo, Foo>();
			services.Decorate<IFoo, DecoratorFoo>();
			CheckResolvedServiceHasInnerService(services);
		}

		private void CheckResolvedServiceHasInnerService(IServiceCollection services)
		{
			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();
			var decorator = foo as DecoratorFoo;
			var inner = decorator?.Inner;

			Assert.IsNotNull(inner);
			Assert.IsTrue(inner is Foo);
			Assert.IsFalse(inner is DecoratorFoo);
		}
	}
}
