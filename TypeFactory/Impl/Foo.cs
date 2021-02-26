using TypeFactory.Facilities;
using TypeFactory.Impl.Interfaces;

namespace TypeFactory.Impl
{
    public class Foo : IFoo
    {
        private readonly IFooFactory _fooFactory;
        private readonly IBar _bar;
        private bool _isDisposing;

        public Foo(IFooFactory fooFactory)
        {
            _fooFactory = fooFactory;
        }

        public Foo(IBar bar)
        {
            _bar = bar;
        }

        public void Dispose()
        {
            if(_isDisposing) return;
            _isDisposing = true;
            _bar.Dispose();
            _fooFactory.Release(this);
        }
    }
}