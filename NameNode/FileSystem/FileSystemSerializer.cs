using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public class FileSystemSerializer : IFileSystemSerializer
    {
        public IDirectory Deserialize(IEnumerable<string> fileImageLines)
        {
            var fileImageLinesEnumerator = fileImageLines.GetEnumerator();
            fileImageLinesEnumerator.MoveNext();
            return DeserializeNodes(fileImageLinesEnumerator) as IDirectory;
        }

        internal static INode DeserializeNodes(IEnumerator<string> lineEnumerator)
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

        public string Serialize(IDirectory root)
        {
            return SerializeNode(root);
        }

        internal static string SerializeNode(INode node)
        {
            var result = new StringBuilder();

            result.Append(node is IDirectory ? "1" : "0");
            result.Append(",");

            // Save node details
            result.AppendFormat("\"{0}\"", node.Name);

            if (node is IDirectory)
            {
                result.Append(",");

                var nodeDirectory = node as IDirectory;

                result.Append(nodeDirectory.ChildCount);

                // Enumerate children and save them
                foreach (var child in nodeDirectory)
                {
                    var savedChildren = SerializeNode(child);
                    result.AppendLine();
                    result.Append(savedChildren);
                }
            }

            return result.ToString();
        }
    }
}
