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
            CdfsFileStatus expectedFileStatus = new CdfsFileStatus();
            IList<CdfsFileStatus> expectedFileList = new List<CdfsFileStatus>() { expectedFileStatus };

            // Arrange
            var mockClientProtocol = new Mock<IRestClientProtocol>();
            var mockConsole = new Mock<IConsole>();
            var sut = new ListingCommandHandler(mockClientProtocol.Object, mockConsole.Object);
            var stubListingCommand = new ListingCommand() { FilePath = testFilePath };
            mockClientProtocol.Setup(x => x.GetListing(testFilePath)).Returns(expectedFileList);

            // Act
            sut.Handle(stubListingCommand);

            // Assert
            mockClientProtocol.Verify(x => x.GetListing(testFilePath));
            mockConsole.Verify(x => x.WriteLine(expectedFileStatus));
        }
    }
}
