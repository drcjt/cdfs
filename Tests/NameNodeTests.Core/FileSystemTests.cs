using Microsoft.Extensions.Logging;
using Moq;
using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;

namespace NameNodeTests
{
    [TestFixture]
    class FileSystemTests
    {
        [Test]
        public void Root_Uninitialised_ReadsFileSystem()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var rootDirectoryName = "TheRoot";
            var directoryRoot = new Directory { Name = rootDirectoryName };

            stubFileSystemReaderWriter.Setup(x => x.ReadFileSystem()).Returns(directoryRoot);

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            // Act
            var root = fileSystem.Root;

            // Assert
            Assert.IsNull(root.Parent);
            Assert.AreEqual(rootDirectoryName, root.Name);
            Assert.AreEqual(0, root.Count());
            Assert.IsTrue(root is IDirectory);
        }

        [Test]
        public void CreateFile_ValidFilePath_AddsFile()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var rootDirectoryName = "TheRoot";
            var directoryRoot = new Directory { Name = rootDirectoryName };

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            stubFileSystemReaderWriter.Setup(x => x.WriteFileSystem(It.IsAny<IDirectory>()));
            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "", false)).Returns(directoryRoot);

            // Act
            fileSystem.Create("NewFile", "");

            // Assert
            Assert.AreEqual(1, directoryRoot.Count());
            Assert.IsTrue(directoryRoot.ElementAt<INode>(0) is IFile);
            Assert.AreEqual("NewFile", directoryRoot.ElementAt<INode>(0).Name);
        }

        [Test]
        public void CreateFile_InvalidFilePath_Throws()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "", false)).Returns<INode>(null);

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
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "", false)).Returns<INode>(null);

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
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            var parentDirectory = new Directory { Name = "Parent" };
            var fileToDelete = new File { Name = "NewFile" };
            parentDirectory.AddChild(fileToDelete);

            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewFile", false)).Returns(fileToDelete);

            // Act
            fileSystem.Delete("NewFile");

            // Assert
            Assert.AreEqual(0, parentDirectory.Count());
        }

        [Test]
        public void Mkdir_NonExistantParentDirectoryPath_Throws()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory", true)).Returns<INode>(null);

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
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            var mockRoot = new Directory { Name = "" };
            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "\\", true)).Returns(mockRoot);

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
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            var directoryRoot = new Directory { Name = "" };

            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory\\NewSubDirectory", true)).Returns(directoryRoot);

            stubFileSystemReaderWriter.Setup(x => x.WriteFileSystem(It.IsAny<IDirectory>()));

            // Act
            fileSystem.Mkdir("NewDirectory\\NewSubDirectory");

            // Assert
            Assert.AreEqual(1, directoryRoot.Count());

            var newDirectory = directoryRoot.ElementAt<INode>(0);
            Assert.AreEqual("NewDirectory", newDirectory.Name);
            Assert.IsTrue(newDirectory is IDirectory);

            var newSubDirectory = (newDirectory as IDirectory).ElementAt<INode>(0);
            Assert.AreEqual("NewSubDirectory", newSubDirectory.Name);
            Assert.IsTrue(newSubDirectory is IDirectory);
        }

        [Test]
        public void GetListing_InvalidDirectoryPath_Throws()
        {
            // Arrange
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var mockNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, mockNodeWalker.Object, stubFileSystemReaderWriter.Object);

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
            var stubLoggerFactory = new Mock<ILoggerFactory>();
            stubLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(new Mock<ILogger>().Object);
            var stubFileSystemReaderWriter = new Mock<IFileSystemReaderWriter>();
            var stubNodeWalker = new Mock<INodeWalker>();

            var fileSystem = new FileSystem(stubLoggerFactory.Object, stubNodeWalker.Object, stubFileSystemReaderWriter.Object);

            var rootDirectory = new Directory { Name = "" };
            rootDirectory.AddChild(new File { Name = "File1" });
            rootDirectory.AddChild(new File { Name = "File2" });
            rootDirectory.AddChild(new Directory { Name = "Directory" });

            stubNodeWalker.Setup(x => x.GetNodeByPath(fileSystem.Root, "NewDirectory\\NewSubDirectory", true)).Returns(rootDirectory);

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
