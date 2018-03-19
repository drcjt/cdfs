using Moq;
using NameNode.Controllers;
using NameNode.Models.Builders;
using NameNode.Services.Interfaces;
using NameNode.Status;
using NUnit.Framework;
using System;

namespace NameNodeTests
{
    [TestFixture]
    class NameNodeStatusModelBuilderTests
    {
        [Test]
        public void CreateModel_Always_ReturnsModel()
        {
            DateTime startedDateTime = DateTime.Now;
            const int deadNodes = 5;
            const int liveNodes = 10;

            // Arrange
            var stubNameNodeStatus = new Mock<INameNodeStatus>();
            stubNameNodeStatus.Setup(x => x.Started).Returns(startedDateTime);
            var stubDataNodesStatus = new Mock<IDataNodesStatus>();
            stubDataNodesStatus.Setup(x => x.DeadNodes).Returns(deadNodes);
            stubDataNodesStatus.Setup(x => x.LiveNodes).Returns(liveNodes);

            // Act
            var result = NameNode.Models.Builders.NameNodeStatusModelBuilder.CreateModel(stubNameNodeStatus.Object, stubDataNodesStatus.Object);

            // Assert
            Assert.AreEqual(startedDateTime, result.Started);
            Assert.AreEqual(deadNodes, result.DeadNodes);
            Assert.AreEqual(liveNodes, result.LiveNodes);
        }
    }
}
