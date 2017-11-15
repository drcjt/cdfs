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
    class FileSystemSerializerTests
    {
        [Test]
        public void SaveNode_RootWithOneChild_SavesJustNodeDetails()
        {
            // Arrange
            var rootWithSimpleChild = new Directory { Name = "Root" };
            rootWithSimpleChild.AddChild(new File { Name = "Test" });
            var fileSystemSerializer = new FileSystemSerializer();

            // Act
            var result = fileSystemSerializer.Serialize(rootWithSimpleChild);

            // Assert
            Assert.AreEqual("1,\"Root\",1\r\n0,\"Test\"", result);
        }

        [Test]
        public void SaveNode_RootHasNoChildren_SavesJustNodeDetails()
        {
            // Arrange
            var rootWithNoChildren = new Directory { Name = "Root" };
            var fileSystemSerializer = new FileSystemSerializer();

            // Act
            var result = fileSystemSerializer.Serialize(rootWithNoChildren);

            // Assert
            Assert.AreEqual("1,\"Root\",0", result);
        }

        [Test]
        public void LoadFileImage_ImageHasOnlyRoot_CreatesRootNode()
        {
            // Arrange
            var fileImageLines = new string[] { "1,\"Root\",0" };
            var fileSystemSerializer = new FileSystemSerializer();

            // Act
            var result = fileSystemSerializer.Deserialize(fileImageLines);

            // Assert
            Assert.AreEqual("Root", result.Name);
            Assert.AreEqual(0, result.ChildCount);
        }

        [Test]
        public void LoadFileImage_ImageHasyRootWithChild_CreatesRootAndChildNode()
        {
            // Arrange
            var fileImageLines = new string[] { "1,\"Root\",1", "0,\"Test\"" };
            var fileSystemSerializer = new FileSystemSerializer();

            // Act
            var result = fileSystemSerializer.Deserialize(fileImageLines);

            // Assert
            Assert.AreEqual("Root", result.Name);
            Assert.AreEqual(1, result.ChildCount);

            var child = result.First<INode>();
            Assert.AreEqual("Test", child.Name);
        }
    }
}
