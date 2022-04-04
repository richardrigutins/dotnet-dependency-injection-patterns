using System;

namespace Microsoft.Extensions.DependencyInjection.Patterns
{
	public static class GenericExtensions
	{
		public static IServiceCollection Add<TService>(this IServiceCollection services, ServiceLifetime lifetime)
			where TService : class
		{
			services = services ?? throw new ArgumentNullException(nameof(services));

			services.Add(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));

			return services;
		}

		public static IServiceCollection Add<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
			where TService : class
			where TImplementation : class, TService
		{
			services = services ?? throw new ArgumentNullException(nameof(services));

			services.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));

			return services;
		}
	}
}
