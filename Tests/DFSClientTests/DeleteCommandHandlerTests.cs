using DFSClient.Commands;
using DFSClient.Protocol;
using Moq;
using NUnit.Framework;

namespace DFSClientTests
{
    [TestFixture]
    class DeleteCommandHandlerTests
    {
        [Test]

        public void Handle_WithDeleteCommand_CallsDeleteAPI()
        {
            const string testFilePath = "TestFilePath";

            // Arrange
            var mockClientProtocol = new Mock<IRestClientProtocol>();
            var sut = new DeleteCommandHandler(mockClientProtocol.Object);
            var stubDeleteCommand = new DeleteCommand() { FilePath = testFilePath };

            // Act
            sut.Handle(stubDeleteCommand);

            // Assert
            mockClientProtocol.Verify(x => x.Delete(testFilePath));
        }
    }
}
