using DFSClient;
using DFSClient.CommandBuilders;
using DFSClient.Commands;
using DFSClient.Options;
using DFSClient.Protocol;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DFSClientTests
{
    [TestFixture]
    class ClientHostTests
    {
        private const string testNameNodeUri = "http://127.0.0.1";

        [Test]
        public void Run_WithNullArgs_InvokesCommandDispatcher()
        {
            // Arrange
            var stubOptionParser = new Mock<IOptionParser>();
            var listingSubOptions = new ListingSubOptions() { NameNodeUri = testNameNodeUri, FilePath = new List<string> { } };
            stubOptionParser.Setup(x => x.ParseOptions(It.IsAny<string[]>())).Returns(listingSubOptions);
            var mockClientProtocol = new Mock<IRestClientProtocol>();
            mockClientProtocol.SetupProperty(x => x.BaseUrl);
            var mockCommandDispatcher = new Mock<ICommandDispatcher>();
            var stubCommandFactory = new Mock<ICommandFactory>();

            var sut = new ClientHost(stubOptionParser.Object, mockClientProtocol.Object, mockCommandDispatcher.Object, stubCommandFactory.Object);

            // Act
            sut.Run(new string[] { });

            // Assert
            mockCommandDispatcher.Verify(x => x.Dispatch(It.IsAny<ICommand>()));
            Assert.AreEqual(testNameNodeUri, mockClientProtocol.Object.BaseUrl.OriginalString);
        }
    }
}
