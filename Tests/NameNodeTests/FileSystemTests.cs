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
        public void Root_MissingFileSystemImage_ReturnsNewDirectory()
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
        public void Root_ValidFileSystemImage_ReturnsNodesRepresentingImage()
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
            Assert.AreEqual(deserializedRoot, root);
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
        public void CreateFile_ValidFilePath_AddsFileAndSavesFileSystem()
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
        public void CreateFile_InvalidFilePath_Throws()
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
            StringAssert.Contains("Path does not exist", ex.Message);
            Assert.AreEqual("directoryPath", ex.ParamName);
        }

        [Test]
        public void DeleteFile_InvalidFilePath_Throws()
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
            StringAssert.Contains("Path does not exist", ex.Message);
            Assert.AreEqual("filePath", ex.ParamName);
        }

        [Test]
        public void DeleteFile_ValidFilePath_RemovesFileAndSavesFileSystem()
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
            Assert.AreEqual(0, parentOfNodeToDelete.Count());
        }

        [Test]
        public void Mkdir_NonExistantParentDirectoryPath_Throws()
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
            StringAssert.Contains("Parent directory does not exist", ex.Message);
            Assert.AreEqual("directoryPath", ex.ParamName);
        }

        [Test]
        public void Mkdir_RootDirectoryPath_Throws()
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
            StringAssert.Contains("Must specify a directory to create", ex.Message);
            Assert.AreEqual("directoryPath", ex.ParamName);
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
            Assert.AreEqual(1, mockRoot.Count());

            var newDirectory = mockRoot.ElementAt<INode>(0);
            Assert.AreEqual("NewDirectory", newDirectory.Name);
            Assert.IsTrue(newDirectory is IDirectory);

            var newSubDirectory = (newDirectory as IDirectory).ElementAt<INode>(0);
            Assert.AreEqual("NewSubDirectory", newSubDirectory.Name);
            Assert.IsTrue(newSubDirectory is IDirectory);

            // Assert
            mockFileSystemReaderWriter.VerifyAll();
            mockFileSystemSerializer.VerifyAll();
        }

        [Test]
        public void GetListing_InvalidDirectoryPath_Throws()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory\\NewSubDirectory", true)).Returns<INode>(null);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => fileSystem.GetListing("\\"));

            // Asssert
            StringAssert.Contains("Path does not exist", ex.Message);
            Assert.AreEqual("directoryPath", ex.ParamName);
        }

        [Test]
        public void GetListing_ValidDirectoryPath_ReturnsDirectoryListing()
        {
            // Arrange
            var mockLogger = new Mock<ILog>();
            var mockFileSystemSerializer = new Mock<IFileSystemSerializer>();
            var mockFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(mockLogger.Object, mockNodeWalker.Object, mockFileSystemSerializer.Object, mockFileSystemReaderWriter.Object);

            var mockRoot = new Directory { Name = "" };
            mockRoot.AddChild(new File { Name = "File1" });
            mockRoot.AddChild(new File { Name = "File2" });
            mockRoot.AddChild(new Directory { Name = "Directory" });

            mockNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory\\NewSubDirectory", true)).Returns(mockRoot);

            // Act
            var result = fileSystem.GetListing("NewDirectory\\NewSubDirectory");

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Select(x => x.Name).Contains("File1"));
            Assert.IsTrue(result.Select(x => x.Name).Contains("File2"));
            Assert.IsTrue(result.Select(x => x.Name).Contains("Directory"));
        }
    }
}