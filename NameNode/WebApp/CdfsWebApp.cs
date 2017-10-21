using System;
using Nancy.Hosting.Self;
using StructureMap;

namespace NameNode.WebApp
{
    class CdfsWebApp
    {
        private NancyHost _nancyHost;

        public void Start(IContainer container)
        {
            var hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;
            _nancyHost = new NancyHost(new CustomBootstrapper(container), hostConfigs, new Uri("http://localhost:5151"));
            _nancyHost.Start();
        }

        public void Stop()
        {
            _nancyHost.Stop();
        }
    }
}