using NUnit.Framework;
using Vikle.Core;
using Vikle.Core.Models;
using Vikle.Core.Services;

namespace Vikle.Tests.Services
{
    /// <summary>
    /// This class contains the implementations of the unit tests for the login service
    /// </summary>
    [TestFixture]
    public class LoginServiceTests
    {
        const string CLIENT_EMAIL = "client@email.com";
        const string WORKER_EMAIL = "worker@email.com";
        const string ERROR_EMAIL = "error@email.com";
        const string CLIENT_PASSWORD = "clientpassword";
        const string WORKER_PASSWORD = "workerpassword";
        const string WRONG_PASSWORD = "wrongpassword";
        
        LoginService _loginService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _loginService = new LoginService();
        }

        [Test]
        public void LoginService_NoEmailProvided_DataRequiredErrorShown()
        {
            // Given
            // When
            LoginResult result = _loginService.Login(null, CLIENT_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EmailPasswordRequired, result.Message);
        }
        
        [Test]
        public void LoginService_NoPasswordProvided_DataRequiredErrorShown()
        {
            // Given
            // When
            LoginResult result = _loginService.Login(CLIENT_EMAIL, null);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EmailPasswordRequired, result.Message);
        }
        
        [Test]
        public void LoginService_InvalidEmailProvided_WrongEmailErrorShown()
        {
            // Given
            // When
            LoginResult result = _loginService.Login("invalid", CLIENT_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual(Strings.EnterValidEmail, result.Message);
        }
        
        [Test]
        public void LoginService_CorrectWorkerCredentials_WorkerUserReturned()
        {
            // Given
            // TODO: Pending implement API mock here
            
            // When
            LoginResult result = _loginService.Login(WORKER_EMAIL, WORKER_PASSWORD);

            // Then
            Assert.IsFalse(result.Error);
            Assert.IsTrue(result.Worker);
        }
        
        [Test]
        public void LoginService_CorrectClientCredentials_ClientUserReturned()
        {
            // Given
            // TODO: Pending implement API mock here
            
            // When
            LoginResult result = _loginService.Login(CLIENT_EMAIL, CLIENT_PASSWORD);

            // Then
            Assert.IsFalse(result.Error);
            Assert.IsFalse(result.Worker);
        }
        
        [Test]
        public void LoginService_IncorrectCredentials_UnauthorizedErrorReturned()
        {
            // Given
            // TODO: Pending implement API mock here
            
            // When
            LoginResult result = _loginService.Login(ERROR_EMAIL, WRONG_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual("Incorrect email or password", result.Message);
        }
        
        [Test]
        public void LoginService_APIRandomError_ServerErrorReturned()
        {
            // Given
            // TODO: Pending implement API mock here
            
            // When
            LoginResult result = _loginService.Login(ERROR_EMAIL, WRONG_PASSWORD);

            // Then
            Assert.IsTrue(result.Error);
            Assert.AreEqual("Server error. Try again later", result.Message);
        }
    }
}