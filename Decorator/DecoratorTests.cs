using System;
using System.Linq;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics.Helpers;
using Decorator.Impl;
using Decorator.Impl.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace Decorator
{
    //https://github.com/castleproject/Windsor/blob/master/docs/orphan-decorators.md
    //https://github.com/castleproject/Windsor/blob/5e4416da3a2aedad20949d3dd521a2137325abeb/docs/handler-selectors.md


    [TestFixture]
    public class DecoratorTests
    {
        private static IWindsorContainer _container;

        [SetUp]
        public static void TestSetUp()
        {
            var bootstrapper = new Bootstrapper();
            _container = bootstrapper.Container;

            _container.Register(Component.For<IFooBar>().ImplementedBy<Foo>());
            _container.Register(Component.For<IFooBar>().ImplementedBy<Bar>());
        }

        [TearDown]
        public static void TestTearDown()
        {
            _container.Dispose();
        }

        [Test]
        public static void TestDecorator()
        {
            var foo = _container.Resolve<IFooBar>();
            foo.Should().NotBeNull();
            foo.GetType().Should().Be<Foo>();
        }

        [Test]
        public static void TestResolveAllDecorators()
        {
            var fooBars= _container.ResolveAll<IFooBar>();

            fooBars.Any(x => x.GetType() == typeof(Foo)).Should().BeTrue();
            fooBars.Any(x => x.GetType() == typeof(Bar)).Should().BeTrue();
        }


        //https://github.com/castleproject/Windsor/blob/5e4416da3a2aedad20949d3dd521a2137325abeb/docs/handler-selectors.md
        [Test]
        public static void TestSelector()
        {
            _container.Kernel.AddHandlerSelector(new MyDecoratorSelector());

            var foo = _container.Resolve<IFooBar>("Foo");
            foo.GetType().Should().Be<Foo>();

            var bar = _container.Resolve<IFooBar>("Bar");
            bar.GetType().Should().Be<Bar>();
        }
    }

    public class MyDecoratorSelector : IHandlerSelector
    {
        public bool HasOpinionAbout(string key, Type service)
        {
            return key != null && (key.Equals("Foo") || key.Equals("Bar"));
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            return handlers.First(x => x.GetComponentName().Equals($"{key} / IFooBar"));
        }
    }
}