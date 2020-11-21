using System;
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
using Xamarin.Essentials;

namespace Vikle.Tests.Services
{
    public class ContactCustomerServiceTests : MvxIoCSupportingTest
    {
        ContactCustomerService _contactCustomerService;
        Mock<IRestClient> _restClientMock;
        Mock<IXamarinEssentialsService> _xamarinEssentialsMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            Ioc.RegisterSingleton<ISecureStorageService> (new DummySecureStorageService());
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            _xamarinEssentialsMock = new Mock<IXamarinEssentialsService> ();
            Ioc.RegisterSingleton<IXamarinEssentialsService> (_xamarinEssentialsMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _contactCustomerService = new ContactCustomerService();
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
        public async Task CallCustomer_WrongNumber_ErrorReturned()
        {
            // Given
            _xamarinEssentialsMock.Setup(xe => xe.MakePhoneCall(It.IsAny<string>()))
                .Throws(new ArgumentNullException());
            
            // When
            var result = await _contactCustomerService.CallCustomer(null);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.WrongNumber, result.Message);
        }
        
        [Test]
        public async Task CallCustomer_CallsNotSupported_ErrorReturned()
        {
            // Given
            _xamarinEssentialsMock.Setup(xe => xe.MakePhoneCall(It.IsAny<string>()))
                .Throws(new FeatureNotSupportedException());
            
            // When
            var result = await _contactCustomerService.CallCustomer("123456789");
        
            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.CallsNotSupported, result.Message);
        }
        
        [Test]
        public async Task CallCustomer_CallFails_ErrorReturned()
        {
            // Given
            _xamarinEssentialsMock.Setup(xe => xe.MakePhoneCall(It.IsAny<string>()))
                .Throws(new Exception());
            
            // When
            var result = await _contactCustomerService.CallCustomer("123456789");
        
            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.CouldNotCall, result.Message);
        }
        
        [Test]
        public async Task CallCustomer_CallSuccess_NoErrorReturned()
        {
            // Given
            // When
            var result = await _contactCustomerService.CallCustomer("123456789");
        
            // Then
            Assert.IsFalse(result.Error);
        }
        
        [Test]
        public async Task SendEmail_EmailsNotSupported_ErrorReturned()
        {
            // Given
            _xamarinEssentialsMock.Setup(xe => xe.SendEmail(It.IsAny<EmailMessage>()))
                .Throws(new FeatureNotSupportedException());
            
            // When
            var result = await _contactCustomerService.SendEmail("user@email.com");
        
            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EmailsNotSupported, result.Message);
        }
        
        [Test]
        public async Task SendEmail_EmailFails_ErrorReturned()
        {
            // Given
            _xamarinEssentialsMock.Setup(xe => xe.SendEmail(It.IsAny<EmailMessage>()))
                .Throws(new Exception());
            
            // When
            var result = await _contactCustomerService.SendEmail("user@email.com");
        
            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.CouldNotSendEmail, result.Message);
        }
        
        [Test]
        public async Task SendEmail_EmailSuccess_NoErrorReturned()
        {
            // Given
            // When
            var result = await _contactCustomerService.SendEmail("user@email.com");
        
            // Then
            Assert.IsFalse(result.Error);
        }
        
        [Test]
        public async Task GetVehicleOwner_APICallFails_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<User>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            var result = await _contactCustomerService.GetVehicleOwner("1234 ABC");

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.ServerError, result.Message);
        }
        
        [Test]
        public async Task GetVehicleOwner_UserUnauthorised_ErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<User>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            var result = await _contactCustomerService.GetVehicleOwner("1234 ABC");

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.UserUnauthorised, result.Message);
        }

        [Test]
        public async Task SaveDate_UserAuthorised_NoErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<User>(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed
            });
            
            // When
            var result = await _contactCustomerService.GetVehicleOwner("1234 ABC");

            // Then
            Assert.IsFalse(result.Error);
        }
    }
}