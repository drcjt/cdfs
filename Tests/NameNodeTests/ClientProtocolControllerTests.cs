using Moq;
using NameNode.Controllers;
using NUnit.Framework;
using Protocols;
using System.Collections.Generic;

namespace NameNodeTests
{
    [TestFixture]
    class ClientProtocolControllerTests
    {
        [Test]
        public void Create_Always_InvokesCreate()
        {
            // Arrange
            var mockClientProtocol = new Mock<IClientProtocol>();
            mockClientProtocol.Setup(x => x.Create("srcFile", "filePath"));
            var controller = new ClientProtocolController(mockClientProtocol.Object);

            // Act
            controller.Create("srcFile", "filePath");

            // Assert
            mockClientProtocol.VerifyAll();
        }

        [Test]
        public void Delete_Always_InvokesDelete()
        {
            // Arrange
            var mockClientProtocol = new Mock<IClientProtocol>();
            mockClientProtocol.Setup(x => x.Delete("filePath"));
            var controller = new ClientProtocolController(mockClientProtocol.Object);

            // Act
            controller.Delete("filePath");

            // Assert
            mockClientProtocol.VerifyAll();
        }

        [Test]
        public void Mkdir_Always_InvokesMkdir()
        {
            // Arrange
            var mockClientProtocol = new Mock<IClientProtocol>();
            mockClientProtocol.Setup(x => x.Mkdir("directoryPath"));
            var controller = new ClientProtocolController(mockClientProtocol.Object);

            // Act
            controller.Mkdir("directoryPath");

            // Assert
            mockClientProtocol.VerifyAll();
        }

        [Test]
        public void GetListing_Always_InvokesGetListing()
        {
            // Arrange
            var mockClientProtocol = new Mock<IClientProtocol>();
            var mockListing = new List<CdfsFileStatus> { new CdfsFileStatus() };
            mockClientProtocol.Setup(x => x.GetListing("filePath")).Returns(mockListing);
            var controller = new ClientProtocolController(mockClientProtocol.Object);

            // Act
            var result = controller.GetListing("filePath");

            // Assert
            CollectionAssert.AreEqual(mockListing, result);
            mockClientProtocol.VerifyAll();
        }
    }
}
