using DFSClient.CommandBuilders;
using DFSClient.Commands;
using DFSClient.Options;
using NUnit.Framework;
using System.Collections.Generic;

namespace DFSClientTests
{
    [TestFixture]
    class MkdirCommandBuilderTests
    {
        [Test]
        public void Build_WithMkdirSubOptionsWithDirectoryPath_ReturnsMkdirCommandWithDirectoryPath()
        {
            // Arrange
            var mkdirSubOptions = new MkdirSubOptions();
            mkdirSubOptions.DirectoryPath = new List<string> { "TestDirectoryPath" };

            // Act
            var result = new MkdirCommandBuilder().Build(mkdirSubOptions);

            // Assert
            Assert.IsInstanceOf<MkdirCommand>(result);
            var mkdirCommand = result as MkdirCommand;
            Assert.AreEqual("TestDirectoryPath", mkdirCommand.DirectoryPath);
        }
    }
}
