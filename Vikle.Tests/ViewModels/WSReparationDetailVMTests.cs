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
    /// This class contains the implementation of the workshop reparation details viewmodel tests
    /// </summary>
    [TestFixture]
    public class WSReparationDetailVMTests : MvxIoCSupportingTest
    {
        Mock<IWSReparationDetailService> _reparationDetailServiceMock;
        private Mock<IMvxNavigationService> _navigationMock;
        WSReparationDetailVM _reparationDetailVM;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            _reparationDetailServiceMock = new Mock<IWSReparationDetailService> ();
            Ioc.RegisterSingleton<IWSReparationDetailService> (_reparationDetailServiceMock.Object);
            _navigationMock = new Mock<IMvxNavigationService> ();
            _reparationDetailVM = new WSReparationDetailVM(_navigationMock.Object, _reparationDetailServiceMock.Object);
        }
        
        [SetUp]
        public void Init() {
            base.Setup();
        }
        
        [TearDown] 
        public void Cleanup()
        {
            _reparationDetailServiceMock.Reset();
            _navigationMock.Reset();
        }
        
        [Test]
        public async Task ContinueCommand_APICallFails_ErrorShown()
        {
            // Given
            _reparationDetailVM.Model = new Reparation
            {
                PlateNumber = "1234 ABC",
            };
            _reparationDetailServiceMock.Setup(m => m.UpdateReparation(_reparationDetailVM.Model))
                .ReturnsAsync(new Result { Error = true, Message = "Error" });
            
            // When
            await _reparationDetailVM.ContinueCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_reparationDetailVM.ShowDetailError);
            Assert.AreEqual("Error", _reparationDetailVM.DetailError);
        }
        
        [Test]
        public async Task ContinueCommand_IncompleteModel_ErrorShown()
        {
            // Given
            _reparationDetailVM.Model = new Reparation();
            _reparationDetailVM.NewReparation = true;

            // When
            await _reparationDetailVM.ContinueCommand.ExecuteAsync();
            
            // Then
            Assert.IsTrue(_reparationDetailVM.ShowDetailError);
            Assert.AreEqual(Strings.AllFieldsAreRequired, _reparationDetailVM.DetailError);
        }
        
        [Test]
        public async Task ContinueCommand_APICallWorks_NavigationBackDone()
        {
            // Given
            _reparationDetailVM.Model = new Reparation
            {
                PlateNumber = "1234 ABC",
            };
            _reparationDetailServiceMock.Setup(m => m.UpdateReparation(_reparationDetailVM.Model))
                .ReturnsAsync(new Result ());
            
            // When
            await _reparationDetailVM.ContinueCommand.ExecuteAsync();
            
            // Then
            Assert.IsFalse(_reparationDetailVM.ShowDetailError);
            _navigationMock.Verify(nm => nm.Close(_reparationDetailVM, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}