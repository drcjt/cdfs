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
            var mockLogger = new Mock<ILog>();
            var mockDataNodeRepository = new Mock<IDataNodeRepository>();
            var dataNodeProtocol = new DataNodeProtocol(mockLogger.Object, mockDataNodeRepository.Object);
            var dataNodeRegistration = new DataNodeRegistration();

            mockDataNodeRepository.Setup(x => x.AddDataNode(It.IsAny<DataNodeDescriptor>())).Returns(Guid.NewGuid());

            var dataNodeID = dataNodeProtocol.RegisterDataNode(dataNodeRegistration);

            mockDataNodeRepository.VerifyAll();
        }
    }
}
