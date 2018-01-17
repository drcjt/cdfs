using Microsoft.AspNetCore.Mvc;
using NameNode.Models;
using NameNode.Services;
using NameNode.Status;

namespace NameNode.Controllers
{
    public class CdfsHealthController : Controller
    {
        private readonly INameNodeStatus _nameNodeStatus;
        private readonly IDataNodesStatus _dataNodesStatus;

        public CdfsHealthController()
        {
        }

        public CdfsHealthController(INameNodeStatus nameNodeStatus, IDataNodesStatus dataNodesStatus)
        {
            _nameNodeStatus = nameNodeStatus;
            _dataNodesStatus = dataNodesStatus;
        }

        public ActionResult Index()
        {
            return View(CreateModel());
        }

        public NameNodeStatusModel CreateModel()
        {
            var model = new NameNodeStatusModel
            {
                Started = _nameNodeStatus.Started,
                LiveNodes = _dataNodesStatus.LiveNodes,
                DeadNodes = _dataNodesStatus.DeadNodes
            };

            return model;
        }
    }
}