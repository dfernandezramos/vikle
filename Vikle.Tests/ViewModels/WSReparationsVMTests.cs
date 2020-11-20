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
            await _reparationsVM.Initialize();
            
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
            await _reparationsVM.Initialize();
            
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
            await _reparationsVM.Initialize();
            
            // Then
            Assert.IsFalse(_reparationsVM.ShowReparationsError);
            Assert.IsNotNull(_reparationsVM.Reparations);
        }
    }
}