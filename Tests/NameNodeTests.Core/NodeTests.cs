using NameNode.Core.FileSystem;
using NUnit.Framework;

namespace NameNodeTests.Core
{
    [TestFixture]
    class NodeTests
    {
        [Test]
        public void IsRoot_ByDefault_ReturnsTrue()
        {
            // Arrange
            var nodeDirectory = new Node();

            // Act
            var isRoot = nodeDirectory.IsRoot;

            // Assert
            Assert.IsTrue(isRoot);
        }

        [Test]
        public void IsRoot_NodeWithParent_ReturnsFalse()
        {
            // Arrange
            var nodeDirectory = new Node { Parent = new Node() };

            // Act
            var isRoot = nodeDirectory.IsRoot;

            // Assert
            Assert.IsFalse(isRoot);
        }

        [Test]
        public void FullPath_NodeWithNoParent_ReturnsNodeName()
        {
            // Arrange
            var node = new Node { Name = "NodeName" };

            // Act
            var fullPath = node.FullPath;

            // Assert
            Assert.AreEqual("NodeName", fullPath);
        }

        [Test]
        public void FullPath_NodeWithParent_ReturnsParentPathCombinedWithNodeName()
        {
            // Arrange
            var node = new Node { Name = "NodeName", Parent = new Node { Name = "ParentPath" } };

            // Act
            var fullPath = node.FullPath;

            // Assert
            Assert.AreEqual("ParentPath\\NodeName", fullPath);
        }
    }
}
