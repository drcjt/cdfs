using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public class FileSystem : IFileSystem
    {
        /*
         * The HDFS namespace is stored by the NameNode. The NameNode uses a transaction log called the EditLog to 
         * persistently record every change that occurs to file system metadata. For example, creating a new file 
         * in HDFS causes the NameNode to insert a record into the EditLog indicating this. Similarly, changing the 
         * replication factor of a file causes a new record to be inserted into the EditLog. The NameNode uses a file 
         * in its local host OS file system to store the EditLog. The entire file system namespace, including the 
         * mapping of blocks to files and file system properties, is stored in a file called the FsImage. The FsImage 
         * is stored as a file in the NameNode’s local file system too.
         * 
         * The NameNode keeps an image of the entire file system namespace and file Blockmap in memory. This key 
         * metadata item is designed to be compact, such that a NameNode with 4 GB of RAM is plenty to support a 
         * huge number of files and directories. When the NameNode starts up, it reads the FsImage and EditLog 
         * from disk, applies all the transactions from the EditLog to the in-memory representation of the FsImage, 
         * and flushes out this new version into a new FsImage on disk. It can then truncate the old EditLog because 
         * its transactions have been applied to the persistent FsImage. This process is called a checkpoint. In the 
         * current implementation, a checkpoint only occurs when the NameNode starts up. Work is in progress to 
         * support periodic checkpointing in the near future.
         */

        private INodeDirectory _root = null;

        public INodeDirectory Root {
            get
            {
                if (_root == null)
                {
                    _root = LoadFileImage("FSImage");
                }

                return _root;
            }
        }

        public static INodeDirectory LoadFileImage(string fileImage)
        {
            if (!File.Exists(fileImage))
            {
                return new NodeDirectory();
            }

            var lines = File.ReadLines(fileImage);
            var lineEnumerator = lines.GetEnumerator();
            lineEnumerator.MoveNext();

            return LoadNodes(lineEnumerator) as INodeDirectory;
        }

        internal static INode LoadNodes(IEnumerator<string> lineEnumerator)
        {
            INode result = null;

            var line = lineEnumerator.Current;
            var lineparts = line.Split(',');
            if (lineparts[0] == "0")
            {
                // Line represents a node file
                result = new NodeFile();
                result.Name = lineparts[1].Trim('"');
                lineEnumerator.MoveNext();
            }
            else
            {
                result = new NodeDirectory();
                result.Name = lineparts[1].Trim('"');

                lineEnumerator.MoveNext();
                var childCount = int.Parse(lineparts[2]);
                for (int childIndex = 0; childIndex < childCount; childIndex++)
                {
                    var child = LoadNodes(lineEnumerator);
                    (result as INodeDirectory).AddChild(child);
                }
            }

            return result;
        }

        public static string SaveFileImage(INodeDirectory root)
        {
            return SaveNode(root);
        }

        internal static string SaveNode(INode node)
        {
            var result = "";

            result += node is INodeDirectory ? "1" : "0";
            result += ",";

            // Save node details
            result += "\"" + node.Name + "\"";

            if (node is INodeDirectory)
            {
                result += ",";

                var nodeDirectory = node as INodeDirectory;

                result += nodeDirectory.ChildCount;

                // Enumerate children and save them
                foreach (var child in nodeDirectory)
                {
                    var savedChildren = SaveNode(child);
                    result += "\r\n" + savedChildren;
                }
            }

            return result;
        }
    }
}
