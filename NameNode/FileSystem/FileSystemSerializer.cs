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
        public INodeDirectory Deserialize(IEnumerator<string> fileImageLines)
        {
            fileImageLines.MoveNext();
            return DeserializeNodes(fileImageLines) as INodeDirectory;
        }

        internal static INode DeserializeNodes(IEnumerator<string> lineEnumerator)
        {
            INode result = null;

            var line = lineEnumerator.Current;
            var lineparts = line.Split(',');
            if (lineparts[0] == "0")
            {
                // Line represents a node file
                result = new NodeFile
                {
                    Name = lineparts[1].Trim('"')
                };
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
                    var child = DeserializeNodes(lineEnumerator);
                    (result as INodeDirectory).AddChild(child);
                }
            }

            return result;
        }

        public string Serialize(INodeDirectory root)
        {
            return SerializeNode(root);
        }

        internal static string SerializeNode(INode node)
        {
            var result = new StringBuilder();

            result.Append(node is INodeDirectory ? "1" : "0");
            result.Append(",");

            // Save node details
            result.AppendFormat("\"{0}\"", node.Name);

            if (node is INodeDirectory)
            {
                result.Append(",");

                var nodeDirectory = node as INodeDirectory;

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
