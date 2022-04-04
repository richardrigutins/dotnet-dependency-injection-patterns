namespace Microsoft.Extensions.DependencyInjection.Patterns
{
	public static class ForwardingExtensions
	{
		public static IServiceCollection AddSingleton<TService1, TService2, TImplementation>(this IServiceCollection services)
			where TService1 : class
			where TService2 : class
			where TImplementation : class, TService1, TService2
		{
			services.AddSingleton<TImplementation>();
			services.AddSingleton<TService1>(x => x.GetRequiredService<TImplementation>());
			services.AddSingleton<TService2>(x => x.GetRequiredService<TImplementation>());

			return services;
		}

		public static IServiceCollection AddScoped<TService1, TService2, TImplementation>(this IServiceCollection services)
			where TService1 : class
			where TService2 : class
			where TImplementation : class, TService1, TService2
		{
			services.AddScoped<TImplementation>();
			services.AddScoped<TService1>(x => x.GetRequiredService<TImplementation>());
			services.AddScoped<TService2>(x => x.GetRequiredService<TImplementation>());

			return services;
		}

		public static IServiceCollection AddTransient<TService1, TService2, TImplementation>(this IServiceCollection services)
			where TService1 : class
			where TService2 : class
			where TImplementation : class, TService1, TService2
		{
			services.AddTransient<TImplementation>();
			services.AddTransient<TService1>(x => x.GetRequiredService<TImplementation>());
			services.AddTransient<TService2>(x => x.GetRequiredService<TImplementation>());

			return services;
		}
	}
}
