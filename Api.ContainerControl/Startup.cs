using System.Linq;
using System.Reflection;
using Api.ContainerControl.Exceptions;
using Api.ContainerControl.Extensions;
using Api.ContainerControl.Impl;
using Api.ContainerControl.Impl.Interfaces;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;
using Castle.Windsor.Installer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.ContainerControl
{

    public class Startup
    {
        private IBar fridayBar;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Bootstrapper.RootContainer.Register(Component.For<IBar>().ImplementedBy<Bar>().Named("Bar-Startup-Registration").LifestyleNetTransient().IsFallback());
        }

        public IConfiguration Configuration { get; }

        //Step 1
        public void ConfigureServices(IServiceCollection services)
        {
            fridayBar = Bootstrapper.RootContainer.Resolve<IBar>();
            if (fridayBar == null) throw new INeedABeerException();

            //e.g. var cookieEvents = new CookieAuthenticationEventsWrapper(settings, **container.Resolve<IDictionaryCache<string, CookieAuthenticationEventUser>>(), container.Resolve<ICookieUserCache>(), container.Resolve<ICqrsDispatcher>());**

            services.AddControllers().AddControllersAsServices(); 
        }

        public void ConfigureContainer (IWindsorContainer container)
        {
            var logger = container.Resolve<ILogger<Startup>>();
            logger.LogInformation("Startup ConfigureContainer");

            container.Install(FromAssembly.InThisApplication(Assembly.GetEntryAssembly()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = app.ApplicationServices.GetService<ILogger<Startup>>();
            logger.LogInformation("Startup Configure");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
