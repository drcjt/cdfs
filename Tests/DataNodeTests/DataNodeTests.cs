using DataNode.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Protocols;
using System;

namespace DataNodeTests
{
    [TestFixture]
    public class DataNodeTests
    {
        [Test]
        public void Run_Always_RegistersDataNodeAndStartsHeartbeatTimer()
        {
            // Arrange
            var mockDataNodeProtocol = new Mock<IDataNodeProtocol>();
            mockDataNodeProtocol.Setup(x => x.RegisterDataNode(It.IsAny<DataNodeId>())).Returns(Guid.NewGuid());

            var mockConfiguration = new Mock<IConfiguration>();

            var sut = new DataNodeService(mockDataNodeProtocol.Object, mockConfiguration.Object);

            // Act
            sut.Run();

            // Assert
            mockDataNodeProtocol.VerifyAll();
        }

        [Test]
        public void SendHeartbeat_Always_SendsAHeartbeat()
        {
            // Arrange
            var mockDataNodeProtocol = new Mock<IDataNodeProtocol>();
            mockDataNodeProtocol.Setup(x => x.SendHeartbeat(It.IsAny<Guid>()));

            var mockConfiguration = new Mock<IConfiguration>();

            var sut = new DataNodeService(mockDataNodeProtocol.Object, mockConfiguration.Object);

            // Act
            sut.SendHeartbeat(null, null);

            // Assert
            mockDataNodeProtocol.VerifyAll();
        }
    }
}
