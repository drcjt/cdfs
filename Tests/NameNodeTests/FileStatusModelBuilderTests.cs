using Moq;
using NameNode.Controllers;
using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
using NameNode.Models;
using NameNode.Models.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace NameNodeTests
{
    [TestFixture]
    class FileStatusModelBuilderTests
    {
        [Test]
        public void CreateModel_ValidDirectoryPath_ReturnsStatusModel()
        {
            // Arrange
            const string directoryPath = "directoryPath";
            const string directoryName = "directory";
            const string subDirectoryName = "subdirectory";
            const string fileName = "file";
            string fileFullPath = System.IO.Path.Combine(directoryName, subDirectoryName, fileName);
            var rootNode = new Directory { Name = directoryName };
            var childDirectory = new Directory { Name = subDirectoryName, Parent = rootNode };
            var childFile = new File { Name = fileName, Parent = childDirectory };

            IList<INode> files = new List<INode> { childFile };            

            // Act
            var result = FileStatusModelBuilder.CreateModel(directoryPath, files);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Files.Count);
            Assert.AreEqual(fileName, result.Files[0].Name);
            Assert.IsTrue(result.Files[0].IsFile);
            Assert.AreEqual(fileFullPath, result.Files[0].FullPath);
        }
    }
}
