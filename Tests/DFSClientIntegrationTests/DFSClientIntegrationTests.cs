using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClientIntegrationTests
{
    [TestFixture]
    class DFSClientIntegrationTests
    {
        [Test]
        public void Main_WithNoArguments_ShowsHelpText()
        {
            // Arrange

            // Act
            DFSClient.Program.Main(new string[] { });

            // Assert
        }
    }
}
