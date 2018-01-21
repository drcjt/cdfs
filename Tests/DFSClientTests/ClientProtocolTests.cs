using DFSClient;
using Moq;
using NUnit.Framework;
using Protocols;
using RestSharp;
using SemanticComparison.Fluent;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DFSClientTests
{
    [TestFixture]
    class ClientProtocolTests
    {
        private const string srcFile = "srcFile";
        private const string filePath = "filePath";
        private const string directoryPath = "directoryPath";
        private const string testSrcFile = "TestSrcFile";
        private const string testFilePath = "TestFilePath";
        private const string testDirectoryPath = "TestDirectoryPath";

        private Mock<IRestClient> _mockRestClient;
        [SetUp]
        public void Setup()
        {
            _mockRestClient = new Mock<IRestClient>();
        }

        [Test]
        public void Create_Always_PerformsCreateRequest()
        {
            // Arrange
            var restResponse = new RestResponse<object>();
            restResponse.StatusCode = HttpStatusCode.OK;

            var restRequest = new RestRequest("/ClientProtocol/Create", Method.POST);
            restRequest.AddParameter(srcFile, testSrcFile);
            restRequest.AddParameter(filePath, testFilePath);
            _mockRestClient.Setup(x => x.Execute<object>(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new ClientProtocol(_mockRestClient.Object);

            // Act
            sut.Create(testSrcFile, testFilePath);

            // Assert
            _mockRestClient.Verify(x => x.Execute<object>(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
        }

        [Test]
        public void AddBlock_Always_PerformsAddBlockRequestAndReturnsLocatedBlock()
        {
            // Arrange
            var restResponse = new RestResponse<LocatedBlock>();
            restResponse.StatusCode = HttpStatusCode.OK;
            restResponse.Data = new LocatedBlock();

            var restRequest = new RestRequest("/ClientProtocol/AddBlock", Method.POST);
            restRequest.AddParameter(srcFile, testSrcFile);
            _mockRestClient.Setup(x => x.Execute<LocatedBlock>(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new ClientProtocol(_mockRestClient.Object);

            // Act
            var result = sut.AddBlock(testSrcFile);

            // Assert
            _mockRestClient.Verify(x => x.Execute<LocatedBlock>(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
            Assert.AreEqual(result, restResponse.Data);
        }

        [Test]
        public void AddDelete_Always_PerformsDeleteRequest()
        {
            // Arrange
            var restResponse = new RestResponse<object>();
            restResponse.StatusCode = HttpStatusCode.OK;

            var restRequest = new RestRequest("/ClientProtocol/Delete", Method.DELETE);
            restRequest.AddParameter(filePath, testFilePath);
            _mockRestClient.Setup(x => x.Execute<object>(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new ClientProtocol(_mockRestClient.Object);

            // Act
            sut.Delete(testFilePath);

            // Assert
            _mockRestClient.Verify(x => x.Execute<object>(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
        }

        [Test]
        public void Mkdir_Always_PerformsMkdirRequest()
        {
            // Arrange
            var restResponse = new RestResponse<object>();
            restResponse.StatusCode = HttpStatusCode.OK;

            var restRequest = new RestRequest("/ClientProtocol/Mkdir", Method.POST);
            restRequest.AddParameter(directoryPath, testDirectoryPath);
            _mockRestClient.Setup(x => x.Execute<object>(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new ClientProtocol(_mockRestClient.Object);

            // Act
            sut.Mkdir(testDirectoryPath);

            // Assert
            _mockRestClient.Verify(x => x.Execute<object>(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
        }

        [Test]
        public void GetListing_Always_PerformsGetListingRequestAndReturnsListOfCdfsFileStatus()
        {
            // Arrange
            var restResponse = new RestResponse<List<CdfsFileStatus>>();
            restResponse.StatusCode = HttpStatusCode.OK;
            restResponse.Data = new List<CdfsFileStatus> {  };

            var restRequest = new RestRequest("/ClientProtocol/GetListing", Method.GET);
            restRequest.AddParameter(filePath, testFilePath);
            _mockRestClient.Setup(x => x.Execute<List<CdfsFileStatus>>(It.IsAny<RestRequest>())).Returns(restResponse);
            var sut = new ClientProtocol(_mockRestClient.Object);

            // Act
            var result = sut.GetListing(testFilePath);

            // Assert
            _mockRestClient.Verify(x => x.Execute<List<CdfsFileStatus>>(It.Is<RestRequest>(r => VerifyRestRequest(restRequest, r))));
            Assert.AreEqual(result, restResponse.Data);
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
