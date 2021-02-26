using System;
using Castle.MicroKernel.Handlers;
using TypeFactory.Impl.Interfaces;

namespace TypeFactory.Facilities
{
    public interface IFooFactory 
    {
        T Create<T>();
        IFoo Create();

        void Release(IDisposable disposable);
    }
}