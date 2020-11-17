using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core;
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
        private bool _onSelection;

        public DatesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
            NewDateButton.Title = Strings.NewDate;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.NewDateDetailsCommand.ExecuteAsync();
            NewDateButton.GestureRecognizers.Add(tapGestureRecognizer);

            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DatesError));
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowDatesError));
            DatesCollection.BindingContext = ViewModel;
            DatesCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(ViewModel.Dates));
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
        }
    }
}