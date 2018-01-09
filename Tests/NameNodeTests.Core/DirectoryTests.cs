using NameNode.Core.FileSystem;
using NUnit.Framework;

namespace NameNodeTests.Core
{
    [TestFixture]
    class DirectoryTests
    {
        [Test]
        public void AddChild_EmptyNodeDirectory_UpdatesParentOnAddedChild()
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
        public void RemoveChild_ChildExists_RemovedNodeHasNullParent()
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
        public void GetChild_EmptyNodeDirectory_ReturnsNull()
        {
            // Arrange
            var nodeDirectory = new Directory();

            // Act
            var result = nodeDirectory.GetChild("A Node");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetChild_ChildExists_ReturnsChild()
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
