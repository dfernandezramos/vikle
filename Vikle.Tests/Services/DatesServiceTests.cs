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
    /// This class contains the implementations of the unit tests for the dates service
    /// </summary>
    [TestFixture]
    public class DatesServiceTests : MvxIoCSupportingTest
    {
        DatesService _datesService;
        Mock<IRestClient> _restClientMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _datesService = new DatesService();
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
        public async Task DatesService_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Date>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Date>>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var dates = await _datesService.GetUserDates();

            // Then
            Assert.IsNull(dates.Data);
            Assert.IsTrue(dates.Error);
            Assert.AreEqual(Strings.ServerError, dates.Message);
        }
        
        [Test]
        public async Task VehiclesService_APICallUnauthorised_UnauthorisedErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Date>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Date>>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var dates = await _datesService.GetUserDates();

            // Then
            Assert.IsNull(dates.Data);
            Assert.IsTrue(dates.Error);
            Assert.AreEqual(Strings.UserUnauthorised, dates.Message);
        }
        
        [Test]
        public async Task VehiclesService_APICallWorks_VehiclesReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<List<Date>>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<List<Date>>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new List<Date>
                {
                    new Date {
                        IdClient = "1",
                        ReparationDate = DateTime.Today.AddMonths(1).AddDays(1).Ticks,
                        PlateNumber = "5678 DEF",
                        Status = ReparationStatus.Repairing
                    }
                }
            });
            
            // When
            var dates = await _datesService.GetUserDates();

            // Then
            Assert.IsNotNull(dates.Data);
            Assert.IsFalse(dates.Error);
            Assert.IsNotEmpty(dates.Data);
            var date = dates.Data.First();
            Assert.AreEqual("5678DEF", date.PlateNumber);
            Assert.AreEqual("1", date.IdClient);
        }
    }
}