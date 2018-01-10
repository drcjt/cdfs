using System;
using Microsoft.AspNetCore.Mvc;
using Protocols;

namespace NameNode.Controllers
{
    [Produces("application/json")]
    public class DataNodeProtocolController : Controller
    {
        private readonly IDataNodeProtocol _dataNodeProtocol;

        public DataNodeProtocolController(IDataNodeProtocol dataNodeProtocol)
        {
            _dataNodeProtocol = dataNodeProtocol;
        }

        // GET: api/Employee
        [HttpPost]
        public Guid Register([FromBody]DataNodeId dataNodeId)
        {
            return _dataNodeProtocol.RegisterDataNode(dataNodeId);
        }

        // POST: api/Employee
        [HttpPost]
        public void SendHeartbeat([FromBody]Guid dataNodeIdGuid)
        {
            _dataNodeProtocol.SendHeartbeat(dataNodeIdGuid);
        }
    }
}