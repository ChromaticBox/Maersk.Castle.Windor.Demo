using Api.Basic.Impl;
using Api.Basic.Impl.Interfaces;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Api.Basic.Installers
{
    //todo https://github.com/castleproject/Windsor/blob/master/docs/conditional-component-registration.md
    public class ManualInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var logger = container.Resolve<ILogger<ManualInstaller>>();
            logger.LogInformation("ManualInstaller Install");

            container.Register(Component.For<IFoo>().ImplementedBy<Foo>().LifestyleNetTransient().IsFallback());
            container.Register(Component.For<IBar>().ImplementedBy<Bar>().LifestyleNetTransient().IsFallback());

            //Is Fallback: https://github.com/castleproject/Windsor/blob/5e4416da3a2aedad20949d3dd521a2137325abeb/src/Castle.Windsor.Tests/ResolveAllTestCase.cs
        }
    }
}