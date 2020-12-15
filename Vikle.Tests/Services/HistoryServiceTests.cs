// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
                        PlateNumber = "1234 ABC",
                        Liquids = true,
                        ITV = true,
                        Date = DateTime.Today.Ticks,
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
            var reparation = reparations.Data.First();
            Assert.AreEqual("1234ABC", reparation.PlateNumber);
        }
    }
}