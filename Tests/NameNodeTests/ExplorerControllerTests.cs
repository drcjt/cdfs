using Moq;
using NameNode.Controllers;
using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
using NameNode.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace NameNodeTests
{
    [TestFixture]
    class ExplorerControllerTests
    {
        [Test]
        public void CreateModel_ValidDirectoryPath_ReturnsStatusModel()
        {
            const string directoryPath = "directoryPath";
            const string directoryName = "directory";
            const string subDirectoryName = "subdirectory";
            const string fileName = "file";
            string fileFullPath = System.IO.Path.Combine(directoryName, subDirectoryName, fileName);
            var rootNode = new Directory { Name = directoryName };
            var childDirectory = new Directory { Name = subDirectoryName, Parent = rootNode };
            var childFile = new File { Name = fileName, Parent = childDirectory };

            IList<INode> listing = new List<INode> { childFile };
            
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(x => x.GetListing(directoryPath)).Returns(listing);
            var controller = new ExplorerController(mockFileSystem.Object);

            // Act
            var result = controller.CreateModel(directoryPath);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Files.Count);
            Assert.AreEqual(fileName, result.Files[0].Name);
            Assert.IsTrue(result.Files[0].IsFile);
            Assert.AreEqual(fileFullPath, result.Files[0].FullPath);
        }
    }
}
