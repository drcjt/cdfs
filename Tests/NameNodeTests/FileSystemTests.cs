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
            Assert.That(root.Parent, Is.Null);
            Assert.That(root.Name, Is.Null);
            Assert.That(root.Count(), Is.EqualTo(0));
            Assert.That(root is IDirectory, Is.True);
        }

        [Test]
        public void Root_WhenFileSystemImageExists_ReturnsNodesRepresentingImage()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileImageLines = new string[] { "1,\"Root\",1", "0,\"File\"" };

            var deserializedRoot = new Directory { Name = "Root" };
            deserializedRoot.AddChild(new File { Name = "File" });

            mockFileSystemReaderWriter.Setup(x => x.FileSystemImageExists(It.IsAny<string>())).Returns(true);
            mockFileSystemReaderWriter.Setup(x => x.ReadFileSystemImageLines(It.IsAny<string>())).Returns(fileImageLines);
            mockFileSystemSerializer.Setup(x => x.Deserialize(fileImageLines)).Returns(deserializedRoot);

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            // Act
            var root = fileSystem.Root;

            // Assert
            Assert.That(root, Is.EqualTo(deserializedRoot));
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
        public void CreateFile_InExistingDirectory_AddsFileAndSavesFileSystem()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockFileSystemSerializer.Setup(x => x.Serialize(fileSystem.Root)).Returns("SerializedFileSystemImage");
            mockFileSystemReaderWriter.Setup(x => x.WriteFileSystemImage("FSImage", "SerializedFileSystemImage"));
            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "", false)).Returns(fileSystem.Root);

            // Act
            fileSystem.Create("NewFile", "");

            // Assert
            Assert.AreEqual(1, fileSystem.Root.Count());
            Assert.IsTrue(fileSystem.Root.ElementAt<INode>(0) is IFile);
            Assert.AreEqual("NewFile", fileSystem.Root.ElementAt<INode>(0).Name);

            mockFileSystemReaderWriter.VerifyAll();
            mockFileSystemSerializer.VerifyAll();
        }

        [Test]
        public void CreateFile_InNonExistantDirectory_ThrowsArgumentException()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "", false)).Returns<INode>(null);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => fileSystem.Create("NewFile", "NonExistantDirectory"));

            // Assert
            Assert.That(ex.Message, Does.StartWith("Path does not exist"));
            Assert.That(ex.ParamName, Is.EqualTo("directoryPath"));
        }

        [Test]
        public void DeleteFile_ThatDoesntExist_ThrowsArgumentException()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "", false)).Returns<INode>(null);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => fileSystem.Delete("NewFile"));

            // Assert
            Assert.That(ex.Message, Does.StartWith("Path does not exist"));
            Assert.That(ex.ParamName, Is.EqualTo("filePath"));
        }

        [Test]
        public void DeleteFile_ThatExists_RemovesFileAndSavesFileSystem()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            var parentOfNodeToDelete = new Directory { Name = "Parent" };
            var nodeToDelete = new File { Name = "NewFile" };
            parentOfNodeToDelete.AddChild(nodeToDelete);

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewFile", false)).Returns(nodeToDelete);

            // Act
            fileSystem.Delete("NewFile");

            // Assert
            Assert.That(parentOfNodeToDelete.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Mkdir_WithNonExistantParentDirectory_ThrowsArgumentException()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory", true)).Returns<INode>(null);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => fileSystem.Mkdir("NewDirectory"));

            // Assert
            Assert.That(ex.Message, Does.StartWith("Parent directory does not exist"));
            Assert.That(ex.ParamName, Is.EqualTo("directoryPath"));
        }

        [Test]
        public void Mkdir_WithOnlyRootDirectory_ThrowsArgumentException()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            var mockRoot = new Directory { Name = "" };
            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "\\", true)).Returns(mockRoot);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => fileSystem.Mkdir("\\"));

            // Assert
            Assert.That(ex.Message, Does.StartWith("Must specify a directory to create"));
            Assert.That(ex.ParamName, Is.EqualTo("directoryPath"));
        }

        [Test]
        public void Mkdir_MultipleMissingDirectories_AddsDirectories()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            var mockRoot = new Directory { Name = "" };

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory\\NewSubDirectory", true)).Returns(mockRoot);

            mockFileSystemSerializer.Setup(x => x.Serialize(fileSystem.Root)).Returns("SerializedFileSystemImage");
            mockFileSystemReaderWriter.Setup(x => x.WriteFileSystemImage("FSImage", "SerializedFileSystemImage"));

            // Act
            fileSystem.Mkdir("NewDirectory\\NewSubDirectory");

            // Assert
            Assert.That(mockRoot.Count(), Is.EqualTo(1));

            var newDirectory = mockRoot.ElementAt<INode>(0);
            Assert.That(newDirectory.Name, Is.EqualTo("NewDirectory"));
            Assert.That(newDirectory is IDirectory, Is.True);

            var newSubDirectory = (newDirectory as IDirectory).ElementAt<INode>(0);
            Assert.That(newSubDirectory.Name, Is.EqualTo("NewSubDirectory"));
            Assert.That(newSubDirectory is IDirectory, Is.True);

            // Assert
            mockFileSystemReaderWriter.VerifyAll();
            mockFileSystemSerializer.VerifyAll();
        }
    }
}