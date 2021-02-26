using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.ContainerControl.Installers
{
    //https://github.com/castleproject/Windsor/blob/master/docs/registering-components-by-conventions.md

    public class AutomaticInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var automaticRegistration= bool.Parse(container.Resolve<IConfiguration>()["AutomaticRegistration"]);
            if(!automaticRegistration) return;

            container.Register(Classes.FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .Where(t => !t.IsDefined(typeof(SingletonAttribute), true))
                .Configure(component =>
                {
                    component.IsDefault();
                    component.Named($"{component.Implementation.FullName}-Default");
                })
                .WithServiceAllInterfaces()
                .LifestyleNetTransient());

            container.Register(Classes.FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .Where(t => !t.IsDefined(typeof(SingletonAttribute), true))
                .Configure(component =>
                {
                    component.IsDefault();
                    component.Named($"{component.Implementation.FullName}-Default");
                })
                .WithServiceAllInterfaces()
                .LifestyleNetTransient());

        }
    }
}