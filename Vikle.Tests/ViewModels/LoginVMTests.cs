// ReSharper disable once RedundantUsingDirective
using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NUnit.Framework;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;

namespace Vikle.Tests.ViewModels
{
    /// <summary>
    /// This class contains the implementations of the unit tests for the login viewmodel
    /// </summary>
    [TestFixture]
    public class LoginVMTests : MvxIoCSupportingTest
    {
        Mock<ILoginService> _loginServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        LoginVM _loginVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _loginServiceMock = new Mock<ILoginService> ();
            Ioc.RegisterSingleton<ILoginService> (_loginServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _loginVM = new LoginVM(_navigationMock.Object, _loginServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _loginServiceMock.Reset();
            _navigationMock.Reset();
        }
		
        [Test]
        public async Task LoginCommand_LoginFails_ErrorIsShown()
        {
            // Given
            _loginServiceMock.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new LoginResult { Error = true, Message = "Error" });
            
            // When
            await _loginVM.LoginCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_loginVM.ShowLoginError);
            Assert.AreEqual("Error", _loginVM.LoginError);
        }
        
        [Test]
        public async Task LoginCommand_LoginReturnsWorker_NavigationToWSReparationsIsPerformed()
        {
            // Given
            _loginServiceMock.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new LoginResult { Worker = true });
            
            // When
            await _loginVM.LoginCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_loginVM.ShowLoginError);
            _navigationMock.Verify(m => m.Navigate<WSReparationsVM> (null,
                It.IsAny<CancellationToken>()), times: Times.Once);
        }
        
        [Test]
        public async Task LoginCommand_LoginReturnsClient_NavigationToVehiclesIsPerformed()
        {
            // Given
            _loginServiceMock.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new LoginResult { Worker = false });
            
            // When
            await _loginVM.LoginCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_loginVM.ShowLoginError);
            _navigationMock.Verify(m => m.Navigate<VehiclesVM> (null,
                It.IsAny<CancellationToken>()), times: Times.Once);
        }
    }
}