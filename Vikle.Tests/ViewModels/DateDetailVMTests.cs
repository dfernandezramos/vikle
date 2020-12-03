using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Base;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using MvvmCross.ViewModels;
using NUnit.Framework;
using Vikle.Core;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;

namespace Vikle.Tests.ViewModels
{
    /// <summary>
    /// This class implements the date detail viewmodel tests
    /// </summary>
    [TestFixture]
    public class DateDetailVMTests : MvxIoCSupportingTest
    {
        Mock<IDateDetailsService> _dateDetailServiceMock;
        Mock<IVehiclesService> _vehiclesServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        DateDetailVM _dateDetailVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _dateDetailServiceMock = new Mock<IDateDetailsService> ();
            Ioc.RegisterSingleton<IDateDetailsService> (_dateDetailServiceMock.Object);
            _vehiclesServiceMock = new Mock<IVehiclesService> ();
            Ioc.RegisterSingleton<IVehiclesService> (_vehiclesServiceMock.Object);
            Ioc.RegisterSingleton<IMvxMainThreadAsyncDispatcher>(Mock.Of<IMvxMainThreadAsyncDispatcher> ());
            _navigationMock = new Mock<IMvxNavigationService> ();
            _dateDetailVM = new DateDetailVM(_navigationMock.Object, _vehiclesServiceMock.Object, _dateDetailServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _dateDetailServiceMock.Reset();
            _navigationMock.Reset();
        }

        [Test]
        public async Task UpdateDateCommand_APICallFails_ErrorShown()
        {
            // Given
            _dateDetailVM.ReparationDate = DateTime.Today;
            _dateDetailVM.ReparationReason = ReparationType.Maintenance;
            _dateDetailVM.SelectedVehiclePlateNumber = "1234 ABC";
            _dateDetailServiceMock.Setup(m => m.SaveDate(It.IsAny<Date>()))
                .ReturnsAsync(new Result { Error = true, Message = "Error" });
            
            // When
            await _dateDetailVM.UpdateDateCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_dateDetailVM.ShowDateDetailsError);
            Assert.AreEqual("Error", _dateDetailVM.DateDetailsError);
        }

        [Test]
        public async Task UpdateDateCommand_SaveDate_NoErrorsReturned()
        {
            // Given
            _dateDetailVM.ReparationDate = DateTime.Today;
            _dateDetailVM.ReparationReason = ReparationType.Maintenance;
            _dateDetailVM.SelectedVehiclePlateNumber = "1234 ABC";
            _dateDetailServiceMock.Setup(m => m.SaveDate(It.IsAny<Date>()))
                .ReturnsAsync(new Result ());
            
            // When
            await _dateDetailVM.UpdateDateCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_dateDetailVM.ShowDateDetailsError);
            _navigationMock.Verify(n => n.Close(It.IsAny<DateDetailVM>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Test]
        public async Task Initialization_APICallFails_ErrorShown()
        {
            // Given
            _vehiclesServiceMock.Setup(m => m.GetUserVehicles())
                .ReturnsAsync(new Result<MvxObservableCollection<Vehicle>> { Error = true });
            
            // When
            await _dateDetailVM.Initialize();
            
            // Then
            Assert.IsTrue(_dateDetailVM.ShowDateDetailsError);
        }
        
        [Test]
        public async Task Initialization_NoVehicles_ErrorShown()
        {
            // Given
            _vehiclesServiceMock.Setup(m => m.GetUserVehicles())
                .ReturnsAsync(new Result<MvxObservableCollection<Vehicle>> { Error = false });
            
            // When
            await _dateDetailVM.Initialize();
            
            // Then
            Assert.IsTrue(_dateDetailVM.ShowDateDetailsError);
            Assert.AreEqual(Strings.NoVehicles, _dateDetailVM.DateDetailsError);
        }
        
        [Test]
        public async Task Initialization_APICallWorks_VehiclesReturned()
        { 
            // Given
            _vehiclesServiceMock.Setup(m => m.GetUserVehicles())
                .ReturnsAsync(new Result<MvxObservableCollection<Vehicle>>
                {
                    Data = new MvxObservableCollection<Vehicle>
                    {
                        new Vehicle
                        {
                            IdClient = "1",
                            IdDrivers = new List<string> (),
                            LastITV = DateTime.Today,
                            LastTBDS = DateTime.Today,
                            Model = "Vespino",
                            PlateNumber = "1234 ABC",
                            VehicleType = VehicleType.MotorCycle,
                            Year = 2015
                        }
                    }
                });
            
            // When
            await _dateDetailVM.Initialize();
            
            // Then
            Assert.IsFalse(_dateDetailVM.ShowDateDetailsError);
            Assert.AreEqual("1234ABC" , _dateDetailVM.PlateNumbers.First());
        }
    }
}