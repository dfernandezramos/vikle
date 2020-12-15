// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
    /// This class contains the implementation of the date details service tests
    /// </summary>
    [TestFixture]
    public class DateDetailServiceTests : MvxIoCSupportingTest
    {
        DateDetailsService _dateDetailService;
        Mock<IRestClient> _restClientMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _dateDetailService = new DateDetailsService();
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
        public async Task SaveDate_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var details = await _dateDetailService.SaveDate(new Date());

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.ServerError, details.Message);
        }
        
        [Test]
        public async Task SaveDate_UserUnauthorised_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var details = await _dateDetailService.SaveDate(new Date());

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.UserUnauthorised, details.Message);
        }
        
        [Test]
        public async Task SaveDate_VehicleAlreadyWithDate_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest
            });
            
            // When
            var details = await _dateDetailService.SaveDate(new Date());

            // Then
            Assert.IsTrue(details.Error);
            Assert.AreEqual(Strings.VehicleAlreadyWithDate, details.Message);
        }
        
        [Test]
        public async Task SaveDate_UserAuthorised_NoErrorReturned()
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
            var details = await _dateDetailService.SaveDate(new Date());

            // Then
            Assert.IsFalse(details.Error);
        }
    }
}