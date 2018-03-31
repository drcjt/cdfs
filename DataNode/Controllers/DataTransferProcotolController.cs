using Microsoft.AspNetCore.Mvc;
using Protocols;
using System;

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
        public void WriteBlock(string id)
        {
            var block = new Block(new Guid(id), 0, DateTime.Now);
            _dataTransferProtocol.WriteBlock(block, Request.Body);
        }
   }
}