using NUnit.Framework;
using Protocols;

namespace ProtocolsTests
{
    [TestFixture]
    class CdfsFileStatusTests
    {
        [Test]
        public void ToString_WithDirectory_ReturnsDirectoryDetails()
        {
            // Arrange
            var cdfsFileStatus = new CdfsFileStatus() { IsDirectory = true, FilePath = "TestFilePath", Length = 123 };

            // Act
            var result = cdfsFileStatus.ToString();

            // Assert
            Assert.AreEqual("<DIR>          TestFilePath", result);
        }

        [Test]
        public void ToString_WithFile_ReturnsFileDetails()
        {
            // Arrange
            var cdfsFileStatus = new CdfsFileStatus() { IsDirectory = false, FilePath = "TestFilePath", Length = 123 };

            // Act
            var result = cdfsFileStatus.ToString();

            // Assert
            Assert.AreEqual("           123 TestFilePath", result);
        }
    }
}
