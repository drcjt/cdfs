using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
using System.Collections.Generic;

namespace NameNode.Models.Builders
{
    static public class FileStatusModelBuilder
    {
        static public FileStatusModel CreateModel(string directoryPath, IList<INode> files)
        {
            var model = new FileStatusModel
            {
                Directory = FileSystemPath.GetFullPath(directoryPath),
                Files = new List<FileStatus>()
            };

            foreach (var file in files)
            {
                model.Files.Add(new FileStatus() { Name = file.Name, IsFile = file is IFile, FullPath = file.FullPath });
            }

            return model;
        }
    }
}
