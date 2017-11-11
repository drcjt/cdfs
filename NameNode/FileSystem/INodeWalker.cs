﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface INodeWalker
    {
        INode GetNodeByPath(INode root, string path, bool stopAtLastExistingNode = false);
    }
}
