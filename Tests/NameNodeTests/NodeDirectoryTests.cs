﻿using NameNode.FileSystem;
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
            var nodeDirectory = new Directory();
            var childNode = new File();

            // Act
            nodeDirectory.AddChild(childNode);

            // Assert
            Assert.AreEqual(nodeDirectory, childNode.Parent);
        }

        [Test]
        public void RemoveChild_FromNodeDirectory_HasNullParent()
        {
            // Arrange
            var nodeDirectory = new Directory();
            var childNode = new File();
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
            var nodeDirectory = new Directory();

            // Act
            var result = nodeDirectory.GetChild("A Node");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetChild_FromNodeDirectoryContainingChild_ReturnsChild()
        {
            // Arrange
            var nodeDirectory = new Directory();
            var child = new File() { Name = "A Node" };
            nodeDirectory.AddChild(child);

            // Act
            var result = nodeDirectory.GetChild("A Node");

            // Assert
            Assert.AreEqual(result, child);
        }

        [Test]
        public void IsRoot_WhenParentIsANodeFile_ReturnsFalse()
        {
            // Arrange
            var nodeDirectory = new Directory();
            nodeDirectory.Parent = new File();

            // Act
            var isRoot = nodeDirectory.IsRoot;

            Assert.IsFalse(nodeDirectory.IsRoot);
        }

        [Test]
        public void GetEnumerator_WithoutChildren_EnumeratesNothing()
        {
            // Arrange
            var nodeDirectory = new Directory();

            // Act
            var enumerator = nodeDirectory.GetEnumerator();

            // Assert
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void GetEnumerator_WithChildren_EnumeratesChildren()
        {
            // Arrange
            var nodeDirectory = new Directory();
            nodeDirectory.AddChild(new File() { Name = "Child_1" });
            nodeDirectory.AddChild(new File() { Name = "Child_2" });
            nodeDirectory.AddChild(new File() { Name = "Child_3" });

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