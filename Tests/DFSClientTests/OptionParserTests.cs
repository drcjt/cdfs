using DFSClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClientTests
{
    [TestFixture]
    class OptionParserTests
    {
        [Test]
        public void ParseOptions_NoArgs_ReturnsNull()
        {
            // Arrange
            var sut = new OptionParser();

            // Act
            var result = sut.ParseOptions(new string[] { });

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ParseOptions_WithLsArg_ReturnsListingSubOptions()
        {
            // Arrange
            var sut = new OptionParser();

            // Act
            var result = sut.ParseOptions(new string[] { "ls", "\\mydir" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ListingSubOptions>(result);
            var listingSubOptions = result as ListingSubOptions;
            Assert.AreEqual("\\mydir", listingSubOptions.FilePath[0]);
        }

        [Test]
        public void ParseOptions_WithPutArg_ReturnsPutSubOptions()
        {
            // Arrange
            var sut = new OptionParser();

            // Act
            var result = sut.ParseOptions(new string[] { "put", "test.txt", "\\mydir" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PutSubOptions>(result);
            var putSubOptions = result as PutSubOptions;
            Assert.AreEqual("test.txt", putSubOptions.PutValues[0]);
            Assert.AreEqual("\\mydir", putSubOptions.PutValues[1]);
        }

        [Test]
        public void ParseOptions_WithDeleteArg_ReturnsDeleteSubOptions()
        {
            // Arrange
            var sut = new OptionParser();

            // Act
            var result = sut.ParseOptions(new string[] { "rm", "test.txt" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DeleteSubOptions>(result);
            var deleteSubOptions = result as DeleteSubOptions;
            Assert.AreEqual("test.txt", deleteSubOptions.FilePath[0]);
        }

        [Test]
        public void ParseOptions_WithMkdirArg_ReturnsMkdirSubOptions()
        {
            // Arrange
            var sut = new OptionParser();

            // Act
            var result = sut.ParseOptions(new string[] { "mkdir", "\\mydir" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MkdirSubOptions>(result);
            var mkdirSubOptions = result as MkdirSubOptions;
            Assert.AreEqual("\\mydir", mkdirSubOptions.DirectoryPath[0]);
        }
    }
}
