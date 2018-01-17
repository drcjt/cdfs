using Moq;
using NameNode.Controllers;
using NameNode.Services.Interfaces;
using NameNode.Status;
using NUnit.Framework;
using System;

namespace NameNodeTests
{
    [TestFixture]
    class CdfsHealthControllerTests
    {
        [Test]
        public void CreateModel_Always_ReturnsModel()
        {
            DateTime startedDateTime = DateTime.Now;
            const int deadNodes = 5;
            const int liveNodes = 10;

            // Arrange
            var mockNameNodeStatus = new Mock<INameNodeStatus>();
            mockNameNodeStatus.Setup(x => x.Started).Returns(startedDateTime);
            var mockDataNodesStatus = new Mock<IDataNodesStatus>();
            mockDataNodesStatus.Setup(x => x.DeadNodes).Returns(deadNodes);
            mockDataNodesStatus.Setup(x => x.LiveNodes).Returns(liveNodes);
            var controller = new CdfsHealthController(mockNameNodeStatus.Object, mockDataNodesStatus.Object);

            // Act
            var result = controller.CreateModel();

            // Assert
            Assert.AreEqual(startedDateTime, result.Started);
            Assert.AreEqual(deadNodes, result.DeadNodes);
            Assert.AreEqual(liveNodes, result.LiveNodes);
        }
    }
}
