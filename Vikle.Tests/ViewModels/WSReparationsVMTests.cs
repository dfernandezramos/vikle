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
using MvvmCross.ViewModels;
using NUnit.Framework;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;

namespace Vikle.Tests.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the workshop reparations service tests
    /// </summary>
    [TestFixture]
    public class WSReparationsVMTests : MvxIoCSupportingTest
    {
        Mock<IWSReparationsService> _wsReparationsServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        WSReparationsVM _reparationsVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _wsReparationsServiceMock = new Mock<IWSReparationsService> ();
            Ioc.RegisterSingleton<IWSReparationsService> (_wsReparationsServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _reparationsVM = new WSReparationsVM(_navigationMock.Object, _wsReparationsServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _wsReparationsServiceMock.Reset();
            _navigationMock.Reset();
        }
        
        [Test]
        public async Task InitializeWSReparationsVM_WorkshopIdAPICallFails_ErrorIsShown()
        {
            // Given
            _wsReparationsServiceMock.Setup(m => m.GetUserWorkshopId())
                .ReturnsAsync(new Result<string> { Error = true, Message = "Error" });
            
            // When
            _reparationsVM.CallingAPI = true;
            _reparationsVM.ViewAppearing();
            while (_reparationsVM.CallingAPI) { }

            // Then
            Assert.IsTrue(_reparationsVM.ShowReparationsError);
            Assert.AreEqual("Error", _reparationsVM.ReparationsError);
        }
        
        [Test]
        public async Task InitializeWSReparationsVM_WorkshopIdAPICallWorksAndReparationsAPICallFails_ErrorIsShown()
        {
            // Given
            _wsReparationsServiceMock.Setup(m => m.GetUserWorkshopId())
                .ReturnsAsync(new Result<string>
                {
                    Data = "1234"
                });
            _wsReparationsServiceMock.Setup(m => m.GetReparations(It.IsAny<string>()))
                .ReturnsAsync(new Result<MvxObservableCollection<Reparation>> { Error = true, Message = "Error" });
            
            // When
            _reparationsVM.CallingAPI = true;
            _reparationsVM.ViewAppearing();
            while (_reparationsVM.CallingAPI) { }
            
            // Then
            Assert.IsTrue(_reparationsVM.ShowReparationsError);
            Assert.AreEqual("Error", _reparationsVM.ReparationsError);
        }
        
        [Test]
        public async Task InitializeWSReparationsVM_WorkshopIdAndReparationsAPICallWorks_ReparationsReturned()
        {
            // Given
            _wsReparationsServiceMock.Setup(m => m.GetUserWorkshopId())
                .ReturnsAsync(new Result<string>
                {
                    Data = "1234"
                });
            _wsReparationsServiceMock.Setup(m => m.GetReparations(It.IsAny<string>()))
                .ReturnsAsync(new Result<MvxObservableCollection<Reparation>>
                {
                    Data = new MvxObservableCollection<Reparation>()
                });
            
            // When
            _reparationsVM.CallingAPI = true;
            _reparationsVM.ViewAppearing();
            while (_reparationsVM.CallingAPI) { }
            
            // Then
            Assert.IsFalse(_reparationsVM.ShowReparationsError);
            Assert.IsNotNull(_reparationsVM.Reparations);
        }
    }
}