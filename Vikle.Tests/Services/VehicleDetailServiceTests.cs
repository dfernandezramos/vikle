using System;
using System.Collections.Generic;
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
    /// This class contains the implementation of the vehicle details service tests
    /// </summary>
    [TestFixture]
    public class VehicleDetailServiceTests : MvxIoCSupportingTest
    {
        VehicleDetailService _vehicleDetailService;
        Mock<IRestClient> _restClientMock;
        private Vehicle model;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _vehicleDetailService = new VehicleDetailService();
        }

        [SetUp]
        public void Init() {
            base.Setup();
            model = new Vehicle
            {
                IdClient = "1",
                IdDrivers = new List<string>(),
                LastITV = DateTime.Today.Ticks,
                LastTBDS = DateTime.Today.Ticks,
                Model = "Vespino",
                PlateNumber = "1234 ABC",
                VehicleType = VehicleType.MotorCycle,
                Year = 2015
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
            var details = await _vehicleDetailService.UpdateVehicle(model.PlateNumber, model);

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
            var details = await _vehicleDetailService.UpdateVehicle(model.PlateNumber, model);

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
            var details = await _vehicleDetailService.UpdateVehicle(model.PlateNumber, model);

            // Then
            Assert.IsFalse(details.Error);
        }
        
        [Test]
        public async Task DeleteVehicle_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var details = await _vehicleDetailService.DeleteVehicle(model.PlateNumber);

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.ServerError, details.Message);
        }
        
        [Test]
        public async Task DeleteVehicle_UserUnauthorised_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var details = await _vehicleDetailService.DeleteVehicle(model.PlateNumber);

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.UserUnauthorised, details.Message);
        }
        
        [Test]
        public async Task DeleteVehicle_UserAuthorised_NoErrorReturned()
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
            var details = await _vehicleDetailService.DeleteVehicle(model.PlateNumber);

            // Then
            Assert.IsFalse(details.Error);
        }
        
        [Test]
        public async Task GetReparationStatus_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<Reparation>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<Reparation>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var details = await _vehicleDetailService.GetReparationStatus(model.PlateNumber);

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.ServerError, details.Message);
        }
        
        [Test]
        public async Task GetReparationStatus_NoContentError_NoErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<Reparation>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<Reparation>
            {
                StatusCode = HttpStatusCode.NoContent
            });
            
            // When
            var details = await _vehicleDetailService.GetReparationStatus(model.PlateNumber);

            // Then
            Assert.IsFalse(details.Error);
        }
        
        [Test]
        public async Task GetReparationStatus_UserUnauthorised_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<Reparation>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<Reparation>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var details = await _vehicleDetailService.GetReparationStatus(model.PlateNumber);

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.UserUnauthorised, details.Message);
        }
        
        [Test]
        public async Task GetReparationStatus_UserAuthorised_NoErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<Reparation>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<Reparation>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new Reparation {Status = ReparationStatus.Repaired}
            });
            
            // When
            var details = await _vehicleDetailService.GetReparationStatus(model.PlateNumber);

            // Then
            Assert.IsFalse(details.Error);
            Assert.AreEqual(details.Data, ReparationStatus.Repaired);
        }
    }
}