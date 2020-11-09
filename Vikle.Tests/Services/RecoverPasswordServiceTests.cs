using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Tests;
using NUnit.Framework;
using RestSharp;
using Vikle.Core;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.Services;

namespace Vikle.Tests.Services
{
    /// <summary>
    /// This class contains the implementations of the unit tests for the recover password service
    /// </summary>
    [TestFixture]
    public class RecoverPasswordServiceTests : MvxIoCSupportingTest
    {
        RecoverPasswordService _recoverService;
        Mock<IRestClient> _restClientMock;
        
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _recoverService = new RecoverPasswordService();
        }

        [SetUp]
        public void Init() {
            base.Setup();
        }

        [TearDown]
        public void TearDown()
        {
            _restClientMock.Reset();
        }
        
        [Test]
        public async Task RecoverPasswordService_NoValidEmailProvided_InvalidEmailErrorShown()
        {
            // Given
            // When
            Result result = await _recoverService.RecoverPassword("invalidemail");

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EnterValidEmail, result.Message);
        }
        
        [Test]
        public async Task RecoverPasswordService_ValidEmailProvided_NoErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.Is<RestRequest>(
                        r => r.Parameters.Any (p => (string) p.Name == "email")), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed
            });
            
            // When
            Result result = await _recoverService.RecoverPassword("valid@email.com");
            
            // Then
            Assert.IsFalse(result.Error);
        }
        
        [Test]
        public async Task RecoverPasswordService_ServerHasAnError_ServerErrorShown()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.Is<RestRequest>(
                        r => r.Parameters.Any (p => (string) p.Name == "email")), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            Result result = await _recoverService.RecoverPassword("valid@email.com");

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.ServerError, result.Message);
        }
    }
}