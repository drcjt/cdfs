using Moq;
using NameNode.Controllers;
using NUnit.Framework;
using Protocols;
using System;

namespace NameNodeTests
{
    [TestFixture]
    class DataNodeProtocolControllerTests
    {
        [Test]
        public void Register_Always_InvokesRegister()
        {
            // Arrange
            var mockDataNodeProtocol = new Mock<IDataNodeProtocol>();
            var dataNodeId = new DataNodeId();
            mockDataNodeProtocol.Setup(x => x.RegisterDataNode(dataNodeId));
            var controller = new DataNodeProtocolController(mockDataNodeProtocol.Object);

            // Act
            controller.Register(dataNodeId);

            // Assert
            mockDataNodeProtocol.VerifyAll();
        }

        [Test]
        public void SendHeartbeat_Always_InvokesSendHeartbeat()
        {
            // Arrange
            var mockDataNodeProtocol = new Mock<IDataNodeProtocol>();
            var dataNodeId = Guid.NewGuid();
            mockDataNodeProtocol.Setup(x => x.SendHeartbeat(dataNodeId));
            var controller = new DataNodeProtocolController(mockDataNodeProtocol.Object);

            // Act
            controller.SendHeartbeat(dataNodeId);

            // Assert
            mockDataNodeProtocol.VerifyAll();
        }
    }
}
