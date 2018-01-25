using DFSClient.CommandBuilders;
using DFSClient.Commands;
using DFSClient.Options;
using NUnit.Framework;
using System.Collections.Generic;

namespace DFSClientTests
{
    [TestFixture]
    class PutCommandBuilderTests
    {
        [Test]
        public void Build_WithPutSubOptionsWithSrcFileAndFilePath_ReturnsPutCommandWithSrcFileAndFilePath()
        {
            // Arrange
            var putSubOptions = new PutSubOptions();
            putSubOptions.PutValues = new List<string> { "TestSrcFile", "TestFilePath" };

            // Act
            var result = new PutCommandBuilder().Build(putSubOptions);

            // Assert
            Assert.IsInstanceOf<PutCommand>(result);
            var putCommand = result as PutCommand;
            Assert.AreEqual("TestSrcFile", putCommand.SrcFile);
            Assert.AreEqual("TestFilePath", putCommand.FilePath);
        }
    }
}
