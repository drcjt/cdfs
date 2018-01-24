using DFSClient.Commands;
using DFSClient.Protocol;
using Moq;
using NUnit.Framework;

namespace DFSClientTests
{
    [TestFixture]
    class PutCommandHandlerTests
    {
        [Test]
        public void Handle_WithPutCommand_CallsPutAPI()
        {
            const string testFilePath = "TestFilePath";
            const string testSrcFile = "TestSrcFile";

            // Arrange
            var mockClientProtocol = new Mock<IRestClientProtocol>();
            var sut = new PutCommandHandler(mockClientProtocol.Object);
            var stubPutCommand = new PutCommand() { FilePath = testFilePath, SrcFile = testSrcFile };

            // Act
            sut.Handle(stubPutCommand);

            // Assert
            mockClientProtocol.Verify(x => x.Create(testSrcFile, testFilePath));
        }
    }
}
