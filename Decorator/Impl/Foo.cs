using Decorator.Impl.Interfaces;

namespace Decorator.Impl
{
    public class Foo : IFooBar
    {
        private readonly IFooBar _bar;

        public Foo(IFooBar bar)
        {
            _bar = bar;
        }

        public void Dispose()
        {
            ;
        }
    }
}