using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Patterns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Uticode.DependencyInjectionPatterns.Tests.TestServices;
using Uticode.DependencyInjectionPatterns.Tests.Utilities;

namespace Uticode.DependencyInjectionPatterns.Tests
{
	[TestClass]
	public class CompositeExtensionsTests
	{
		public List<Type> GetInnerServicesTypes()
		{
			var innerServices = new List<Type>()
			{
				typeof(Foo),
				typeof(MultipleInterfacesClass)
			};

			return innerServices;
		}

		private TService ResolveService<TService>(IServiceCollection services)
		{
			var provider = services.BuildServiceProvider();
			var service = provider.GetRequiredService<TService>();

			return service;
		}

		private void RegisterServices<TService>(IServiceCollection services, IEnumerable<Type> servicesTypes, ServiceLifetime lifetime)
		{
			foreach (var service in servicesTypes)
			{
				services.Add(new ServiceDescriptor(typeof(TService), service, lifetime));
			}
		}

		[DataTestMethod]
		[DataRow(ServiceLifetime.Singleton)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Transient)]
		public void Compose_ComposedServices_ServiceIsRegistered(ServiceLifetime serviceLifetime)
		{
			IServiceCollection services = new ServiceCollection();
			var innerServicesTypes = GetInnerServicesTypes();
			RegisterServices<IFoo>(services, innerServicesTypes, serviceLifetime);

			services.Compose<IFoo, ComposedFoo>();

			ServiceAssert.ServiceIsRegistered<IFoo>(services);
		}

		[DataTestMethod]
		[DataRow(ServiceLifetime.Singleton)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Transient)]
		public void Compose_ComposedServices_ServiceResolvedIsOfComposedType(ServiceLifetime serviceLifetime)
		{
			IServiceCollection services = new ServiceCollection();
			var innerServicesTypes = GetInnerServicesTypes();
			RegisterServices<IFoo>(services, innerServicesTypes, serviceLifetime);

			services.Compose<IFoo, ComposedFoo>();

			var foo = ResolveService<IFoo>(services);

			Assert.IsInstanceOfType(foo, typeof(ComposedFoo));
		}

		[DataTestMethod]
		[DataRow(ServiceLifetime.Singleton)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Transient)]
		public void Compose_ComposedMultipleServices_ResolvedServiceHasListOfServices(ServiceLifetime serviceLifetime)
		{
			IServiceCollection services = new ServiceCollection();
			var innerServicesTypes = GetInnerServicesTypes();
			RegisterServices<IFoo>(services, innerServicesTypes, serviceLifetime);

			services.Compose<IFoo, ComposedFoo>();

			var foo = ResolveService<IFoo>(services) as ComposedFoo;
			var innerServices = foo?.ComposedServices ?? new List<IFoo>();

			CollectionAssert.AreEquivalent(innerServicesTypes, innerServices.Select(s => s.GetType()).ToList());
		}

		[DataTestMethod]
		[DataRow(ServiceLifetime.Singleton)]
		[DataRow(ServiceLifetime.Scoped)]
		[DataRow(ServiceLifetime.Transient)]
		public void Compose_OnlyOneComposedServices_ResolvedServiceHasInnerService(ServiceLifetime serviceLifetime)
		{
			IServiceCollection services = new ServiceCollection();
			var innerServicesTypes = new List<Type>() { typeof(Foo) };
			RegisterServices<IFoo>(services, innerServicesTypes, serviceLifetime);

			services.Compose<IFoo, ComposedFoo>();

			var foo = ResolveService<IFoo>(services) as ComposedFoo;
			var innerServices = foo?.ComposedServices ?? new List<IFoo>();

			CollectionAssert.AreEquivalent(innerServicesTypes, innerServices.Select(s => s.GetType()).ToList());
		}

		[TestMethod]
		public void Compose_ServicesNotRegistered_ThrowsException()
		{
			IServiceCollection services = new ServiceCollection();

			Action composeAction = () => services.Compose<IFoo, ComposedFoo>();

			Assert.ThrowsException<ArgumentException>(composeAction);
		}
	}
}