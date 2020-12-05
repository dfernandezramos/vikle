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