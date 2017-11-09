using NameNode.FileSystem;
using NameNode.WebApp.Models;
using Nancy;
using System.Collections.Generic;

namespace NameNode.WebApp
{
    public class ExplorerModule : NancyModule
    {
        public ExplorerModule(IFileSystem fileSystem)
        {
            Get["/browse"] = parameters =>
            {
                var model = new List<FileStatus>();
                var files = fileSystem.GetListing(Request.Query.path);
                foreach (var file in files)
                {
                    model.Add(new FileStatus() { Name = file.Name, IsFile = file is IFile, FullPath = file.FullPath });
                }

                return View["explorer.html", model];
            };
        }
    }
}
