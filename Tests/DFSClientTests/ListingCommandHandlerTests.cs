using DFSClient;
using DFSClient.Commands;
using DFSClient.Protocol;
using Moq;
using NUnit.Framework;
using Protocols;
using System.Collections.Generic;

namespace DFSClientTests
{
    [TestFixture]
    class ListingCommandHandlerTests
    {
        [Test]
        public void Handle_WithPutCommand_CallsPutAPI()
        {
            const string testFilePath = "TestFilePath";
            var expectedFileStatus = new CdfsFileStatus();
            var expectedFileList = new List<CdfsFileStatus>() { expectedFileStatus };

            // Arrange
            var stubClientProtocol = new Mock<IRestClientProtocol>();
            var mockConsole = new Mock<IConsole>();
            var sut = new ListingCommandHandler(stubClientProtocol.Object, mockConsole.Object);
            var listingCommand = new ListingCommand() { FilePath = testFilePath };
            stubClientProtocol.Setup(x => x.GetListing(testFilePath)).Returns(expectedFileList);

            // Act
            sut.Handle(listingCommand);

            // Assert
            mockConsole.Verify(x => x.WriteLine(expectedFileStatus));
        }
    }
}
