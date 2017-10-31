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
        Mock<IDataNodeRepository> _mockDataNodeRepository;
        Mock<IDateTimeProvider> _mockDateTimeProvider;
        DataNodeProtocol _sut;

        [SetUp]
        public void Setup()
        {
            _mockDataNodeRepository = new Mock<IDataNodeRepository>();
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _sut = new DataNodeProtocol(new Mock<ILog>().Object, _mockDataNodeRepository.Object, _mockDateTimeProvider.Object);
        }

        [Test]
        public void RegisterDataNode_WhenNodeNotPreviouslyRegistered_DataNodeIsRegistered()
        {
            // Arrange
            var dataNodeRegistration = new DataNodeRegistration();
            _mockDataNodeRepository.Setup(x => x.AddDataNode(It.IsAny<DataNodeDescriptor>())).Returns(Guid.NewGuid());

            // Act
            var dataNodeID = _sut.RegisterDataNode(dataNodeRegistration);

            // Assert
            _mockDataNodeRepository.VerifyAll();
        }

        [Test]
        public void SendHeartbeat_WithRegisteredDataNodeID_UpdatesLastUpdatedTime()
        {
            // Arrange
            var dataNodeID = Guid.NewGuid();
            var mockDataNodeDescriptor = new Mock<IDataNodeDescriptor>();
            _mockDataNodeRepository.Setup(x => x.GetDataNodeDescriptorById(dataNodeID)).Returns(mockDataNodeDescriptor.Object);

            var now = new DateTime(999);
            _mockDateTimeProvider.Setup(x => x.Now).Returns(now);

            // Act
            _sut.SendHeartbeat(dataNodeID);

            // Assert
            mockDataNodeDescriptor.VerifySet(m => m.LastUpdate = now.Ticks);
        }

        [Test]
        public void SendHeartbeat_WithUnregisteredDataNodeID_DoesntUpdateLastUpdatedTime()
        {
            // Arrange
            _mockDataNodeRepository.Setup(x => x.GetDataNodeDescriptorById(It.IsAny<Guid>())).Returns<IDataNodeDescriptor>(null);

            // Act
            _sut.SendHeartbeat(Guid.NewGuid());

            // Assert
            _mockDataNodeRepository.VerifyAll();
        }
    }
}
