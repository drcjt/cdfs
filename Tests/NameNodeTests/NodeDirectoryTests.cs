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
    class NodeDirectoryTests
    {
        [Test]
        public void AddChild_ToEmptyNodeDirectory_UpdatesParentOnAddedChild()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();
            var childNode = new NodeFile();

            // Act
            nodeDirectory.AddChild(childNode);

            // Assert
            Assert.AreEqual(nodeDirectory, childNode.Parent);
        }

        [Test]
        public void RemoveChild_FromNodeDirectory_HasNullParent()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();
            var childNode = new NodeFile();
            nodeDirectory.AddChild(childNode);

            // Act
            nodeDirectory.RemoveChild(childNode);

            // Assert
            Assert.IsNull(childNode.Parent);
        }

        [Test]
        public void GetChild_WithEmptyNodeDirectory_ReturnsNull()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();

            // Act
            var result = nodeDirectory.GetChild("A Node");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetChild_FromNodeDirectoryContainingChild_ReturnsChild()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();
            var child = new NodeFile() { Name = "A Node" };
            nodeDirectory.AddChild(child);

            // Act
            var result = nodeDirectory.GetChild("A Node");

            // Assert
            Assert.AreEqual(result, child);
        }

        [Test]
        public void IsDirectory_Always_ReturnsTrue()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();

            // Act
            var isDirectory = nodeDirectory.IsDirectory;

            // Assert
            Assert.IsTrue(isDirectory);
        }

        [Test]
        public void IsFile_Always_ReturnsFalse()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();

            // Act
            var isFile = nodeDirectory.IsFile;

            // Assert
            Assert.IsFalse(isFile);
        }

        [Test]
        public void IsRoot_WhenParentIsANodeFile_ReturnsFalse()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();
            nodeDirectory.Parent = new NodeFile();

            // Act
            var isRoot = nodeDirectory.IsRoot;

            Assert.IsFalse(nodeDirectory.IsRoot);
        }

        [Test]
        public void GetINodeForFullDirectoryPath_WithEmptyPath_ReturnsCurrentNode()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();

            // Act
            var result = nodeDirectory.GetINodeForPath(String.Empty);

            // Assert
            Assert.AreEqual(nodeDirectory, result);
        }

        [Test]
        public void GetINodeForFullDirectoryPath_WithDirectoryPath_ReturnsChildDirectory()
        {
            // Arrange
            var topLevelDirectory = new NodeDirectory();
            var childLevelDirectory = new NodeDirectory() { Name = "ChildDirectory" };
            topLevelDirectory.AddChild(childLevelDirectory);

            // Act
            var result = topLevelDirectory.GetINodeForPath("ChildDirectory");

            // Assert
            Assert.AreEqual(childLevelDirectory, result);
        }

        [Test]
        public void GetINodeForFullDirectoryPath_WithFilePath_ReturnsCurrentNode()
        {
            // Arrange
            var topLevelDirectory = new NodeDirectory();
            topLevelDirectory.AddChild(new NodeFile() { Name = "ChildFile" });

            // Act
            var result = topLevelDirectory.GetINodeForPath("ChildFile");

            // Assert
            Assert.AreEqual(topLevelDirectory, result);
        }

        [Test]
        public void GetINoodeForFullDirectoryPath_WithMultiLevelDirectoryPath_ReturnsChildDirectory()
        {
            // Arrange
            var topLevelDirectory = new NodeDirectory();
            var firstLevelChildDirectory = new NodeDirectory() { Name = "FirstLevelChildDirectory" };
            var secondLevelChildDirectory = new NodeDirectory() { Name = "SecondLevelChildDirectory" };

            // Act
            var result = topLevelDirectory.GetINodeForPath(@"FirstLevelChildDirectory\SecondLevelChildDirectory");

            // Assert
            Assert.AreEqual(secondLevelChildDirectory, result);
        }

        [Test]
        public void GetEnumerator_WithoutChildren_EnumeratesNothing()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();

            // Act
            var enumerator = nodeDirectory.GetEnumerator();

            // Assert
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void GetEnumerator_WithChildren_EnumeratesChildren()
        {
            // Arrange
            var nodeDirectory = new NodeDirectory();
            nodeDirectory.AddChild(new NodeFile() { Name = "Child_1" });
            nodeDirectory.AddChild(new NodeFile() { Name = "Child_2" });
            nodeDirectory.AddChild(new NodeFile() { Name = "Child_3" });

            // Act
            var enumerator = nodeDirectory.GetEnumerator();

            // Assert
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("Child_1", enumerator.Current.Name);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("Child_2", enumerator.Current.Name);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("Child_3", enumerator.Current.Name);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
