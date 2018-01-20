using Microsoft.AspNetCore.Mvc;
using NameNode.Models;
using NameNode.Models.Builders;
using NameNode.Services.Interfaces;
using NameNode.Status;

namespace NameNode.Controllers
{
    public class CdfsHealthController : Controller
    {
        private readonly INameNodeStatus _nameNodeStatus;
        private readonly IDataNodesStatus _dataNodesStatus;

        public CdfsHealthController(INameNodeStatus nameNodeStatus, IDataNodesStatus dataNodesStatus)
        {
            _nameNodeStatus = nameNodeStatus;
            _dataNodesStatus = dataNodesStatus;
        }

        public ActionResult Index()
        {
            return View(NameNodeStatusModelBuilder.CreateModel(_nameNodeStatus, _dataNodesStatus));
        }
    }
}