using Microsoft.AspNetCore.Mvc;
using Protocols;
using System.Collections.Generic;

namespace DataNode.Controllers
{
    [Produces("application/json")]
    public class DataTransferProtocolController : Controller
    {
        private readonly IDataTransferProtocol _dataTransferProtocol;

        public DataTransferProtocolController(IDataTransferProtocol dataTransferProtocol)
        {
            _dataTransferProtocol = dataTransferProtocol;
        }

        // PATCH: api/WriteBlock/<Block Id>
        [HttpPatch]
        public void WriteBlock(string blockID)
        {
            var block = new Block(blockID, 0, null);
            _dataTransferProtocol.WriteBlock(block, Request.Body);
        }
   }
}