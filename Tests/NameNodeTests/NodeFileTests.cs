using NameNode.FileSystem;
using NUnit.Framework;

namespace NameNodeTests
{
    [TestFixture]
    class NodeFileTests
    {
        private File _nodeFile;

        [SetUp]
        public void Setup()
        {
            _nodeFile = new File();
        }

        [Test]
        public void IsRoot_WhenParentIsNull_ReturnsTrue()
        {
            Assert.IsTrue(_nodeFile.IsRoot);
        }

        [Test]
        public void IsRoot_WhenParentIsANodeFile_ReturnsFalse()
        {
            _nodeFile.Parent = new File();

            Assert.IsFalse(_nodeFile.IsRoot);
        }

    }
}
