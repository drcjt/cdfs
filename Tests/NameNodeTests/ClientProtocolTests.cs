using Moq;
using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
using NameNode.Services;
using NameNode.Services.Interfaces;
using NUnit.Framework;
using Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameNodeTests
{
    [TestFixture]
    class ClientProtocolTests
    {
        [Test]
        public void AddBlock_ValidFile_AllocatesBlockAndReturnsLocatedBlock()
        {
            var stubFileSystem = new Mock<IFileSystem>();
            var file = new File();
            stubFileSystem.Setup(x => x.GetFile(It.IsAny<string>())).Returns(file);

            var stubDataNodeRepository = new Mock<IDataNodeRepository>();
            var dataNodeId = Guid.NewGuid();
            stubDataNodeRepository.Setup(x => x.GetRandomDataNodeId()).Returns(dataNodeId);
            var dataNodeID = new DataNodeId() { HostName = "HostName", IPAddress = "IPAddress" };
            stubDataNodeRepository.Setup(x => x.GetDataNodeDescriptorById(dataNodeId)).Returns(dataNodeID);

            var clientProtocol = new ClientProtocol(stubFileSystem.Object, stubDataNodeRepository.Object);

            var result = clientProtocol.AddBlock("testFile");

            Assert.That(result.Locations, Has.Exactly(1).Matches<DataNodeId>(x => x.HostName == dataNodeID.HostName && x.IPAddress == dataNodeID.IPAddress));
        }
    }
}
