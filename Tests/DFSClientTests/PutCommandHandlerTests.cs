using DFSClient.Commands;
using DFSClient.Protocol;
using Moq;
using NUnit.Framework;
using Protocols;

namespace DFSClientTests
{
    [TestFixture]
    class PutCommandHandlerTests
    {
        [Test, Ignore("Needs Fixing to work with DataTransferProtocol implementation")]
        public void Handle_WithPutCommand_CallsPutAPI()
        {
            const string testFilePath = "TestFilePath";
            const string testSrcFile = "TestSrcFile";

            // Arrange
            var mockClientProtocol = new Mock<IRestClientProtocol>();
            var locatedBlock = new LocatedBlock() { Locations = new DataNodeId[] { new DataNodeId { IPAddress = "http://localhost" } } };
            mockClientProtocol.Setup(x => x.AddBlock(It.IsAny<string>())).Returns(locatedBlock);
            var stubDataTransferProtocol = new Mock<IRestDataTransferProtocol>();
            var sut = new PutCommandHandler(mockClientProtocol.Object, stubDataTransferProtocol.Object);
            var stubPutCommand = new PutCommand() { FilePath = testFilePath, SrcFile = testSrcFile };

            // Act
            sut.Handle(stubPutCommand);

            // Assert
            mockClientProtocol.Verify(x => x.Create(testSrcFile, testFilePath));
        }
    }
}
