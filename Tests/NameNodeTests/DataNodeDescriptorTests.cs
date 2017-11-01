using NameNode.Service;
using NUnit.Framework;

namespace NameNodeTests
{
    [TestFixture]
    class DataNodeDescriptorTests
    {
        [Test]
        public void CopyConstructor_Always_CreatesDataNodeDescriptorWithSameProperties()
        {
            // Arrange
            var dataNode = new DataNodeDescriptor();
            dataNode.HostName = "testHostName";
            dataNode.IPAddress = "testIPAddress";
            dataNode.LastUpdate = 999;

            // Act
            var dataNodeCopy = new DataNodeDescriptor(dataNode);

            // Assert
            Assert.AreEqual(dataNode.HostName, dataNodeCopy.HostName);
            Assert.AreEqual(dataNode.IPAddress, dataNodeCopy.IPAddress);
            Assert.AreEqual(dataNode.LastUpdate, dataNodeCopy.LastUpdate);
        }
    }
}
