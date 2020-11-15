using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using CollectionView = Xamarin.Forms.CollectionView;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the client dates view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class DatesView : MvxContentPage<DatesVM>
    {
        public DatesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            DatesCollectionView.BindingContext = ViewModel;
            DatesCollectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(ViewModel.Dates));
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
        }
    }
}