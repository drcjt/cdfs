using NameNode.FileSystem;
using NUnit.Framework;
using Protocols;
using System;

namespace NameNodeTests
{
    [TestFixture]
    class FileTests
    {
        [Test]
        public void GetBlocks_WithNoBlocksAdded_ReturnsEmptyList()
        {
            // Arrange
            var file = new File();

            // Act
            var blocks = file.GetBlocks();

            // Assert
            Assert.AreEqual(0, blocks.Count);
        }

        [Test]
        public void GetBlocks_WithBlocksAdded_ReturnsBlocks()
        {
            // Arrange
            var file = new File();
            var block1 = new Block(Guid.NewGuid(), 10, DateTime.Now);
            var block2 = new Block(Guid.NewGuid(), 20, DateTime.Now);
            var block3 = new Block(Guid.NewGuid(), 30, DateTime.Now);
            file.AddBlock(block1);
            file.AddBlock(block2);
            file.AddBlock(block3);

            // Act
            var blocks = file.GetBlocks();

            // Assert
            CollectionAssert.AreEqual(new[] { block1, block2, block3 }, blocks);
        }
    }
}
