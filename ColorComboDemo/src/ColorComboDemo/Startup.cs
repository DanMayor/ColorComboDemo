using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace ColorComboDemo
{
    public class Startup {
        /// <summary>
        ///     ConfigureServices is called by the host runtime and signals our application to register
        ///     the .NET Core services it requires
        /// </summary>
        /// <param name="services">The Services collection to register with</param>
        public void ConfigureServices(IServiceCollection services) {
            var builder = services.AddMvc();
            builder.AddMvcOptions(options => {
                options.CacheProfiles.Add("NoCache", new CacheProfile {
                    NoStore = true,
                    Duration = 0
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        ///     Configure is called by the host runtime and signals our application to execute the .NET Core
        ///     services registered above.
        /// </summary>
        /// <param name="app">The application builder object</param>
        /// <param name="env">The environment builder object</param>
        /// <param name="loggerFactory">The logger builder object</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            // Serves as the default action / request handler
            app.Use(async (context, next) => {
                await next();

                if (context.Response.StatusCode == 404 && 
                !Path.HasExtension(context.Request.Path.Value) && 
                !context.Request.Path.Value.StartsWith("/node_modules/") &&
                !context.Request.Path.Value.StartsWith("/api/")) {

                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            string libPath = Path.GetFullPath(Path.Combine(env.WebRootPath, @"..\node\modules\"));
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(libPath),
                RequestPath = new PathString("/node_modules")
            });

            app.Run(async (context) => {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
