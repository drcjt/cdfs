using DataNode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DataNodeIntegrationTests
{
    [TestFixture]
    class DataNodeIntegrationTests
    {
        [Test]
        public async Task GetIndex_Always_ReturnsEmptyString()
        {
            // Arrange
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var testClient = testServer.CreateClient();

            // Act
            var response = await testClient.GetAsync("/Index");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual("", responseString);
        }
    }
}
