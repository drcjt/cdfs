using Microsoft.Extensions.Logging;
using Moq;
using NameNode.Core.FileSystem;
using NameNode.Core.FileSystem.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameNodeTests.Core
{
    [TestFixture]
    class FileSystemReaderWriterTests
    {
        [Test]
        public void WriteFileSystem_EmptyRootDirectory_SerailizesAndWritesDirectory()
        {
            // Arrange
            var rootDirectory = new Directory { Name = "Root" };

            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);

            var mockFileSystemImageFile = new Mock<IFileSystemImageFile>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();

            var serializedRootLines = new List<string> { "Serialized Root" };

            mockFileSystemSerializer.Setup(x => x.Serialize(rootDirectory)).Returns(serializedRootLines);
            mockFileSystemImageFile.Setup(x => x.WriteFileSystemImage(serializedRootLines));

            var fileSystemReaderWriter = new FileSystemReaderWriter(stubLoggerFactory.Object, mockFileSystemSerializer.Object, mockFileSystemImageFile.Object);

            // Act
            fileSystemReaderWriter.WriteFileSystem(rootDirectory);

            // Verify mocks?
            mockFileSystemImageFile.VerifyAll();
            mockFileSystemSerializer.VerifyAll();
        }

        [Test]
        public void ReadFileSystem_MissingFile_ReturnsEmptyDirectory()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemImageFile = new Mock<IFileSystemImageFile>();
            var stubFileSystemSerializer = new Mock<IFileSystemSerializer>();

            stubFileSystemImageFile.Setup(x => x.FileSystemImageExists()).Returns(false);

            var fileSystemReaderWriter = new FileSystemReaderWriter(stubLoggerFactory.Object, stubFileSystemSerializer.Object, stubFileSystemImageFile.Object);

            // Act
            var result = fileSystemReaderWriter.ReadFileSystem();

            // Assert
            Assert.IsTrue(result is IDirectory);
            Assert.AreEqual(null, result.Name);
            Assert.IsNull(result.Parent);
            Assert.AreEqual(0, result.ChildCount);
        }

        [Test]
        public void ReadFileSystem_ValidFile_ReadsAndDeserializesFile()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemImageFile = new Mock<IFileSystemImageFile>();
            var stubFileSystemSerializer = new Mock<IFileSystemSerializer>();

            stubFileSystemImageFile.Setup(x => x.FileSystemImageExists()).Returns(true);

            var serializedRootLines = new List<string> { "Serialized Root" };

            var deserializedRootDirectory = new Directory { Name = "Root" };

            stubFileSystemImageFile.Setup(x => x.ReadFileSystemImageLines()).Returns(serializedRootLines);
            stubFileSystemSerializer.Setup(x => x.Deserialize(serializedRootLines)).Returns(deserializedRootDirectory);

            var fileSystemReaderWriter = new FileSystemReaderWriter(stubLoggerFactory.Object, stubFileSystemSerializer.Object, stubFileSystemImageFile.Object);

            // Act
            var result = fileSystemReaderWriter.ReadFileSystem();

            // Assert
            Assert.IsTrue(result is IDirectory);
            Assert.AreEqual("Root", result.Name);
            Assert.IsNull(result.Parent);
            Assert.AreEqual(0, result.ChildCount);
        }
    }
}
