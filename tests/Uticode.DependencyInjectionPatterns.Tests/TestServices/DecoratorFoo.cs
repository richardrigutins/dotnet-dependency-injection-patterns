namespace Uticode.DependencyInjectionPatterns.Tests.TestServices
{
	public class DecoratorFoo : IFoo
	{
		private readonly IFoo _inner;

		public DecoratorFoo(IFoo inner)
		{
			_inner = inner;
		}

		public IFoo Inner => _inner;
	}
}
