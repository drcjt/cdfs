using DataNode.Options;
using DataNode.ProtocolWrappers;
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
            var mockDataNodeProtocol = new Mock<IRestDataNodeProtocol>();
            mockDataNodeProtocol.Setup(x => x.RegisterDataNode(It.IsAny<DataNodeId>())).Returns(Guid.NewGuid());

            var stubConfiguration = new Mock<IConfiguration>();

            var stubDataNodeOptions = new Mock<IDataNodeOptions>();
            stubDataNodeOptions.SetupGet(x => x.NameNodeUri).Returns("http://localhost");

            var sut = new DataNodeService(mockDataNodeProtocol.Object, stubConfiguration.Object, stubDataNodeOptions.Object);

            // Act
            sut.Run();

            // Assert
            mockDataNodeProtocol.VerifyAll();
        }

        [Test]
        public void SendHeartbeat_Always_SendsAHeartbeat()
        {
            // Arrange
            var mockDataNodeProtocol = new Mock<IRestDataNodeProtocol>();
            mockDataNodeProtocol.Setup(x => x.SendHeartbeat(It.IsAny<Guid>()));

            var stubConfiguration = new Mock<IConfiguration>();

            var stubDataNodeOptions = new Mock<IDataNodeOptions>();

            var sut = new DataNodeService(mockDataNodeProtocol.Object, stubConfiguration.Object, stubDataNodeOptions.Object);

            // Act
            sut.SendHeartbeat(null, null);

            // Assert
            mockDataNodeProtocol.VerifyAll();
        }
    }
}
