// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
    /// This class contains the implementations of the unit tests for the login service
    /// </summary>
    [TestFixture]
    public class LoginServiceTests : MvxIoCSupportingTest
    {
        const string CLIENT_EMAIL = "client@email.com";
        const string WORKER_EMAIL = "worker@email.com";
        const string ERROR_EMAIL = "error@email.com";
        const string CLIENT_PASSWORD = "clientpassword";
        const string WORKER_PASSWORD = "workerpassword";
        const string WRONG_PASSWORD = "wrongpassword";
        const string WORKER_ID = "1";
        const string CLIENT_ID = "2";
        const string WORKER_TOKEN = "WorkerToken";
        const string CLIENT_TOKEN = "UserToken";
        
        LoginService _loginService;
        DummySecureStorageService _secureStorageService;
        Mock<IRestClient> _restClientMock;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            _secureStorageService = new DummySecureStorageService();
            Ioc.RegisterSingleton<ISecureStorageService> (_secureStorageService);
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _loginService = new LoginService();
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
        public async Task LoginService_NoEmailProvided_DataRequiredErrorShown()
        {
            // Given
            // When
            LoginResult result = await _loginService.Login(null, CLIENT_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EmailPasswordRequired, result.Message);
        }
        
        [Test]
        public async Task LoginService_NoPasswordProvided_DataRequiredErrorShown()
        {
            // Given
            // When
            LoginResult result = await _loginService.Login(CLIENT_EMAIL, null);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EmailPasswordRequired, result.Message);
        }
        
        [Test]
        public async Task LoginService_InvalidEmailProvided_WrongEmailErrorShown()
        {
            // Given
            // When
            LoginResult result = await _loginService.Login("invalid", CLIENT_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EnterValidEmail, result.Message);
        }
        
        [Test]
        public async Task LoginService_CorrectWorkerCredentials_WorkerUserReturned()
        {
            // Given
            SetupCorrectLoginMocks(WORKER_TOKEN, WORKER_ID);

            // When
            LoginResult result = await _loginService.Login(WORKER_EMAIL, WORKER_PASSWORD);

            // Then
            Assert.IsFalse(result.Error);
            Assert.IsTrue(result.Worker);
            var token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            Assert.AreEqual(WORKER_TOKEN, token);
            var userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            Assert.AreEqual(WORKER_ID, userId);
            var worker = await _secureStorageService.GetAsync(Constants.SS_WORKER);
            Assert.IsFalse(string.IsNullOrEmpty(worker));
        }
        
        [Test]
        public async Task LoginService_CorrectClientCredentials_ClientUserReturned()
        {
            // Given
            SetupCorrectLoginMocks(CLIENT_TOKEN, CLIENT_ID);
            
            // When
            LoginResult result = await _loginService.Login(CLIENT_EMAIL, CLIENT_PASSWORD);

            // Then
            Assert.IsFalse(result.Error);
            Assert.IsFalse(result.Worker);
            var token = await _secureStorageService.GetAsync(Constants.SS_TOKEN);
            Assert.AreEqual(CLIENT_TOKEN, token);
            var userId = await _secureStorageService.GetAsync(Constants.SS_USER_ID);
            Assert.AreEqual(CLIENT_ID, userId);
            var worker = await _secureStorageService.GetAsync(Constants.SS_WORKER);
            Assert.IsTrue(string.IsNullOrEmpty(worker));
        }
        
        [Test]
        public async Task LoginService_IncorrectCredentials_UnauthorizedErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<LoginData>(It.Is<RestRequest>(
                        r => r.Parameters.Any (p => (string) p.Value == ERROR_EMAIL) &&
                             r.Parameters.Any (p => (string) p.Value == WRONG_PASSWORD)),
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<LoginData>
            {
                StatusCode = HttpStatusCode.Forbidden
            });
            
            // When
            LoginResult result = await _loginService.Login(ERROR_EMAIL, WRONG_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.IncorrectEmailOrPassword, result.Message);
        }
        
        [Test]
        public async Task LoginService_APIRandomError_ServerErrorReturned()
        {
            // Given
            _restClientMock.Setup(
                m => m.ExecuteAsync<LoginData>(It.Is<RestRequest>(
                        r => r.Parameters.Any (p => (string) p.Value == ERROR_EMAIL) &&
                             r.Parameters.Any (p => (string) p.Value == WRONG_PASSWORD)),
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<LoginData>
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            
            // When
            LoginResult result = await _loginService.Login(ERROR_EMAIL, WRONG_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.ServerError, result.Message);
        }

        void SetupCorrectLoginMocks(string userToken, string userId)
        {
            _restClientMock.Setup(m => m.ExecuteAsync<LoginData>(It.IsAny<RestRequest>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<LoginData>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new LoginData { Token = userToken, UserId = userId}
            });
            _restClientMock.Setup(m => m.ExecuteAsync<User>(It.Is<RestRequest>(
                    r => r.Parameters.Any (p => (string) p.Value == userId)),
                It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse<User>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Data = new User { IsWorker = userId == WORKER_ID, Id = userId}
            });
        }
    }
}