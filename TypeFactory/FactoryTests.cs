using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using NUnit.Framework;
using TypeFactory.Facilities;
using TypeFactory.Impl;
using TypeFactory.Impl.Interfaces;

namespace TypeFactory
{
    [TestFixture]
    public class FactoryTests
    {
        private static Bootstrapper _bootstrapper;

        [OneTimeSetUp]
        public static void TestOneTimeSetUp()
        {
            _bootstrapper = new Bootstrapper();
            var container = _bootstrapper.Container;
            container.Register(Component.For<IFoo>().ImplementedBy<Foo>().LifestyleTransient().IsFallback());
            container.Register(Component.For<IBar>().ImplementedBy<Bar>().LifestyleTransient().IsFallback());

            _bootstrapper.Container.Register(Component.For<IFooFactory>().AsFactory());

            container.Register(Component.For<BarFactorySelector, ITypedFactoryComponentSelector>());
            _bootstrapper.Container.Register(Component.For<IBarFactory>().AsFactory(c=>c.SelectedWith<BarFactorySelector>()));
        }

        [Test]
        public static void TestFooFacility()
        {

            var facility = _bootstrapper.Container.Resolve<IFooFactory>();
            using var actual = facility.Create<IFoo>();
            actual.Should().NotBeNull();
        }


        [Test]
        public static void TestBarFacility()
        {
            var facility = _bootstrapper.Container.Resolve<IBarFactory>();
            using var actual = facility.Create();
            actual.Should().NotBeNull();
        }


    }

    //https://github.com/castleproject/Windsor/blob/5e4416da3a2aedad20949d3dd521a2137325abeb/docs/typed-factory-facility-interface-based.md
    public class BarFactorySelector : DefaultTypedFactoryComponentSelector
    {
        private readonly IKernelInternal _kernel;

        public BarFactorySelector(IKernelInternal kernel)
        {
            _kernel = kernel;
        }

        protected override Arguments GetArguments(MethodInfo method, object[] arguments)
        {
            return base.GetArguments(method, arguments);
        }

        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            return base.GetComponentName(method, arguments);
        }
    }
}