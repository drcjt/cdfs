using Nancy;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;
using Nancy.TinyIoc;
using System.Reflection;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;
using NameNode.DependencyInjection;

namespace NameNode.WebApp
{
    public class CustomBootstrapper : StructureMapNancyBootstrapper
    {
        protected override NancyInternalConfiguration InternalConfiguration => NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);

        private readonly IContainer _container;

        public CustomBootstrapper(IContainer container)
        {
            _container = container;
        }

        void OnConfigurationBuilder(NancyInternalConfiguration config)
        {
            config.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }

        protected override IContainer GetApplicationContainer()
        {
            return _container;
        }
        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            ResourceViewLocationProvider.RootNamespaces.Add(Assembly.GetAssembly(typeof(MainModule)), "NameNode.WebApp.Views");
        }
    }
}
