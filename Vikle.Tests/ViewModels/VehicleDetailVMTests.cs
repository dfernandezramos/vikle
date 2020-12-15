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
using Vikle.Core;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;
using Vikle.Core.ViewModels;

namespace Vikle.Tests.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the vehicle details viewmodel tests
    /// </summary>
    [TestFixture]
    public class VehicleDetailVMTests : MvxIoCSupportingTest
    {
        Mock<IVehicleDetailService> _vehicleDetailServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        VehicleDetailVM _vehicleDetailVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _vehicleDetailServiceMock = new Mock<IVehicleDetailService> ();
            Ioc.RegisterSingleton<IVehicleDetailService> (_vehicleDetailServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _vehicleDetailVM = new VehicleDetailVM(_navigationMock.Object, _vehicleDetailServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _vehicleDetailServiceMock.Reset();
            _navigationMock.Reset();
        }
		
        [Test]
        public async Task DeleteVehicleCommand_APICallFails_ErrorIsShown()
        {
            // Given
            _vehicleDetailVM.EditionMode = true;
            _vehicleDetailServiceMock.Setup(m => m.DeleteVehicle(It.IsAny<string>()))
                .ReturnsAsync(new Result { Error = true, Message = "Error" });
            
            // When
            await _vehicleDetailVM.DeleteVehicleCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_vehicleDetailVM.EditionMode);
            Assert.IsTrue(_vehicleDetailVM.ShowDetailError);
            Assert.AreEqual("Error", _vehicleDetailVM.DetailError);
        }
        
        [Test]
        public async Task DeleteVehicleCommand_APICallWorks_EditionModeChanged()
        {
            // Given
            _vehicleDetailVM.EditionMode = true;
            _vehicleDetailServiceMock.Setup(m => m.DeleteVehicle(It.IsAny<string>()))
                .ReturnsAsync(new Result());
            
            // When
            await _vehicleDetailVM.DeleteVehicleCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_vehicleDetailVM.EditionMode);
            Assert.IsFalse(_vehicleDetailVM.ShowDetailError);
        }
        
        [Test]
        public async Task UpdateVehicleCommand_APICallFails_ErrorShown()
        {
            // Given
            _vehicleDetailVM.EditionMode = true;
            _vehicleDetailVM.Model = new Vehicle
            {
                PlateNumber = "1234 ABC",
                Model = "Audi A3",
                IdClient = "1"
            };
            _vehicleDetailServiceMock.Setup(m => m.UpdateVehicle(It.IsAny<string>(), _vehicleDetailVM.Model))
                .ReturnsAsync(new Result { Error = true, Message = "Error" });
            
            // When
            await _vehicleDetailVM.UpdateVehicleCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_vehicleDetailVM.EditionMode);
            Assert.IsTrue(_vehicleDetailVM.ShowDetailError);
            Assert.AreEqual("Error", _vehicleDetailVM.DetailError);
        }
        
        [Test]
        public async Task UpdateVehicleCommand_IncompleteModel_ErrorShown()
        {
            // Given
            _vehicleDetailVM.EditionMode = true;
            _vehicleDetailVM.Model = new Vehicle();

            // When
            await _vehicleDetailVM.UpdateVehicleCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_vehicleDetailVM.EditionMode);
            Assert.IsTrue(_vehicleDetailVM.ShowDetailError);
            Assert.AreEqual(Strings.AllFieldsAreRequired, _vehicleDetailVM.DetailError);
        }
        
        [Test]
        public async Task UpdateVehicleCommand_ChangePlateNumber_PlateNumberChanged()
        {
            // Given
            var vehicle = new Vehicle
            {
                PlateNumber = "1234 ABC",
                Model = "Audi A3",
                IdClient = "1"
            };
            _vehicleDetailVM.Prepare((vehicle, true));
            _vehicleDetailVM.PlateNumber = "5678 DEF";
            _vehicleDetailServiceMock.Setup(m => m.UpdateVehicle("1234ABC", _vehicleDetailVM.Model))
                .ReturnsAsync(new Result ());
            
            // When
            await _vehicleDetailVM.UpdateVehicleCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_vehicleDetailVM.EditionMode);
            Assert.IsFalse(_vehicleDetailVM.ShowDetailError);
            Assert.AreEqual("5678DEF", _vehicleDetailVM.PlateNumber);
        }
        
        [Test]
        public async Task Initialization_APICallFails_ErrorShown()
        {
            // Given
            _vehicleDetailVM.Model = new Vehicle
            {
                PlateNumber = "1234 ABC",
                Model = "Audi A3",
                IdClient = "1"
            };
            _vehicleDetailServiceMock.Setup(m => m.GetReparationStatus(It.IsAny<string>()))
                .ReturnsAsync(new Result<ReparationStatus> { Error = true });
            
            // When
            await _vehicleDetailVM.Initialize();
            
            // Then
            Assert.IsTrue(_vehicleDetailVM.ShowDetailError);
        }
        
        [Test]
        public async Task Initialization_VehicleIsOnReparation_StatusReturned()
        {
            // Given
            _vehicleDetailVM.Model = new Vehicle
            {
                PlateNumber = "1234 ABC",
                Model = "Audi A3",
                IdClient = "1"
            };
            _vehicleDetailServiceMock.Setup(m => m.GetReparationStatus(It.IsAny<string>()))
                .ReturnsAsync(new Result<ReparationStatus> { Data = ReparationStatus.Repaired });
            
            // When
            await _vehicleDetailVM.Initialize();
            
            // Then
            Assert.IsFalse(_vehicleDetailVM.ShowDetailError);
            Assert.AreEqual(ReparationStatus.Repaired , _vehicleDetailVM.ReparationStatus);
            Assert.IsTrue(_vehicleDetailVM.ShowReparationStatus);
        }
        
        [Test]
        public async Task Initialization_VehicleIsNotOnReparation_NoStatusReturned()
        {
            // Given
            _vehicleDetailVM.Model = new Vehicle
            {
                PlateNumber = "1234 ABC",
                Model = "Audi A3",
                IdClient = "1"
            };
            _vehicleDetailServiceMock.Setup(m => m.GetReparationStatus(It.IsAny<string>()))
                .ReturnsAsync(new Result<ReparationStatus> { Error = true });
            
            // When
            await _vehicleDetailVM.Initialize();
            
            // Then
            Assert.IsTrue(_vehicleDetailVM.ShowDetailError);
            Assert.IsNull(_vehicleDetailVM.ReparationStatus);
            Assert.IsFalse(_vehicleDetailVM.ShowReparationStatus);
        }
    }
}