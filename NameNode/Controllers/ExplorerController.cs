using System.Collections.Generic;
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
            return View(CreateModel(Request.Query["path"]));
        }

        public FileStatusModel CreateModel(string directoryPath)
        {
            var model = new FileStatusModel
            {
                Files = new List<FileStatus>()
            };
            var files = _fileSystem.GetListing(directoryPath);
            foreach (var file in files)
            {
                model.Files.Add(new FileStatus() { Name = file.Name, IsFile = file is IFile, FullPath = file.FullPath });
            }

            return model;
        }
    }
}