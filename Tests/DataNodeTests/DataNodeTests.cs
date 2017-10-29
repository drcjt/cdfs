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
            var mockDataNodeProtocol = new Mock<IDataNodeProtocol>();
            mockDataNodeProtocol.Setup(x => x.RegisterDataNode(It.IsAny<DataNodeRegistration>())).Returns(Guid.NewGuid());
            var sut = new DataNode.DataNode(mockDataNodeProtocol.Object);

            sut.Run();

            mockDataNodeProtocol.VerifyAll();
        }
    }
}
