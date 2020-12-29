#region

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spire.Examples.Shared;
using Spire.Hosting.Web;

#endregion

namespace Spire.Examples.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpire(configurator =>
            {
                configurator.WithConfigurationOptions();

                configurator.ConfigureBotHost(webHost
                    =>
                {
                    webHost.WithBot(options => Defaults.BuildDefaultBot(options.ApiToken));
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSpire();
        }
    }
}