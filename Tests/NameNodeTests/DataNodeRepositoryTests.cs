﻿using Moq;
using NameNode.Services;
using NameNode.Services.Interfaces;
using NUnit.Framework;
using Protocols;
using System;

namespace NameNodeTests
{
    [TestFixture]
    class DataNodeRepositoryTests
    {
        [Test]
        public void AddDataNode_Always_ReturnsNewDataNodeID()
        {
            // Arrange
            var dataNodeID = new DataNodeId()
            {
                IPAddress = "IPAddress",
                HostName = "HostName",
            };
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);

            // Act
            var result = sut.AddDataNode(dataNodeID);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetDataNodeDescriptorById_InvalidId_ReturnsNull()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);

            // Act
            var result = sut.GetDataNodeDescriptorById(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetDataNodeDescriptorById_ValidId_ReturnsCopyOfDataNodeId()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);
            var dataNodeId = new DataNodeId()
            {
                IPAddress = "IPAddress",
                HostName = "HostName",
            };
            var dataNodeGuid = sut.AddDataNode(dataNodeId);

            // Act
            var result = sut.GetDataNodeDescriptorById(dataNodeGuid);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result, dataNodeId); // Validate result is a copy of the original DataNodeId object
            Assert.AreEqual(dataNodeId.IPAddress, result.IPAddress);
            Assert.AreEqual(dataNodeId.HostName, result.HostName);
        }

        [Test]
        public void LiveNodes_NoNodesAdded_ReturnsZero()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);

            // Act
            var result = sut.LiveNodes;

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void DeadNodes_NoNodesAdded_ReturnsZero()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);

            // Act
            var result = sut.DeadNodes;

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetRandomDataNodeId_Always_ReturnsRandomNodeId()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);
            var dataNodeId = new DataNodeId()
            {
                IPAddress = "IPAddress",
                HostName = "HostName",
            };
            var dataNodeGuid = sut.AddDataNode(dataNodeId);

            // Act
            var result = sut.GetRandomDataNodeId();

            // Assert
            Assert.AreEqual(dataNodeGuid, result);
        }

        [Test]
        public void LiveNodes_LastUpdateLessThanExpiryInterval_ReturnsOne()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            stubTimeProvider.Setup(x => x.Now).Returns(DateTime.Now);
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);
            var dataNodeId = new DataNodeId()
            {
                IPAddress = "IPAddress",
                HostName = "HostName",
            };
            sut.AddDataNode(dataNodeId);

            // Act
            var result = sut.LiveNodes;

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void LiveNodes_LastUpdateMoreThanExpiryInterval_ReturnsZero()
        {
            // Arrange
            var stubRandomGenerator = new Mock<IRandomGenerator>();
            stubRandomGenerator.Setup(x => x.Generate(It.IsAny<int>())).Returns(0);
            var stubTimeProvider = new Mock<ITimeProvider>();
            var sut = new DataNodeRepository(stubRandomGenerator.Object, stubTimeProvider.Object);
            var now = DateTime.Now;
            stubTimeProvider.Setup(x => x.Now).Returns(now);
            var dataNodeId = new DataNodeId()
            {
                IPAddress = "IPAddress",
                HostName = "HostName",
            };
            sut.AddDataNode(dataNodeId);

            stubTimeProvider.Setup(x => x.Now).Returns(now.AddMilliseconds(sut.HeartBeatExpireIntervalMilliseconds + 1));

            // Act
            var result = sut.LiveNodes;

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
