using Api.ContainerControl.Impl.Interfaces;

namespace Api.ContainerControl.Impl
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