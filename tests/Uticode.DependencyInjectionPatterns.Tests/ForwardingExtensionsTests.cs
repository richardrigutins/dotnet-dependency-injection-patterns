using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Patterns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uticode.DependencyInjectionPatterns.Tests.TestServices;

namespace Uticode.DependencyInjectionPatterns.Tests
{
	[TestClass]
	public class ForwardingExtensionsTests
	{
		[TestMethod]
		public void AddSingleton_RegisteredClassForMultipleInterfaces_AllInterfacesAreRegistered()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddSingleton<IFoo, IBar, MultipleInterfacesClass>();

			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();
			var bar = provider.GetService<IBar>();

			Assert.IsNotNull(foo);
			Assert.IsNotNull(bar);
		}

		[TestMethod]
		public void AddSingleton_RegisteredClassForMultipleInterfaces_InstancesAreTheSame()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddSingleton<IFoo, IBar, MultipleInterfacesClass>();

			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();
			var bar = provider.GetService<IBar>();

			Assert.AreEqual(foo, bar);
		}

		[TestMethod]
		public void AddScoped_RegisteredClassForMultipleInterfaces_AllInterfacesAreRegistered()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddScoped<IFoo, IBar, MultipleInterfacesClass>();

			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();
			var bar = provider.GetService<IBar>();

			Assert.IsNotNull(foo);
			Assert.IsNotNull(bar);
		}

		[TestMethod]
		public void AddScoped_RegisteredClassForMultipleInterfaces_InstancesAreTheSame()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddScoped<IFoo, IBar, MultipleInterfacesClass>();

			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();
			var bar = provider.GetService<IBar>();

			Assert.AreEqual(foo, bar);
		}

		[TestMethod]
		public void AddTransient_RegisteredClassForMultipleInterfaces_AllInterfacesAreRegistered()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddTransient<IFoo, IBar, MultipleInterfacesClass>();

			var provider = services.BuildServiceProvider();

			var foo = provider.GetService<IFoo>();
			var bar = provider.GetService<IBar>();

			Assert.IsNotNull(foo);
			Assert.IsNotNull(bar);
		}
	}
}
