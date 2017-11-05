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
        public void SaveNode_RootWithOneChild_SavesJustNodeDetails()
        {
            // Arrange
            var rootWithSimpleChild = new NodeDirectory { Name = "Root" };
            rootWithSimpleChild.AddChild(new NodeFile { Name = "Test" });

            // Act
            var result = FileSystem.SaveFileImage(rootWithSimpleChild);

            // Assert
            Assert.AreEqual("1,\"Root\",1\r\n0,\"Test\"", result);
        }

        [Test]
        public void SaveNode_RootHasNoChildren_SavesJustNodeDetails()
        {
            // Arrange
            var rootWithNoChildren = new NodeDirectory { Name = "Root" };

            // Act
            var result = FileSystem.SaveFileImage(rootWithNoChildren);

            // Assert
            Assert.AreEqual("1,\"Root\",0", result);
        }

        [Test]
        public void LoadFileImage_ImageHasOnlyRoot_CreatesRootNode()
        {
            // Arrange
            var fileImage = "1,\"Root\",0";

            // Act
            var result = FileSystem.LoadFileImage(fileImage);

            // Assert
            Assert.AreEqual("Root", result.Name);
            Assert.AreEqual(0, result.ChildCount);
        }

        [Test]
        public void LoadFileImage_ImageHasyRootWithChild_CreatesRootAndChildNode()
        {
            // Arrange
            var fileImage = "1,\"Root\",1\r\n0,\"Test\"";

            // Act
            var result = FileSystem.LoadFileImage(fileImage);

            // Assert
            Assert.AreEqual("Root", result.Name);
            Assert.AreEqual(1, result.ChildCount);

            var child = result.First<INode>();
            Assert.AreEqual("Test", child.Name);
        }
    }
}
