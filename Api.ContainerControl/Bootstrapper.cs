using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection;

namespace Api.ContainerControl
{
    public class Bootstrapper : WindsorServiceProviderFactoryBase
    {
        internal static IWindsorContainer RootContainer { get; } = new WindsorContainer();

        //for reference https://github.com/castleproject/Windsor/blob/14089ae3b2a5b092ff3f20f58ea53d31b74dfcc0/src/Castle.Windsor.Extensions.DependencyInjection/WindsorServiceProviderFactory.cs
        public Bootstrapper()
        {
            base.CreateRootScope();
            base.SetRootContainer(RootContainer);
        }

    }
}