using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the implementation of the Menu page as Master for the MasterDetailPage in the Worker side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class WorkerMenuPage : MvxContentPage<WorkerMenuVM>
    {
        public WorkerMenuPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            var reparationsTapGestureRecognizer = new TapGestureRecognizer();
            reparationsTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.ReparationsNavigationCommand.ExecuteAsync();
            ReparationsButton.GestureRecognizers.Add(reparationsTapGestureRecognizer);
            
            var logoutTapGestureRecognizer = new TapGestureRecognizer();
            logoutTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.LogoutCommand.ExecuteAsync();
            LogoutButton.GestureRecognizers.Add(logoutTapGestureRecognizer);
        }
    }
}