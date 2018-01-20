using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NameNode.FileSystem.Interfaces;
using NameNode.Models;
using NameNode.Models.Builders;

namespace NameNode.Controllers
{
    public class ExplorerController : Controller
    {
        private readonly IFileSystem _fileSystem;
        public ExplorerController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IActionResult Index(string path)
        {
            var files = _fileSystem.GetListing(path);
            return View(FileStatusModelBuilder.CreateModel(path, files));
        }
    }
}