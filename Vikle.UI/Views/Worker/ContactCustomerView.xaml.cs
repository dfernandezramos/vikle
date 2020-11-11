using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the customer contact view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class ContactCustomerView : MvxContentPage<ContactCustomerVM>
    {
        public ContactCustomerView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
        }
    }
}