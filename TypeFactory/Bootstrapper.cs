using Castle.Facilities.TypedFactory;
using Castle.Windsor;

namespace TypeFactory
{
    public class Bootstrapper
    {
        //https://github.com/castleproject/Windsor/blob/master/docs/facilities.md
        public IWindsorContainer Container { get; } = new WindsorContainer();

        public Bootstrapper()
        {
            Container.AddFacility<TypedFactoryFacility>();
        }
    }
}