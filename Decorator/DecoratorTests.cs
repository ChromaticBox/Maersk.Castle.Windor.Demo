using System.Linq;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
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
        private static Bootstrapper _bootstrapper;

        [OneTimeSetUp]
        public static void TestOneTimeSetUp()
        {
            _bootstrapper = new Bootstrapper();
            var container = _bootstrapper.Container;

            container.Register(Component.For<IFooBar>().ImplementedBy<Foo>());
            container.Register(Component.For<IFooBar>().ImplementedBy<Bar>());
        }

        [Test]
        public static void TestDecorator()
        {
            var foo = _bootstrapper.Container.Resolve<IFooBar>();
            foo.Should().NotBeNull();
            foo.GetType().Should().Be<Foo>();
        }

        [Test]
        public static void TestDecorators()
        {
            var container = _bootstrapper.Container;
            var fooBars= container.ResolveAll<IFooBar>();

            fooBars.Any(x => x.GetType() == typeof(Foo)).Should().BeTrue();
            fooBars.Any(x => x.GetType() == typeof(Bar)).Should().BeTrue();
        }
    }
}