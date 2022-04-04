using System.Collections.Generic;

namespace Uticode.DependencyInjectionPatterns.Tests.TestServices
{
	public class ComposedFoo : IFoo
	{
		private readonly IEnumerable<IFoo> _composedServices;

		public ComposedFoo(IEnumerable<IFoo> composedServices)
		{
			_composedServices = composedServices;
		}

		public IEnumerable<IFoo> ComposedServices => _composedServices;
	}
}
