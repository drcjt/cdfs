using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NameNode.FileSystem.Interfaces;
using NameNode.Models;

namespace NameNode.Controllers
{
    public class ExplorerController : Controller
    {
        private readonly IFileSystem _fileSystem;
        public ExplorerController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IActionResult Index()
        {
            var model = new FileStatusModel();
            model.Files = new List<FileStatus>();
            var files = _fileSystem.GetListing(Request.Query["path"]);
            foreach (var file in files)
            {
                model.Files.Add(new FileStatus() { Name = file.Name, IsFile = file is IFile, FullPath = file.FullPath });
            }

            return View(model);
        }
    }
}