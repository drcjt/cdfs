using DFSClient.CommandBuilders;
using DFSClient.Commands;
using DFSClient.Options;
using NUnit.Framework;
using System.Collections.Generic;

namespace DFSClientTests
{
    [TestFixture]
    class DeleteCommandBuilderTests
    {
        [Test]
        public void Build_WithDeleteSubOptionsWithFilePath_ReturnsDeleteCommandWithFilePath()
        {
            // Arrange
            var deleteSubOptions = new DeleteSubOptions();
            deleteSubOptions.FilePath = new List<string> { "TestFilePath" };

            // Act
            var result = new DeleteCommandBuilder().Build(deleteSubOptions);

            // Assert
            Assert.IsInstanceOf<DeleteCommand>(result);
            var deleteCommand = result as DeleteCommand;
            Assert.AreEqual("TestFilePath", deleteCommand.FilePath);
        }
    }
}
