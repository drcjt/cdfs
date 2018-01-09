using NameNode.Core.FileSystem;
using NameNode.Core.FileSystem.Interfaces;
using NUnit.Framework;
using System.Linq;

namespace NameNodeTests.Core
{
    [TestFixture]
    class FileSystemSerializerTests
    {
        [Test]
        public void SaveNode_RootWithOneChild_SavesRootAndChildNodeDetails()
        {
            // Arrange
            var rootWithSimpleChild = new Directory { Name = "Root" };
            rootWithSimpleChild.AddChild(new File { Name = "Test" });
            var fileSystemSerializer = new FileSystemSerializer();

            // Act
            var result = fileSystemSerializer.Serialize(rootWithSimpleChild);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("1,\"Root\",1", result.ElementAt(0));
            Assert.AreEqual("0,\"Test\"", result.ElementAt(1));
        }

        [Test]
        public void SaveNode_RootHasNoChildren_SavesRootNodeDetails()
        {
            // Arrange
            var rootWithNoChildren = new Directory { Name = "Root" };
            var fileSystemSerializer = new FileSystemSerializer();

            // Act
            var result = fileSystemSerializer.Serialize(rootWithNoChildren);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("1,\"Root\",0", result.ElementAt(0));
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
        public void LoadFileImage_ImageHasRootWithChild_CreatesRootAndChildNode()
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
