using DFSClient;
using Moq;
using NUnit.Framework;
using Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClientTests
{
    [TestFixture]
    class ClientHostTests
    {
        private const string testNameNodeUri = "http://127.0.0.1";
        private const string testDirectoryPath = "\\MyDirectory";
        private const string testFilePath = "TestFile";

        [Test]
        public void Run_WithListingSubOptions_WritesFileDetailsToConsole()
        {
            // Arrange
            var stubOptionParser = new Mock<IOptionParser>();
            var listingSubOptions = new ListingSubOptions() { FilePath = new List<string> { testDirectoryPath }, NameNodeUri = testNameNodeUri };
            stubOptionParser.Setup(x => x.ParseOptions(It.IsAny<string[]>())).Returns(listingSubOptions);
            var mockConsole = new Mock<IConsole>();
            var stubClientProtocol = new Mock<IRestClientProtocol>();
            var fileStatus = new CdfsFileStatus { FilePath = testFilePath, IsDirectory = false, Length = 123 };
            var files = new List<CdfsFileStatus>() { fileStatus };
            stubClientProtocol.Setup(x => x.GetListing(testDirectoryPath)).Returns(files);

            var sut = new ClientHost(stubOptionParser.Object, stubClientProtocol.Object, mockConsole.Object);

            // Act
            sut.Run(new string[] { "ls", testDirectoryPath });

            // Assert
            mockConsole.Verify(x => x.WriteLine(fileStatus));
        }

        [Test]
        public void Run_WithPutSubOptions_CallsClientProtocolCreateMethod()
        {
            // Arrange
            var stubOptionParser = new Mock<IOptionParser>();
            var putSubOptions = new PutSubOptions() { PutValues = new List<string> { testFilePath, testDirectoryPath }, NameNodeUri = testNameNodeUri };
            stubOptionParser.Setup(x => x.ParseOptions(It.IsAny<string[]>())).Returns(putSubOptions);
            var mockConsole = new Mock<IConsole>();
            var stubClientProtocol = new Mock<IRestClientProtocol>();

            var sut = new ClientHost(stubOptionParser.Object, stubClientProtocol.Object, mockConsole.Object);

            // Act
            sut.Run(new string[] { "put", testFilePath, testDirectoryPath });

            // Assert
            stubClientProtocol.Verify(x => x.Create(testFilePath, testDirectoryPath));
        }

        [Test]
        public void Run_WithDeleteSubOptions_CallsClientProtocolDeleteMethod()
        {
            // Arrange
            var stubOptionParser = new Mock<IOptionParser>();
            var deleteSubOptions = new DeleteSubOptions() { FilePath = new List<string> { testFilePath }, NameNodeUri = testNameNodeUri };
            stubOptionParser.Setup(x => x.ParseOptions(It.IsAny<string[]>())).Returns(deleteSubOptions);
            var mockConsole = new Mock<IConsole>();
            var stubClientProtocol = new Mock<IRestClientProtocol>();

            var sut = new ClientHost(stubOptionParser.Object, stubClientProtocol.Object, mockConsole.Object);

            // Act
            sut.Run(new string[] { "rm", testFilePath });

            // Assert
            stubClientProtocol.Verify(x => x.Delete(testFilePath));
        }

        [Test]
        public void Run_WithMkdirSubOptions_CallsClientProtocolMkdirMethod()
        {
            // Arrange
            var stubOptionParser = new Mock<IOptionParser>();
            var mkdirSubOptions = new MkdirSubOptions() { DirectoryPath = new List<string> { testDirectoryPath }, NameNodeUri = testNameNodeUri };
            stubOptionParser.Setup(x => x.ParseOptions(It.IsAny<string[]>())).Returns(mkdirSubOptions);
            var mockConsole = new Mock<IConsole>();
            var stubClientProtocol = new Mock<IRestClientProtocol>();

            var sut = new ClientHost(stubOptionParser.Object, stubClientProtocol.Object, mockConsole.Object);

            // Act
            sut.Run(new string[] { "mkdir", testDirectoryPath });

            // Assert
            stubClientProtocol.Verify(x => x.Mkdir(testDirectoryPath));
        }

    }
}
