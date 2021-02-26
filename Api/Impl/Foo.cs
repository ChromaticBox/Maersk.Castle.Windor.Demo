using Api.Basic.Impl.Interfaces;

namespace Api.Basic.Impl
{
    public class Foo : IFoo
    {
        private readonly IBar _bar;

        public Foo(IBar bar)
        {
            _bar = bar;
        }

        public void Dispose()
        {
            ;
        }
    }
}