using Microsoft.AspNetCore.Mvc;
using Protocols;
using System.Collections.Generic;

namespace NameNode.Controllers
{
    [Produces("application/json")]
    public class ClientProtocolController : Controller
    {
        private readonly IClientProtocol _clientProtocol;

        public ClientProtocolController(IClientProtocol clientProtocol)
        {
            _clientProtocol = clientProtocol;
        }

        // POST: api/Create
        [HttpPost]
        public void Create(string srcFile, string filePath)
        {
            _clientProtocol.Create(srcFile, filePath);
        }

        // POST: api/AddBlock
        [HttpPost]
        public LocatedBlock AddBlock(string srcFile)
        {
            return null;
        }

        // DELETE: api/Delete
        [HttpDelete]
        public void Delete(string filePath)
        {
            _clientProtocol.Delete(filePath);
        }

        [HttpPost]
        public void Mkdir(string directoryPath)
        {
            _clientProtocol.Mkdir(directoryPath);
        }

        [HttpGet]
        public ICollection<CdfsFileStatus> GetListing(string filePath)
        {
            return _clientProtocol.GetListing(filePath);
        }       
    }
}