using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameNode.Models;
using NameNode.Services;
using NameNode.Status;

namespace NameNode.Controllers
{
    public class CdfsHealthController : Controller
    {
        INameNodeStatus _nameNodeStatus;
        IDataNodesStatus _dataNodesStatus;

        public CdfsHealthController(INameNodeStatus nameNodeStatus, IDataNodesStatus dataNodesStatus)
        {
            _nameNodeStatus = nameNodeStatus;
            _dataNodesStatus = dataNodesStatus;
        }

        public ActionResult Index()
        {
            var model = new NameNodeStatusModel();
            model.Started = _nameNodeStatus.Started;
            model.LiveNodes = _dataNodesStatus.LiveNodes;
            model.DeadNodes = _dataNodesStatus.DeadNodes;

            return View(model);
        }
    }
}