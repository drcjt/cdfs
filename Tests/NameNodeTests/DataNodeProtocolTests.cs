using Microsoft.Extensions.Logging;
using Moq;
using NameNode.Services;
using NameNode.Services.Interfaces;
using NUnit.Framework;
using Protocols;
using System;

namespace NameNodeTests
{
    [TestFixture]
    class DataNodeProtocolTests
    {
        [Test]
        public void RegisterDataNode_WhenNodeNotPreviouslyRegistered_DataNodeIsRegistered()
        {
            // Arrange
            var stubDataNodeRepository = new Mock<IDataNodeRepository>();
            var stubDataNodeId = new Mock<IDataNodeId>();
            var expectedDataNodeID = Guid.NewGuid();
            stubDataNodeRepository.Setup(x => x.AddDataNode(stubDataNodeId.Object)).Returns(expectedDataNodeID);

            var stubDateTimeProvider = new Mock<IDateTimeProvider>();

            // TODO - see this post re. ILoggerFactory -> https://stackoverflow.com/questions/45221362/how-to-moq-mock-a-loggerfactory-in-c-sharp-aspnet-core
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var dataNodeProtocol = new DataNodeProtocol(stubLoggerFactory.Object, stubDataNodeRepository.Object, stubDateTimeProvider.Object);

            // Act
            var dataNodeID = dataNodeProtocol.RegisterDataNode(stubDataNodeId.Object);

            // Assert
            Assert.AreEqual(expectedDataNodeID, dataNodeID);
        }

        [Test]
        public void SendHeartbeat_Always_SetsLastUpdatesTicksToNow()
        {
            // Arrange
            var stubDateTimeProvider = new Mock<IDateTimeProvider>();
            var now = new DateTime(999);
            stubDateTimeProvider.Setup(x => x.Now).Returns(now);

            var mockDataNodeRepository = new Mock<IDataNodeRepository>();
            var dataNodeID = Guid.NewGuid();

            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);

            var dataNodeProtocol = new DataNodeProtocol(stubLoggerFactory.Object, mockDataNodeRepository.Object, stubDateTimeProvider.Object);

            // Act
            dataNodeProtocol.SendHeartbeat(dataNodeID);

            // Assert
            mockDataNodeRepository.Verify(x => x.SetLastUpdateTicks(dataNodeID, now.Ticks), Times.Once);
        }
    }
}
