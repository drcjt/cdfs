using Moq;
using NameNode.Interfaces;
using NameNode.Service;
using NUnit.Framework;
using log4net;
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
            var dataNodeRegistration = new DataNodeRegistration();
            var expectedDataNodeID = Guid.NewGuid();
            stubDataNodeRepository.Setup(x => x.AddDataNode(It.IsAny<DataNodeDescriptor>())).Returns(expectedDataNodeID);

            var stubDateTimeProvider = new Mock<IDateTimeProvider>();
            var dataNodeProtocol = new DataNodeProtocol(new Mock<ILog>().Object, stubDataNodeRepository.Object, stubDateTimeProvider.Object);

            // Act
            var dataNodeID = dataNodeProtocol.RegisterDataNode(dataNodeRegistration);

            // Assert
            Assert.AreEqual(expectedDataNodeID, dataNodeID);
        }

        [Test]
        public void SendHeartbeat_WithRegisteredDataNodeID_UpdatesLastUpdatedTime()
        {
            // Arrange
            var mockDataNodeRepository = new Mock<IDataNodeRepository>();
            var dataNodeID = Guid.NewGuid();
            var dataNodeDescriptor = new DataNodeDescriptor();
            mockDataNodeRepository.Setup(x => x.GetDataNodeDescriptorById(dataNodeID)).Returns(dataNodeDescriptor);

            var stubDateTimeProvider = new Mock<IDateTimeProvider>();
            var now = new DateTime(999);
            stubDateTimeProvider.Setup(x => x.Now).Returns(now);

            var dataNodeProtocol = new DataNodeProtocol(new Mock<ILog>().Object, mockDataNodeRepository.Object, stubDateTimeProvider.Object);

            // Act
            dataNodeProtocol.SendHeartbeat(dataNodeID);

            // Assert
            mockDataNodeRepository.Verify(x => x.UpdateDataNode(dataNodeID, It.Is<IDataNodeDescriptor>(dn => dn.LastUpdate == now.Ticks)), Times.Once);
        }

        [Test]
        public void SendHeartbeat_WithUnregisteredDataNodeID_DoesntUpdateLastUpdatedTime()
        {
            // Arrange
            var mockDataNodeRepository = new Mock<IDataNodeRepository>();
            mockDataNodeRepository.Setup(x => x.GetDataNodeDescriptorById(It.IsAny<Guid>())).Returns<IDataNodeDescriptor>(null);
            var stubDateTimeProvider = new Mock<IDateTimeProvider>();
            var dataNodeProtocol = new DataNodeProtocol(new Mock<ILog>().Object, mockDataNodeRepository.Object, stubDateTimeProvider.Object);

            // Act
            dataNodeProtocol.SendHeartbeat(Guid.NewGuid());

            // Assert
            mockDataNodeRepository.Verify(x => x.UpdateDataNode(It.IsAny<Guid>(), It.IsAny<IDataNodeDescriptor>()), Times.Never);
        }
    }
}
