using log4net;
using Moq;
using NameNode.FileSystem;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNodeTests
{
    [TestFixture]
    class FileSystemTests
    {
        [Test]
        public void Root_WhenFileSystemImageDoesntExist_ReturnsNewDirectory()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            mockFileSystemReaderWriter.Setup(x => x.FileSystemImageExists(It.IsAny<string>())).Returns(false);

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            // Act
            var root = fileSystem.Root;

            // Assert
            Assert.IsNull(root.Parent);
            Assert.IsNull(root.Name);
            Assert.AreEqual(0, root.Count());
            Assert.IsTrue(root is IDirectory);
        }

        [Test]
        public void Root_WhenFileSystemImageExists_ReturnsNodesRepresentingImage()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var fileSystemSerializer = new FileSystemSerializer();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var mockFileSystemImageLines = new string[] { "1,\"Root\",1", "0,\"File\"" };

            mockFileSystemReaderWriter.Setup(x => x.FileSystemImageExists(It.IsAny<string>())).Returns(true);
            mockFileSystemReaderWriter.Setup(x => x.ReadFileSystemImageLines(It.IsAny<string>())).Returns(mockFileSystemImageLines);

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, fileSystemSerializer, mockFileSystemReaderWriter.Object);

            // Act
            var root = fileSystem.Root;

            // Assert
            Assert.IsNull(root.Parent);
            Assert.AreEqual("Root", root.Name);
            Assert.AreEqual(1, root.Count());
            Assert.IsTrue(root is IDirectory);

            Assert.AreEqual("File", root.ElementAt<INode>(0).Name);
            Assert.IsTrue(root.ElementAt<INode>(0) is IFile);
        }

        [Test]
        public void SaveFileImage_Always_SerializesAndWritesFileSystem()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockFileSystemSerializer.Setup(x => x.Serialize(fileSystem.Root)).Returns("SerializedFileSystemImage");
            mockFileSystemReaderWriter.Setup(x => x.WriteFileSystemImage("FSImage", "SerializedFileSystemImage"));

            // Act
            fileSystem.SaveFileImage();

            // Assert
            mockFileSystemReaderWriter.VerifyAll();
            mockFileSystemSerializer.VerifyAll();
        }

        [Test]
        public void CreateFile_WithDefaultRoot_AddsFileAndSavesFileSystem()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var nodeWalker = new NodeWalker();

            var fileSystem = new FileSystem(mockLogger.Object, nodeWalker, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockFileSystemSerializer.Setup(x => x.Serialize(fileSystem.Root)).Returns("SerializedFileSystemImage");
            mockFileSystemReaderWriter.Setup(x => x.WriteFileSystemImage("FSImage", "SerializedFileSystemImage"));

            // Act
            fileSystem.Create("NewFile", "");

            // Assert
            Assert.AreEqual(1, fileSystem.Root.Count());
            Assert.IsTrue(fileSystem.Root.ElementAt<INode>(0) is IFile);
            Assert.AreEqual("NewFile", fileSystem.Root.ElementAt<INode>(0).Name);

            mockFileSystemReaderWriter.VerifyAll();
            mockFileSystemSerializer.VerifyAll();
        }


    }
}
