using DFSClient.CommandBuilders;
using DFSClient.Commands;
using DFSClient.Options;
using NUnit.Framework;
using System.Collections.Generic;

namespace DFSClientTests
{
    [TestFixture]
    class ListingCommandBuilderTests
    {
        [Test]
        public void Build_WithListingSubOptionsWithNoFilePath_ReturnsListingCommandWithEmptyFilePath()
        {
            // Arrange
            var listingSubOptions = new ListingSubOptions();
            listingSubOptions.FilePath = new List<string>();

            // Act
            var result = new ListingCommandBuilder().Build(listingSubOptions);

            // Assert
            Assert.IsInstanceOf<ListingCommand>(result);
            var listingCommand = result as ListingCommand;
            Assert.AreEqual("", listingCommand.FilePath);
        }

        [Test]
        public void Build_WithListingSubOptionsWithFilePath_ReturnsListingCommandWithFilePath()
        {
            // Arrange
            var listingSubOptions = new ListingSubOptions();
            listingSubOptions.FilePath = new List<string> { "TestFilePath" };

            // Act
            var result = new ListingCommandBuilder().Build(listingSubOptions);

            // Assert
            Assert.IsInstanceOf<ListingCommand>(result);
            var listingCommand = result as ListingCommand;
            Assert.AreEqual("TestFilePath", listingCommand.FilePath);
        }
    }
}
