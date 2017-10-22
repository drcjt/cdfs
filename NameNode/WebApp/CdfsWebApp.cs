using System;
using Nancy.Hosting.Self;
using StructureMap;

namespace NameNode.WebApp
{
    class CdfsWebApp
    {
        private NancyHost _nancyHost;

        private readonly IContainer _container;
        private readonly Uri _baseUri;

        public CdfsWebApp(IContainer container, Uri baseUri)
        {
            _container = container;
            _baseUri = baseUri;
        }

        public void Start()
        {
            var hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;
            _nancyHost = new NancyHost(new CustomBootstrapper(_container), hostConfigs, _baseUri);
            _nancyHost.Start();
        }

        public void Stop()
        {
            _nancyHost.Stop();
        }
    }
}