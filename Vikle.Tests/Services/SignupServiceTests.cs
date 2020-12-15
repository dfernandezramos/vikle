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
    /// This class contains the implementations of the unit tests for the signup service
    /// </summary>
    [TestFixture]
    public class SignupServiceTests : MvxIoCSupportingTest
    {
        SignupService _signupService;
        Mock<IRestClient> _restClientMock;
        SignupData data;
        
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            _restClientMock = new Mock<IRestClient> ();
            Ioc.RegisterSingleton<IRestClient> (_restClientMock.Object);
            Ioc.RegisterSingleton<IApiClientService> (new ApiClientService());
            _signupService = new SignupService();
        }

        [SetUp]
        public void Init() {
            base.Setup();
            data = new SignupData
            {
                Name = "David",
                Surname = "Fernandez",
                Phone = "123456789",
            };
        }

        [TearDown]
        public void TearDown()
        {
            _restClientMock.Reset();
        }
        
        [Test]
        public async Task SignupService_IncompleteDataProvided_DataRequiredErrorShown()
        {
            // Given
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.AllFieldsAreRequired, result.Message);
        }
        
        [Test]
        public async Task SignupService_InvalidEmailProvided_InvalidEmailErrorShown()
        {
            // Given
            data.RepeatedPassword = data.Password = "*";
            data.Email = "dfernandezramos";
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EnterValidEmail, result.Message);
        }
        
        [Test]
        public async Task SignupService_DifferentPasswords_DifferentPasswordsErrorShown()
        {
            // Given
            data.RepeatedPassword = "pass1";
            data.Password = "pass2";
            data.Email = "email@valid.com";
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.PasswordsAreNotEqual, result.Message);
        }
        
        [Test]
        public async Task SignupService_ShortPasswords_ShortPasswordErrorShown()
        {
            // Given
            data.RepeatedPassword = "pass1";
            data.Password = "pass1";
            data.Email = "email@valid.com";
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.IncorrectPasswordLength, result.Message);
        }
        
        [Test]
        public async Task SignupService_IncorrectPasswordFormat_IncorrectPasswordFormatErrorShown()
        {
            // Given
            data.RepeatedPassword = "password1";
            data.Password = "password1";
            data.Email = "email@valid.com";
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.PasswordIncorrectFormat, result.Message);
        }
        
        [Test]
        public async Task SignupService_CorrectDataFormat_NoErrorReturned()
        {
            // Given
            data.RepeatedPassword = "Password1";
            data.Password = "Password1";
            data.Email = "email@valid.com";
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed
            });
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsFalse(result.Error);
        }
        
        [Test]
        public async Task SignupService_ErrorOnAPI_ServerErrorShown()
        {
            // Given
            data.RepeatedPassword = "Password1";
            data.Password = "Password1";
            data.Email = "email@valid.com";
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.ServerError, result.Message);
        }
        
        [Test]
        public async Task SignupService_EmailAlreadyInUse_EmailInUseErrorShown()
        {
            // Given
            data.RepeatedPassword = "Password1";
            data.Password = "Password1";
            data.Email = "email@valid.com";
            _restClientMock.Setup(
                m => m.ExecuteAsync(It.IsAny<RestRequest>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.Conflict
            });
            
            // When
            Result result = await _signupService.SignUp(data);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EmailAlreadyInUse, result.Message);
        }
    }
}