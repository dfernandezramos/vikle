// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NUnit.Framework;
using Vikle.Core;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;

namespace Vikle.Tests.ViewModels
{
    /// <summary>
    /// This class contains the implementations of the unit tests for the signup viewmodel
    /// </summary>
    [TestFixture]
    public class SignupVMVMTests : MvxIoCSupportingTest
    {
        Mock<ISignupService> _signupServiceMock;
        Mock<IMvxNavigationService> _navigationMock;
        SignupVM _signupVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            _signupServiceMock = new Mock<ISignupService>();
            Ioc.RegisterSingleton<ISignupService>(_signupServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService>();
            _signupVM = new SignupVM(_navigationMock.Object, _signupServiceMock.Object);
        }

        [SetUp]
        public void Init()
        {
            base.Setup();
        }

        [TearDown]
        public void Cleanup()
        {
            _signupServiceMock.Reset();
            _navigationMock.Reset();
        }

        [Test]
        public async Task RecoverCommand_RecoverFails_ErrorIsShown()
        {
            // Given
            _signupServiceMock.Setup(m => m.SignUp(It.IsAny<SignupData>()))
                .ReturnsAsync(new Result {Error = true, Message = "Error"});

            // When
            await _signupVM.SignupCommand.ExecuteAsync();

            // Then
            Assert.IsTrue(_signupVM.ShowSignupError);
            Assert.AreEqual("Error", _signupVM.SignupError);
        }

        [Test]
        public async Task RecoverCommand_RecoverSuccessful_NavigationToConfirmationViewPerformed()
        {
            // Given
            _signupServiceMock.Setup(m => m.SignUp(It.IsAny<SignupData>()))
                .ReturnsAsync(new Result());

            // When
            await _signupVM.SignupCommand.ExecuteAsync();

            // Then
            Assert.IsFalse(_signupVM.ShowSignupError);
            _navigationMock.Verify(m => m.Navigate<ConfirmationVM, ConfirmationParams>(
                It.Is<ConfirmationParams>(cp => cp.Title == Strings.SignupConfirmationViewTitle &&
                                                cp.Subtitle == Strings.SignupConfirmationViewSubtitle), null,
                It.IsAny<CancellationToken>()), times: Times.Once);
        }
    }
}