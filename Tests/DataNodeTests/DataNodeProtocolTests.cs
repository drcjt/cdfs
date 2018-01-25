using DataNode.ProtocolWrappers;
using Moq;
using NUnit.Framework;
using Protocols;
using RestSharp;
using System;
using System.Net;

namespace DataNodeTests
{
    [TestFixture]
    class DataNodeProtocolTests
    {
        private Mock<IRestClient> _mockRestClient;
        [SetUp]
        public void Setup()
        {
            _mockRestClient = new Mock<IRestClient>();
        }

        [Test]
        public void RegisterDataNode_Always_PerformsRegisterRequest()
        {
            // Arrange
            var restResponse = new RestResponse<Guid>();
            restResponse.StatusCode = HttpStatusCode.OK;
            restResponse.Data = Guid.NewGuid();

            var dataNodeId = new DataNodeId();

            var restRequest = new RestRequest("/DataNodeProtocol/Register", Method.POST);
            restRequest.AddJsonBody(dataNodeId);
            _mockRestClient.Setup(x => x.Execute<Guid>(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new DataNodeProtocol(_mockRestClient.Object);

            // Act
            var result = sut.RegisterDataNode(dataNodeId);

            // Assert
            Assert.AreEqual(restResponse.Data, result);
            _mockRestClient.Verify(x => x.Execute<Guid>(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
        }

        [Test]
        public void SendHeartbeat_Always_PerformsSendbeatRequest()
        {
            // Arrange
            var restResponse = new RestResponse();
            restResponse.StatusCode = HttpStatusCode.OK;

            var dataNodeId = Guid.NewGuid();

            var restRequest = new RestRequest("/DataNodeProtocol/SendHeartbeat", Method.POST);
            restRequest.AddJsonBody(dataNodeId);
            _mockRestClient.Setup(x => x.Execute(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new DataNodeProtocol(_mockRestClient.Object);

            // Act
            sut.SendHeartbeat(dataNodeId);

            // Assert
            _mockRestClient.Verify(x => x.Execute(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
        }

        private bool VerifyRestRequest(RestRequest expected, RestRequest actual)
        {
            bool result = true;
            result &= expected.Method == actual.Method;
            result &= expected.Resource == actual.Resource;
            result &= expected.Parameters.Count == actual.Parameters.Count;
            for (var parameterIndex = 0; parameterIndex < expected.Parameters.Count; parameterIndex++)
            {
                result &= expected.Parameters[parameterIndex].Name == actual.Parameters[parameterIndex].Name;
                result &= expected.Parameters[parameterIndex].Value as string == actual.Parameters[parameterIndex].Value as string;
            }

            return result;
        }
    }
}