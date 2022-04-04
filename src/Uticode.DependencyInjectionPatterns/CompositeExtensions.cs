using System;
using System.Collections.Generic;
using System.Linq;
using Uticode.DependencyInjectionPatterns.Common;

namespace Microsoft.Extensions.DependencyInjection.Patterns
{
	public static class CompositeExtensions
	{
		/// <summary>
		/// Adds a service of the type specified in <typeparamref name="TService"/> 
		/// with a composite implementation type specified in <typeparamref name="TImplementation"/> 
		/// to the specified <typeparamref name="IServiceCollection"/>,
		/// composing all the existing types registered for <typeparamref name="TService"/>
		/// and using them as a dependency for <typeparamref name="TImplementation"/>;
		/// the scope of <typeparamref name="TService"/> is determined from the most specific scope of those types
		/// </summary>
		/// <typeparam name="TService">The type of the service to add.</typeparam>
		/// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
		/// <param name="services">The <typeparamref name="IServiceCollection"/> to add the service to.</param>
		/// <exception cref="ArgumentException"></exception>
		public static IServiceCollection Compose<TService, TImplementation>(this IServiceCollection services)
			where TService : class
			where TImplementation : class, TService
		{
			if (!services.IsServiceRegistered<TService>())
			{
				throw new ArgumentException($"Cannot find any type registered for service {typeof(TService).Name}");
			}

			var wrappedDescriptors = services.GetRegisteredServices<TService>();
			foreach (var descriptor in wrappedDescriptors)
			{
				services.Remove(descriptor);
			}

			var compositeLifetime = GetMostSpecificLifetime(wrappedDescriptors);
			var compositeServiceDescriptor = GetCompositeServiceDescriptor<TService, TImplementation>(wrappedDescriptors, compositeLifetime);
			services.Add(compositeServiceDescriptor);

			return services;
		}

		private static ServiceLifetime GetMostSpecificLifetime(IEnumerable<ServiceDescriptor> serviceDescriptors)
		{
			return serviceDescriptors.Select(d => d.Lifetime).Max();
		}

		private static ServiceDescriptor GetCompositeServiceDescriptor<TService, TImplementation>(IEnumerable<ServiceDescriptor> wrappedDescriptors, ServiceLifetime compositeLifetime)
			where TService : class
			where TImplementation : class, TService
		{
			var compositeObjectFactory = GetCompositeObjectFactory<TService, TImplementation>();
			var compositeServiceDescriptor = ServiceDescriptor.Describe(
				typeof(TService),
				s => (TService)compositeObjectFactory(s, new[] { wrappedDescriptors.Select(d => s.GetInstance(d)).Cast<TService>() }),
				compositeLifetime);

			return compositeServiceDescriptor;
		}

		private static ObjectFactory GetCompositeObjectFactory<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService
		{
			return ActivatorUtilities.CreateFactory(typeof(TImplementation), new[] { typeof(IEnumerable<TService>) });
		}

	}
}
