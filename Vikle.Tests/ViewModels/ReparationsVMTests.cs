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
    /// This class contains the implementation of the reparations viewmodel tests
    /// </summary>
    [TestFixture]
    public class ReparationsVMTests : MvxIoCSupportingTest
    {
        Mock<IHistoryService> _historyServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        ReparationsVM _reparationsVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _historyServiceMock = new Mock<IHistoryService> ();
            Ioc.RegisterSingleton<IHistoryService> (_historyServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _reparationsVM = new ReparationsVM(_navigationMock.Object, _historyServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _historyServiceMock.Reset();
            _navigationMock.Reset();
        }
		
        [Test]
        public async Task InitializeReparationsVM_APICallFails_ErrorIsShown()
        {
            // Given
            _historyServiceMock.Setup(m => m.GetReparations(It.IsAny<string>()))
                .ReturnsAsync(new Result<MvxObservableCollection<Reparation>> { Error = true, Message = "Error" });
            
            // When
            await _reparationsVM.Initialize();
            
            // Then
            Assert.IsTrue(_reparationsVM.ShowHistoryError);
            Assert.AreEqual("Error", _reparationsVM.HistoryError);
        }
        
        [Test]
        public async Task InitializeReparationsVM_APICallWorks_EditionModeChanged()
        {
            // Given
            _historyServiceMock.Setup(m => m.GetReparations(It.IsAny<string>()))
                .ReturnsAsync(new Result<MvxObservableCollection<Reparation>>
                {
                    Data = new MvxObservableCollection<Reparation>()
                });
            
            // When
            await _reparationsVM.Initialize();
            
            // Then
            Assert.IsFalse(_reparationsVM.ShowHistoryError);
            Assert.IsNotNull(_reparationsVM.Reparations);
        }
    }
}