using DFSClient.Core;
using NUnit.Framework;

namespace DFSClientTests.Core
{
    [TestFixture]
    public class DFSClientTests
    {
        [Test]
        public void SimpleTest()
        {
            // Just cause the DFSClient assembly to get loaded so that opencover can pick up lack of coverage
            // TODO Write some useful tests here!
            var dfsclient = new Program();
        }
    }
}
