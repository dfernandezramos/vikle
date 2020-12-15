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
    /// This class contains the implementations of the unit tests for the recover password viewmodel
    /// </summary>
    [TestFixture]
    public class RecoverPasswordVMTests : MvxIoCSupportingTest
    {
        Mock<IRecoverPasswordService> _recoverPasswordServiceMock;
        Mock<IMvxNavigationService> _navigationMock;
        RecoverPasswordVM _recoverVM;
        
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            
            _recoverPasswordServiceMock = new Mock<IRecoverPasswordService> ();
            Ioc.RegisterSingleton<IRecoverPasswordService> (_recoverPasswordServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _recoverVM = new RecoverPasswordVM(_navigationMock.Object, _recoverPasswordServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _recoverPasswordServiceMock.Reset();
            _navigationMock.Reset();
        }
        
        [Test]
        public async Task RecoverCommand_RecoverFails_ErrorIsShown()
        {
            // Given
            _recoverPasswordServiceMock.Setup(m => m.RecoverPassword(It.IsAny<string>()))
                .ReturnsAsync(new Result { Error = true, Message = "Error" });
            
            // When
            await _recoverVM.RecoverPasswordCommand.ExecuteAsync();

            // Then
            Assert.IsTrue(_recoverVM.ShowRecoverError);
            Assert.AreEqual("Error", _recoverVM.RecoverError);
        }
        
        [Test]
        public async Task RecoverCommand_RecoverSuccessful_NavigationToConfirmationViewPerformed()
        {
            // Given
            _recoverPasswordServiceMock.Setup(m => m.RecoverPassword(It.IsAny<string>()))
                .ReturnsAsync(new Result ());
            
            // When
            await _recoverVM.RecoverPasswordCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_recoverVM.ShowRecoverError);
            _navigationMock.Verify(m => m.Navigate<ConfirmationVM, ConfirmationParams> (
                It.Is<ConfirmationParams>(cp => cp.Title == Strings.ResetConfirmationViewTitle &&
                                                cp.Subtitle == Strings.ResetConfirmationViewSubtitle), null,
                It.IsAny<CancellationToken>()), times: Times.Once);
        }
    }
}