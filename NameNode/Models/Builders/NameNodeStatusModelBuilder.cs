using NameNode.Services.Interfaces;
using NameNode.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Models.Builders
{
    static public class NameNodeStatusModelBuilder
    {
        static public NameNodeStatusModel CreateModel(INameNodeStatus nameNodeStatus, IDataNodesStatus dataNodesStatus)
        {
            var model = new NameNodeStatusModel
            {
                Started = nameNodeStatus.Started,
                LiveNodes = dataNodesStatus.LiveNodes,
                DeadNodes = dataNodesStatus.DeadNodes
            };

            return model;
        }

    }
}
