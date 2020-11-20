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
    /// This class contains the implementation of the reparations history service tests
    /// </summary>
    [TestFixture]
    public class HistoryServiceTests : MvxIoCSupportingTest
    {
        HistoryService _historyService;
        Mock<IRestClient> _restClientMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _historyService = new HistoryService();
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
        public async Task HistoryService_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Reparation>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Reparation>>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var reparations = await _historyService.GetReparations("");

            // Then
            Assert.IsNull(reparations.Data);
            Assert.IsTrue(reparations.Error);
            Assert.AreEqual(Strings.ServerError, reparations.Message);
        }
        
        [Test]
        public async Task VehiclesService_APICallUnauthorised_UnauthorisedErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Reparation>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Reparation>>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var reparations = await _historyService.GetReparations("");

            // Then
            Assert.IsNull(reparations.Data);
            Assert.IsTrue(reparations.Error);
            Assert.AreEqual(Strings.UserUnauthorised, reparations.Message);
        }
        
        [Test]
        public async Task VehiclesService_APICallWorks_VehiclesReturned()
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
                        Id = "2",
                        PlateNumber = "1234 ABC",
                        Liquids = true,
                        ITV = true,
                        Date = DateTime.Today,
                        Type = ReparationType.Reparation
                    }
                }
            });
            
            // When
            var reparations = await _historyService.GetReparations("1234 ABC");

            // Then
            Assert.IsNotNull(reparations.Data);
            Assert.IsFalse(reparations.Error);
            Assert.IsNotEmpty(reparations.Data);
            var car = reparations.Data.First();
            Assert.AreEqual("1234 ABC", car.PlateNumber);
            Assert.AreEqual("2", car.Id);
        }
    }
}