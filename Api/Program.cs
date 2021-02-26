using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Api.Basic
{
    //https://github.com/castleproject/Windsor/tree/master/docs
    //https://github.com/castleproject/Windsor/blob/master/docs/net-dependency-extension.md

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog()
                .UseWindsorContainerServiceProvider()
            ;
    }
}
