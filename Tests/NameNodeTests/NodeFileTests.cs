using NameNode.FileSystem;
using NUnit.Framework;

namespace NameNodeTests
{
    [TestFixture]
    class NodeFileTests
    {
        private NodeFile _nodeFile;

        [SetUp]
        public void Setup()
        {
            _nodeFile = new NodeFile();
        }

        [Test]
        public void IsDirectory_Always_ReturnsFalse()
        {
            Assert.IsFalse(_nodeFile.IsDirectory);
        }

        [Test]
        public void IsFile_Always_ReturnsTrue()
        {
            Assert.IsTrue(_nodeFile.IsFile);
        }

        [Test]
        public void IsRoot_WhenParentIsNull_ReturnsTrue()
        {
            Assert.IsTrue(_nodeFile.IsRoot);
        }

        [Test]
        public void IsRoot_WhenParentIsANodeFile_ReturnsFalse()
        {
            _nodeFile.Parent = new NodeFile();

            Assert.IsFalse(_nodeFile.IsRoot);
        }

    }
}
