using Moq;
using NameNode.FileSystem;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNodeTests
{
    [TestFixture]
    class NodeWalkerTests
    {
        [Test]
        public void GetNodeByPath_NullPath_ReturnsRoot()
        {
            // Arrange
            var nodeWalker = new NodeWalker();
            var root = new Node();

            // Act
            var result = nodeWalker.GetNodeByPath(root, null);

            // Assert
            Assert.AreEqual(root, result);
        }


        [Test]
        public void GetNodeByPath_EmptyPath_ReturnsRoot()
        {
            // Arrange
            var nodeWalker = new NodeWalker();
            var root = new Node();

            // Act
            var result = nodeWalker.GetNodeByPath(root, "");

            // Assert
            Assert.AreEqual(root, result);
        }

        [Test]
        public void GetNodeByPath_SingleLevelPath_ReturnsCorrespondingNode()
        {
            // Arrange
            var nodeWalker = new NodeWalker();
            var rootDirectory = new Directory { Name = "Root" };
            var childDirectory = new Directory { Name = "A" };
            rootDirectory.AddChild(childDirectory);

            // Act
            var result = nodeWalker.GetNodeByPath(rootDirectory, "A");

            // Assert
            Assert.AreEqual(childDirectory, result);
        }

        [Test]
        public void GetNodeByPath_MultiLevelPath_ReturnsCorrespondingNode()
        {
            // Arrange
            var nodeWalker = new NodeWalker();
            var rootDirectory = new Directory { Name = "Root" };
            var childDirectory = new Directory { Name = "A" };
            rootDirectory.AddChild(childDirectory);
            var subChildDirectory = new Directory { Name = "B" };
            childDirectory.AddChild(subChildDirectory);

            // Act
            var result = nodeWalker.GetNodeByPath(rootDirectory, "A\\B");

            // Assert
            Assert.AreEqual(subChildDirectory, result);
        }

        [Test]
        public void GetNodeByPath_PartialPathExists_ReturnsNull()
        {
            // Arrange
            var nodeWalker = new NodeWalker();
            var rootDirectory = new Directory { Name = "Root" };
            var childDirectory = new Directory { Name = "A" };
            rootDirectory.AddChild(childDirectory);
            var subChildDirectory = new Directory { Name = "C" };
            childDirectory.AddChild(subChildDirectory);

            // Act
            var result = nodeWalker.GetNodeByPath(rootDirectory, "A\\B");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetNodeByPath_StopAtLastExistingNodeAndPartialPathExists_ReturnsLowestExistingLevel()
        {
            // Arrange
            var nodeWalker = new NodeWalker();
            var rootDirectory = new Directory { Name = "Root" };
            var childDirectory = new Directory { Name = "A" };
            rootDirectory.AddChild(childDirectory);
            var subChildDirectory = new Directory { Name = "C" };
            childDirectory.AddChild(subChildDirectory);

            // Act
            var result = nodeWalker.GetNodeByPath(rootDirectory, "A\\B", true);

            // Assert
            Assert.AreEqual(childDirectory, result);
        }
    }
}
