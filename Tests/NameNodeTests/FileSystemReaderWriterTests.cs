using log4net;
using Moq;
using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NameNodeTests
{
    [TestFixture]
    class FileSystemReaderWriterTests
    {
        [Test]
        public void WriteFileSystem_EmptyRootDirectory_SerailizesAndWritesDirectory()
        {
            // Arrange
            var rootDirectory = new Directory { Name = "Root" };

            var stubLogger = new Mock<ILog>();
            var mockFileSystemImageFile = new Mock<IFileSystemImageFile>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();

            var serializedRootLines = new List<string> { "Serialized Root" };

            mockFileSystemSerializer.Setup(x => x.Serialize(rootDirectory)).Returns(serializedRootLines);
            mockFileSystemImageFile.Setup(x => x.WriteFileSystemImage(serializedRootLines));

            var fileSystemReaderWriter = new FileSystemReaderWriter(stubLogger.Object, mockFileSystemSerializer.Object, mockFileSystemImageFile.Object);

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
            var stubLogger = new Mock<ILog>();
            var stubFileSystemImageFile = new Mock<IFileSystemImageFile>();
            var stubFileSystemSerializer = new Mock<IFileSystemSerializer>();

            stubFileSystemImageFile.Setup(x => x.FileSystemImageExists()).Returns(false);

            var fileSystemReaderWriter = new FileSystemReaderWriter(stubLogger.Object, stubFileSystemSerializer.Object, stubFileSystemImageFile.Object);

            // Act
            var result = fileSystemReaderWriter.ReadFileSystem();

            // Assert
            Assert.IsTrue(result is IDirectory);
            Assert.AreEqual(null, result.Name);
            Assert.IsNull(result.Parent);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReadFileSystem_ValidFile_ReadsAndDeserializesFile()
        {
            // Arrange
            var stubLogger = new Mock<ILog>();
            var stubFileSystemImageFile = new Mock<IFileSystemImageFile>();
            var stubFileSystemSerializer = new Mock<IFileSystemSerializer>();

            stubFileSystemImageFile.Setup(x => x.FileSystemImageExists()).Returns(true);

            var serializedRootLines = new List<string> { "Serialized Root" };

            var deserializedRootDirectory = new Directory { Name = "Root" };

            stubFileSystemImageFile.Setup(x => x.ReadFileSystemImageLines()).Returns(serializedRootLines);
            stubFileSystemSerializer.Setup(x => x.Deserialize(serializedRootLines)).Returns(deserializedRootDirectory);

            var fileSystemReaderWriter = new FileSystemReaderWriter(stubLogger.Object, stubFileSystemSerializer.Object, stubFileSystemImageFile.Object);

            // Act
            var result = fileSystemReaderWriter.ReadFileSystem();

            // Assert
            Assert.IsTrue(result is IDirectory);
            Assert.AreEqual("Root", result.Name);
            Assert.IsNull(result.Parent);
            Assert.AreEqual(0, result.Count());
        }
    }
}
