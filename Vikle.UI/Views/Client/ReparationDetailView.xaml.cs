using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the client reparation detail view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class ReparationDetailView : MvxContentPage<ReparationDetailVM>
    {
        public ReparationDetailView()
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