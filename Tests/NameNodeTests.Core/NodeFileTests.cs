using NameNode.Core.FileSystem;
using NUnit.Framework;

namespace NameNodeTests.Core
{
    [TestFixture]
    class NodeFileTests
    {
        [Test]
        public void IsRoot_NodeWithNoParent_ReturnsTrue()
        {
            // Arrange
            var nodeFile = new File();

            // Act
            var isRoot = nodeFile.IsRoot;

            // Assert
            Assert.IsTrue(isRoot);
        }

        [Test]
        public void IsRoot_NodeWithParent_ReturnsFalse()
        {
            // Arrange
            var nodeFile = new File();
            nodeFile.Parent = new File();

            // Act
            var isRoot = nodeFile.IsRoot;

            // Assert
            Assert.IsFalse(isRoot);
        }
    }
}
