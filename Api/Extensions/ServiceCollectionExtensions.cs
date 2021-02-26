using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Basic.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddGzipCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });
        }

        internal static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo 
                    {
                        Version = "v1",
                        Title = "Maersk.Castle.Windor.Demo",
                        Description = "Maersk.Castle.Windor.Demo"
                    }
                );
                options.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}