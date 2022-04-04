using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Uticode.DependencyInjectionPatterns.Common
{
	internal static class ServiceCollectionExtensions
	{
		internal static bool TryGetDescriptors(this IServiceCollection services, Type serviceType, out ICollection<ServiceDescriptor> descriptors)
		{
			descriptors = services.Where(service => service.ServiceType == serviceType).ToList();

			return descriptors.Any();
		}

		internal static bool IsServiceRegistered<TService>(this IServiceCollection services)
		{
			return services.Any(s => s.ServiceType == typeof(TService));
		}

		internal static IEnumerable<ServiceDescriptor> GetRegisteredServices<TService>(this IServiceCollection services)
		{
			return services.Where(s => s.ServiceType == typeof(TService)).ToList();
		}
	}
}
