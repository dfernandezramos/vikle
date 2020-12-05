using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This class contains the implementation of the workshop reparations service tests
    /// </summary>
    [TestFixture]
    public class WSReparationsServiceTests: MvxIoCSupportingTest
    {
        WSReparationsService _reparationsService;
        Mock<IRestClient> _restClientMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _reparationsService = new WSReparationsService();
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
        public async Task GetUserWorkshopId_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<User>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var reparations = await _reparationsService.GetUserWorkshopId();

            // Then
            Assert.IsNull(reparations.Data);
            Assert.IsTrue(reparations.Error);
            Assert.AreEqual(Strings.ServerError, reparations.Message);
        }
        
        [Test]
        public async Task GetUserWorkshopId_APICallUnauthorised_UnauthorisedErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<User>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var reparations = await _reparationsService.GetUserWorkshopId();

            // Then
            Assert.IsNull(reparations.Data);
            Assert.IsTrue(reparations.Error);
            Assert.AreEqual(Strings.UserUnauthorised, reparations.Message);
        }
        
        [Test]
        public async Task GetUserWorkshopId_APICallWorks_WorkshopIdReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<User>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new User
                {
                    IdWorkshop = "1234"
                }
            });
            
            // When
            var reparations = await _reparationsService.GetUserWorkshopId();

            // Then
            Assert.IsNotNull(reparations.Data);
            Assert.IsFalse(reparations.Error);
            Assert.IsNotEmpty(reparations.Data);
            Assert.AreEqual("1234", reparations.Data);
        }
        
        [Test]
        public async Task GetReparations_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Reparation>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Reparation>>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var reparations = await _reparationsService.GetReparations("1234");

            // Then
            Assert.IsNull(reparations.Data);
            Assert.IsTrue(reparations.Error);
            Assert.AreEqual(Strings.ServerError, reparations.Message);
        }
        
        [Test]
        public async Task GetReparations_APICallUnauthorised_UnauthorisedErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Reparation>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Reparation>>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var reparations = await _reparationsService.GetReparations("1234");

            // Then
            Assert.IsNull(reparations.Data);
            Assert.IsTrue(reparations.Error);
            Assert.AreEqual(Strings.UserUnauthorised, reparations.Message);
        }
        
        [Test]
        public async Task GetReparations_APICallWorks_VehiclesReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Reparation>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Reparation>>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new List<Reparation> {
                    new Reparation {
                        PlateNumber = "1234 ABC",
                        Liquids = true,
                        ITV = true,
                        Date = DateTime.Today,
                        Type = ReparationType.Reparation
                    }
                }
            });
            
            // When
            var reparations = await _reparationsService.GetReparations("1234");

            // Then
            Assert.IsNotNull(reparations.Data);
            Assert.IsFalse(reparations.Error);
            Assert.IsNotEmpty(reparations.Data);
            var car = reparations.Data.First();
            Assert.AreEqual("1234ABC", car.PlateNumber);
        }
    }
}