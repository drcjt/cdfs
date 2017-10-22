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
        public MainModule(INameNodeStatus nameNodeServiceManagement, IDataNodesStatus dataNodesStatus)
        {
            var model = new NameNodeStatus();
            model.Started = nameNodeServiceManagement.Started;
            model.LiveNodes = dataNodesStatus.LiveNodes;
            model.DeadNodes = dataNodesStatus.DeadNodes;

            Get["/"] = p => View["cdfshealth.html", model];
        }
    }
}
