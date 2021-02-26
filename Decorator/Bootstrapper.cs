using Castle.Facilities.TypedFactory;
using Castle.Windsor;

namespace Decorator
{
    public class Bootstrapper
    {
        public IWindsorContainer Container { get; } = new WindsorContainer();
        
    }
}