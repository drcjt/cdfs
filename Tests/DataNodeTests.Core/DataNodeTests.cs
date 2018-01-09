using DataNode.Core.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Protocols.Core;
using System;

namespace DataNodeTests.Core
{
    [TestFixture]
    public class DataNodeTests
    {
        [Test]
        public void Run_Always_RegistersDataNodeAndStartsHeartbeatTimer()
        {
            var mockDataNodeProtocol = new Mock<IDataNodeProtocol>();
            mockDataNodeProtocol.Setup(x => x.RegisterDataNode(It.IsAny<DataNodeId>())).Returns(Guid.NewGuid());

            var mockConfiguration = new Mock<IConfiguration>();

            var sut = new DataNodeService(mockDataNodeProtocol.Object, mockConfiguration.Object);

            sut.Run();

            mockDataNodeProtocol.VerifyAll();
        }
    }

}
