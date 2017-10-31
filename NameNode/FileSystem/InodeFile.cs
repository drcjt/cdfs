﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocols;

namespace NameNode.FileSystem
{
    public class InodeFile : Inode
    {
        public override bool IsDirectory { get => false; }
        public override bool IsFile { get => true; }
    }
}