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
    /// This class contains the implementation of the vehicles viewmodel tests
    /// </summary>
    [TestFixture]
    public class VehiclesVMTests : MvxIoCSupportingTest
    {
        Mock<IVehiclesService> _vehiclesServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        VehiclesVM _vehiclesVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _vehiclesServiceMock = new Mock<IVehiclesService>();
            Ioc.RegisterSingleton<IVehiclesService>(_vehiclesServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService>();
            _vehiclesVM = new VehiclesVM(_navigationMock.Object, _vehiclesServiceMock.Object);
        }

        [SetUp]
        public void Init()
        {
            base.Setup();
        }

        [TearDown]
        public void Cleanup()
        {
            _vehiclesServiceMock.Reset();
            _navigationMock.Reset();
        }

        [Test]
        public async Task InitializeVehiclesVM_APICallFails_ErrorIsShown()
        {
            // Given
            _vehiclesServiceMock.Setup(m => m.GetUserVehicles())
                .ReturnsAsync(new Result<MvxObservableCollection<Vehicle>> {Error = true, Message = "Error"});

            // When
            _vehiclesVM.CallingAPI = true;
            _vehiclesVM.ViewAppearing();
            while (_vehiclesVM.CallingAPI){ }

            // Then
            Assert.IsTrue(_vehiclesVM.ShowVehiclesError);
            Assert.AreEqual("Error", _vehiclesVM.VehiclesError);
        }

        [Test]
        public async Task InitializeVehiclesVM_APICallWorks_EditionModeChanged()
        {
            // Given
            _vehiclesServiceMock.Setup(m => m.GetUserVehicles())
                .ReturnsAsync(new Result<MvxObservableCollection<Vehicle>>
                {
                    Data = new MvxObservableCollection<Vehicle>(),
                });

            // When
            _vehiclesVM.CallingAPI = true;
            _vehiclesVM.ViewAppearing();
            while (_vehiclesVM.CallingAPI){ }

            // Then
            Assert.IsFalse(_vehiclesVM.ShowVehiclesError);
            Assert.IsNotNull(_vehiclesVM.Vehicles);
        }
    }
}