using Microsoft.Extensions.DependencyInjection;
using System;

namespace Uticode.DependencyInjectionPatterns.Common
{
	internal static class ServiceProviderExtensions
	{
		internal static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
		{
			object instance = null;

			if (descriptor.ImplementationInstance != null)
			{
				instance = descriptor.ImplementationInstance;
			}
			else if (descriptor.ImplementationFactory != null)
			{
				instance = descriptor.ImplementationFactory(provider);
			}
			else
			{
				instance = provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
			}

			return instance;
		}

		internal static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
		}

		internal static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
		{
			return ActivatorUtilities.CreateInstance(provider, type, arguments);
		}
	}
}
