using DFSClient.Commands;
using DFSClient.Protocol;
using Moq;
using NUnit.Framework;

namespace DFSClientTests
{
    [TestFixture]
    class MkdirCommandHandlerTests
    {
        [Test]
        public void Handle_WithMkdirCommand_CallsMkdirAPI()
        {
            const string testDirectoryPath = "TestDirectoryPath";

            // Arrange
            var mockClientProtocol = new Mock<IRestClientProtocol>();
            var sut = new MkdirCommandHandler(mockClientProtocol.Object);
            var stubMkdirCommand = new MkdirCommand() { DirectoryPath = testDirectoryPath };

            // Act
            sut.Handle(stubMkdirCommand);

            // Assert
            mockClientProtocol.Verify(x => x.Mkdir(testDirectoryPath));
        }
    }
}
