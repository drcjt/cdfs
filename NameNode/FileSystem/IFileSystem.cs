﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface IFileSystem
    {
        INodeDirectory Root { get; }
        void Create(string srcFile, string filePath);
        IList<INode> GetListing(string filePath);
    }
}
