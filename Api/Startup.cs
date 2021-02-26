using System.Reflection;
using Api.Basic.Extensions;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Basic
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Step 1
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwagger();
            services.AddGzipCompression();
            services.AddControllers()

            //Using .NET lifestyle semantics, any service using the Castle Windsor transient lifestyle, and directly injected into the controller,
            //will not be released. To allow the standard Windsor behavior for services directly injected in the controllers, you will have to allow castle Windsor
            //to resolve and release the controllers. To do that add AddControllerAsServices to the call to ConfigureServices in your Startup class:
                .AddControllersAsServices(); 
        }

#region InstallInConfigureContainer Step2-Recommened

        //Step 2
        public void ConfigureContainer (IWindsorContainer container)
        {
            var logger = container.Resolve<ILogger<Startup>>();
            logger.LogInformation("Startup ConfigureContainer");

            container.Install(FromAssembly.InThisApplication(Assembly.GetEntryAssembly()));
            //container.Install(new AutomaticInstaller(), new ManualInstaller());
        }
#endregion

        //Step 3
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
