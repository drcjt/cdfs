using NameNode.Interfaces;
using NameNode.WebApp.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.WebApp
{
    public class MainModule : NancyModule
    {
        public MainModule(INameNodeServiceManagement nameNodeServiceManagement, IDataNodeProtocolManagement dataNodeServiceManagement)
        {
            var model = new NameNodeStatus();
            model.Started = nameNodeServiceManagement.Started;
            model.LiveNodes = dataNodeServiceManagement.LiveNodes;
            model.DeadNodes = dataNodeServiceManagement.DeadNodes;

            Get["/"] = p => View["cdfshealth.html", model];
        }
    }
}
