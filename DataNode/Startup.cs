using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using DataNode.IoC;
using DataNode.Services;
using DataNode.Options;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DataNode
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IContainer _container;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            _container = new Container();

            var nameNodeUri = Configuration.GetValue<string>("ConnectionStrings:NameNodeConnection", "http://localhost:5150");
            var blocksPath = Configuration.GetValue<string>("BlocksPath", "Blocks");
            var dataNodeOptions = new DataNodeOptions { NameNodeUri = nameNodeUri, BlocksPath = blocksPath };

            _container.Configure(config =>
            {
                config.AddRegistry(new DataNodeRegistry());
                config.Populate(services);
                config.For<IDataNodeOptions>().Use(dataNodeOptions);
            });

            return _container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var hostingUrl = "http://localhost:59702";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                hostingUrl = app.ServerFeatures.Get<IServerAddressesFeature>()?.Addresses.First();
            }

            // Start the data node application itself
            var dataNodeService = _container.GetInstance<IDataNodeApplication>();
            dataNodeService.Run(hostingUrl);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
