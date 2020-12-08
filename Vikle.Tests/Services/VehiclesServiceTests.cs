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
    /// This class contains the implementations of the unit tests for the vehicles service
    /// </summary>
    [TestFixture]
    public class VehiclesServiceTests : MvxIoCSupportingTest
    {
        VehiclesService _vehiclesService;
        Mock<IRestClient> _restClientMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _vehiclesService = new VehiclesService();
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
        public async Task VehiclesService_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Vehicle>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Vehicle>>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var vehicles = await _vehiclesService.GetUserVehicles();

            // Then
            Assert.IsNull(vehicles.Data);
            Assert.IsTrue(vehicles.Error);
            Assert.AreEqual(Strings.ServerError, vehicles.Message);
        }
        
        [Test]
        public async Task VehiclesService_APICallUnauthorised_UnauthorisedErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Vehicle>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Vehicle>>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var vehicles = await _vehiclesService.GetUserVehicles();

            // Then
            Assert.IsNull(vehicles.Data);
            Assert.IsTrue(vehicles.Error);
            Assert.AreEqual(Strings.UserUnauthorised, vehicles.Message);
        }
        
        [Test]
        public async Task VehiclesService_APICallWorks_VehiclesReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Vehicle>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Vehicle>>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new List<Vehicle>
                {
                    new Vehicle {
                        IdClient = "2",
                        IdDrivers = new List<string> (),
                        LastITV = DateTime.Today.Ticks,
                        LastTBDS = DateTime.Today.Ticks,
                        Model = "Audi A3",
                        PlateNumber = "5678 DEF",
                        VehicleType = VehicleType.Car,
                        Year = 2007
                    }
                }
            });
            
            // When
            var vehicles = await _vehiclesService.GetUserVehicles();

            // Then
            Assert.IsNotNull(vehicles.Data);
            Assert.IsFalse(vehicles.Error);
            Assert.IsNotEmpty(vehicles.Data);
            var car = vehicles.Data.First();
            Assert.AreEqual("Audi A3", car.Model);
            Assert.AreEqual("2", car.IdClient);
        }
    }
}