using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Tests;
using NUnit.Framework;
using RestSharp;
using Vikle.Core;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.Services;

namespace Vikle.Tests.Services
{
    /// <summary>
    /// This class contains the implementation of the workshop reparation details service tests
    /// </summary>
    [TestFixture]
    public class WSReparationDetailServiceTests : MvxIoCSupportingTest
    {
        WSReparationDetailService _reparationDetailService;
        Mock<IRestClient> _restClientMock;
        private Reparation model;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _reparationDetailService = new WSReparationDetailService();
        }

        [SetUp]
        public void Init() {
            base.Setup();
            model = new Reparation
            {
                Status = ReparationStatus.Pending,
                PlateNumber = "1234 ABC",
                Type = ReparationType.Maintenance,
                Date = DateTime.UtcNow
            };
        }

        [TearDown]
        public void TearDown()
        {
            _restClientMock.Reset();
        }
        
        [Test]
        public async Task UpdateVehicle_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var details = await _reparationDetailService.UpdateReparation(model);

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.ServerError, details.Message);
        }
        
        [Test]
        public async Task UpdateVehicle_UserUnauthorised_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var details = await _reparationDetailService.UpdateReparation(model);

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.UserUnauthorised, details.Message);
        }
        
        [Test]
        public async Task UpdateVehicle_UserAuthorised_NoErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed
            });
            
            // When
            var details = await _reparationDetailService.UpdateReparation(model);

            // Then
            Assert.IsFalse(details.Error);
        }
    }
}