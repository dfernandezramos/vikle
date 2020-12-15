// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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
    /// This class implements the contact customer viewmodel tests
    /// </summary>
    [TestFixture]
    public class ContactCustomerVMTests : MvxIoCSupportingTest
    {
        Mock<IContactCustomerService> _contactCustomerServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        ContactCustomerVM _contactCustomerVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _contactCustomerServiceMock = new Mock<IContactCustomerService> ();
            Ioc.RegisterSingleton<IContactCustomerService> (_contactCustomerServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _contactCustomerVM = new ContactCustomerVM(_navigationMock.Object, _contactCustomerServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _contactCustomerServiceMock.Reset();
            _navigationMock.Reset();
        }
		
        [Test]
        public async Task Initialize_APICallFails_ErrorIsShown()
        {
            // Given
            _contactCustomerServiceMock.Setup(m => m.GetVehicleOwner(It.IsAny<string>()))
                .ReturnsAsync(new Result<User> { Error = true, Message = "Error" });
            
            // When
            await _contactCustomerVM.Initialize();
            
            // Then
            Assert.IsTrue(_contactCustomerVM.ShowContactError);
            Assert.AreEqual("Error", _contactCustomerVM.ContactError);
        }
        
        [Test]
        public async Task Initialize_APICallWorks_ModelIsSet()
        {
            // Given
            _contactCustomerServiceMock.Setup(m => m.GetVehicleOwner(It.IsAny<string>()))
                .ReturnsAsync(new Result<User>
                {
                    Data = new User ()
                });
            
            // When
            await _contactCustomerVM.Initialize();
            
            // Then
            Assert.IsFalse(_contactCustomerVM.ShowContactError);
            Assert.IsNotNull(_contactCustomerVM.Model);
        }
        
        [Test]
        public async Task CallCustomer_CallFails_ErrorShown()
        {
            // Given
            _contactCustomerServiceMock.Setup(m => m.CallCustomer(It.IsAny<string>()))
                .ReturnsAsync(new Result<User> { Error = true, Message = "Error" });
            
            // When
            await _contactCustomerVM.CallCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_contactCustomerVM.ShowContactError);
            Assert.AreEqual("Error", _contactCustomerVM.ContactError);
        }
        
        [Test]
        public async Task CallCustomer_CallWorks_NoErrorShown()
        {
            // Given
            _contactCustomerServiceMock.Setup(m => m.CallCustomer(It.IsAny<string>()))
                .ReturnsAsync(new Result<User>
                {
                    Data = new User ()
                });
            
            // When
            await _contactCustomerVM.CallCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_contactCustomerVM.ShowContactError);
        }
        
        [Test]
        public async Task SenEmail_EmailFails_ErrorShown()
        {
            // Given
            _contactCustomerVM.Model = new User
            {
                Email = "user@email.com"
            };
            _contactCustomerServiceMock.Setup(m => m.SendEmail(It.IsAny<string>()))
                .ReturnsAsync(new Result<User> { Error = true, Message = "Error" });
            
            // When
            await _contactCustomerVM.SendEmailCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_contactCustomerVM.ShowContactError);
            Assert.AreEqual("Error", _contactCustomerVM.ContactError);
        }
        
        [Test]
        public async Task SendEmail_EmailWorks_NoErrorShown()
        {
            // Given
            _contactCustomerVM.Model = new User
            {
                Email = "user@email.com"
            };
            _contactCustomerServiceMock.Setup(m => m.SendEmail(It.IsAny<string>()))
                .ReturnsAsync(new Result<User>
                {
                    Data = new User ()
                });
            
            // When
            await _contactCustomerVM.SendEmailCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_contactCustomerVM.ShowContactError);
        }
    }
}