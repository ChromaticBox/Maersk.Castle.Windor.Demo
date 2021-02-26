using System;
using TypeFactory.Impl.Interfaces;

namespace TypeFactory.Facilities
{
    public interface IBarFactory 
    {
        IBar Create();
        void Release(IDisposable disposable);
    }
}