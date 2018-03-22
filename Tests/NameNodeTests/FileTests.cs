using NameNode.BlockManagement;
using NameNode.FileSystem;
using NUnit.Framework;
using Protocols;
using System;
using System.Collections.Generic;

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
            var block1 = new BlockInfo(new Block(Guid.NewGuid(), 10, DateTime.Now), new List<DataNodeId> { new DataNodeId() });
            var block2 = new BlockInfo(new Block(Guid.NewGuid(), 20, DateTime.Now), new List<DataNodeId> { new DataNodeId() });
            var block3 = new BlockInfo(new Block(Guid.NewGuid(), 30, DateTime.Now), new List<DataNodeId> { new DataNodeId() });
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
