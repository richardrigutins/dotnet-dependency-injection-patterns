using System;
using System.Collections.Generic;
using Uticode.DependencyInjectionPatterns.Common;

namespace Microsoft.Extensions.DependencyInjection.Patterns
{
	public static class DecoratorExtensions
	{
		public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services)
			where TService : class
			where TDecorator : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			if (services.TryGetDescriptors(typeof(TService), out var descriptors))
			{
				services.DecorateDescriptors(descriptors, typeof(TDecorator));
			}

			return services;
		}

		private static IServiceCollection DecorateDescriptors(this IServiceCollection services, ICollection<ServiceDescriptor> descriptors, Type decoratorType)
		{
			foreach (var descriptor in descriptors)
			{
				var index = services.IndexOf(descriptor);
				services[index] = descriptor.Decorate(decoratorType);
			}

			return services;
		}

		private static ServiceDescriptor Decorate(this ServiceDescriptor descriptor, Type decoratorType)
		{
			return descriptor.WithFactory(provider => provider.CreateInstance(decoratorType, provider.GetInstance(descriptor)));
		}

		private static ServiceDescriptor WithFactory(this ServiceDescriptor descriptor, Func<IServiceProvider, object> factory)
		{
			return ServiceDescriptor.Describe(descriptor.ServiceType, factory, descriptor.Lifetime);
		}
	}
}
