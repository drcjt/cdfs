using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NameNode;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NameNodeIntegrationTests
{
    [TestFixture]
    class NameNodeClientProtocolIntegrationTests
    {
        [Test]
        public async Task GetListing_WithNoArguments_ReturnsEmptyArray()
        {
            // Arrange
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var testClient = testServer.CreateClient();

            // Act
            var response = await testClient.GetAsync("/ClientProtocol/GetListing");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual("[]", responseString);
        }
    }
}
