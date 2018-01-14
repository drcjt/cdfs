using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using DataNode.IoC;
using DataNode.Services;
using DataNode.Options;

namespace DataNode
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var container = new Container();

            var nameNodeUri = Configuration.GetValue<string>("ConnectionStrings:NameNodeConnection", "http://localhost:5150");
            var dataNodeOptions = new DataNodeOptions { NameNodeUri = nameNodeUri };

            container.Configure(config =>
            {
                config.AddRegistry(new DataNodeRegistry());
                config.Populate(services);
                config.For<IDataNodeOptions>().Use(dataNodeOptions);
            });

            // Start the data node application itself
            var dataNodeService = container.GetInstance<IDataNodeApplication>();
            dataNodeService.Run();

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
