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
    /// This class contains the implementation of the dates viewmodel tests
    /// </summary>
    [TestFixture]
    public class DatesVMTests : MvxIoCSupportingTest
    {
        Mock<IDatesService> _datesServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        DatesVM _datesVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _datesServiceMock = new Mock<IDatesService> ();
            Ioc.RegisterSingleton<IDatesService> (_datesServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _datesVM = new DatesVM(_navigationMock.Object, _datesServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _datesServiceMock.Reset();
            _navigationMock.Reset();
        }
		
        [Test]
        public async Task InitializeDatesVM_APICallFails_ErrorIsShown()
        {
            // Given
            _datesServiceMock.Setup(m => m.GetUserDates())
                .ReturnsAsync(new Result<MvxObservableCollection<Date>> { Error = true, Message = "Error" });
            
            // When
            _datesVM.CallingAPI = true;
            _datesVM.ViewAppearing();
            while (_datesVM.CallingAPI){ }

            // Then
            Assert.IsTrue(_datesVM.ShowDatesError);
            Assert.AreEqual("Error", _datesVM.DatesError);
        }
        
        [Test]
        public async Task InitializeDatesVM_APICallWorks_EditionModeChanged()
        {
            // Given
            _datesServiceMock.Setup(m => m.GetUserDates())
                .ReturnsAsync(new Result<MvxObservableCollection<Date>>
                {
                    Data = new MvxObservableCollection<Date>()
                });
            
            // When
            _datesVM.CallingAPI = true;
            _datesVM.ViewAppearing();
            while (_datesVM.CallingAPI){ }
            
            // Then
            Assert.IsFalse(_datesVM.ShowDatesError);
            Assert.IsNotNull(_datesVM.Dates);
        }
    }
}