using NameNode.Core.FileSystem.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace NameNode.Core.FileSystem
{
    public class FileSystemSerializer : IFileSystemSerializer
    {
        public IDirectory Deserialize(IEnumerable<string> fileImageLines)
        {
            var fileImageLinesEnumerator = fileImageLines.GetEnumerator();
            fileImageLinesEnumerator.MoveNext();
            return DeserializeNodes(fileImageLinesEnumerator) as IDirectory;
        }

        internal INode DeserializeNodes(IEnumerator<string> lineEnumerator)
        {
            INode result = null;

            var line = lineEnumerator.Current;
            var lineparts = line.Split(',');
            if (lineparts[0] == "0")
            {
                // Line represents a node file
                result = new File
                {
                    Name = lineparts[1].Trim('"')
                };
                lineEnumerator.MoveNext();
            }
            else
            {
                var directory = new Directory
                {
                    Name = lineparts[1].Trim('"')
                };

                lineEnumerator.MoveNext();
                var childCount = int.Parse(lineparts[2]);
                for (int childIndex = 0; childIndex < childCount; childIndex++)
                {
                    var child = DeserializeNodes(lineEnumerator);
                   directory.AddChild(child);
                }
                result = directory;
            }

            return result;
        }

        public IEnumerable<string> Serialize(IDirectory root)
        {
            return SerializeNode(root);
        }

        internal IEnumerable<string> SerializeNode(INode node)
        {
            var result = new List<string>();

            var line = new StringBuilder();

            line.Append(node is IDirectory ? "1" : "0");
            line.Append(",");

            // Save node details
            line.AppendFormat("\"{0}\"", node.Name);

            if (node is IDirectory)
            {
                line.Append(",");

                var nodeDirectory = node as IDirectory;

                line.Append(nodeDirectory.ChildCount);
                result.Add(line.ToString());

                // Enumerate children and save them
                foreach (var child in nodeDirectory)
                {
                    var savedChildren = SerializeNode(child);
                    result.AddRange(savedChildren);
                }
            }
            else
            {
                result.Add(line.ToString());
            }

            return result;
        }
    }
}
